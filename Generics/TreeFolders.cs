using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using TreeViewMS;

namespace Common.Controls
{
    class TreeFolders : TreeViewMS.TreeViewMS
    {
        bool IsEditing = false;

        public virtual bool Exists(string path)
        {
            return false;
        }

        public virtual bool Includes(string path)
        {
            return false;
        }

        public virtual string ToolTipText(string path)
        {
            return "";
        }

        private string GetName(string fullpath, string def)
        {
            string name = Path.GetFileName(fullpath);

            if (name == "")
                name = def;

            return name;
        }

        private void Event_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ExpandNode(e.Node);
        }

        public new void Initialize()
        {
            base.Initialize();
            Nodes.Clear();

            string fullpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            AddNode(Nodes, fullpath, GetName(fullpath, "Desktop"));

            fullpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            AddNode(Nodes, fullpath, GetName(fullpath, "My Documents"));

            fullpath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            AddNode(Nodes, fullpath, GetName(fullpath, "My Pictures"));

            string[] drives = Environment.GetLogicalDrives();
            foreach (string drive in drives)
                AddNode(Nodes, drive, drive);

            this.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.Event_BeforeExpand);
        }

        public void AddBaseNode(string path)
        {
            AddNode(Nodes, path, path);
        }

        public void RemoveBaseNode(string path)
        {
            foreach(TreeNode node in Nodes)
                if (node.Name==path)
                    node.Remove();
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (!IsEditing)
                base.OnAfterCheck(e);
        }

        private TreeNode AddNode(TreeNodeCollection nodes, string path, string name)
        {
            TreeNode subnode = nodes.Add(path, name);
            subnode.Nodes.Add("", "");

            if (Exists(path))
            {
                IsEditing = true;

                subnode.ForeColor = Color.Red; 
                subnode.Checked = true;
                subnode.ToolTipText = ToolTipText(path);

                IsEditing = false;
            }

            if (Includes(path))
                subnode.ForeColor = Color.Red;

            return subnode;
        }

        private void FillTree(TreeNode node, string path, int level, int maxlevel)
        {
            if (!Directory.Exists(path))
                return;

            DirectoryInfo dirorg = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dirorg.GetDirectories();

            foreach (DirectoryInfo di in dirs)
            {
                // do not show Hidden System folders or Reparse Points (Junctions)
                if (((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) &&
                    ((di.Attributes & FileAttributes.System) == FileAttributes.System) ||
                    ((di.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint))
                    continue;

                TreeNode subnode = AddNode(node.Nodes, di.FullName, di.Name);

                if (maxlevel != 0 && level < maxlevel)
                    FillTree(subnode, di.FullName, ++level, maxlevel);
            }
        }

        private void RefreshNode(TreeNode node,bool recursive)
        {
            if (node.Name != "")
            {
                if (Exists(node.Name))
                {
                    node.ForeColor = Color.Red;
                    node.ToolTipText = ToolTipText(node.Name);
                }
                else
                    if (Includes(node.Name))
                        node.ForeColor = Color.Red;
                    else
                        node.ForeColor = Color.Black;

                if (recursive)
                    RefreshTreeNodes(node.Nodes);
            }
        }

        public void RefreshTreeNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
                RefreshNode(node,true);
        }

        public void RefreshTreeNodes(ArrayList nodes) //provided for compatibility with the TreeViewMS class
        {
            foreach (TreeNode node in nodes)
                RefreshNode(node,false);
        }

        public void ExpandNode(TreeNode node)
        {
            node.Nodes.Clear();
            FillTree(node, node.Name, 0, 0);
        }

        protected override void paintSelectedNodes()
        {
            base.paintSelectedNodes();
            RefreshTreeNodes(SelectedNodes);
        }

        protected override void removePaintFromNodes()
        {
            base.removePaintFromNodes();
            RefreshTreeNodes(SelectedNodes);
        }
    }
}
