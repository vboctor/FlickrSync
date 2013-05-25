using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FlickrSync
{
    /// <summary>
    /// SelectFromList is a Dialog to select an item from a list
    /// Pass an ArrayList of strings to the constructor and retrieve selected string on field Selected
    /// 
    /// Example:
    /// ArrayList list;
    /// list.Add("First element");
    /// list.Add("Second element");
    /// SelectFromList sel=new SelectFromList(list);
    /// sel.ShowDialog();
    /// MessageBox.Show(sel.Selected)
    /// </summary>
    public partial class SelectFromList : Form
    {
        ArrayList List;
        public string Selected="";

        public SelectFromList(ArrayList pList)
        {
            InitializeComponent();
            List = pList;
        }

        private void SelectFromList_Load(object sender, EventArgs e)
        {
            foreach (string str in List)
                listBoxList.Items.Add(str);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Selected = listBoxList.SelectedItem.ToString();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Selected = "";
            this.Close();
        }
    }
}