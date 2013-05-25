using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FlickrSync
{
    public class FolderBrowser : FolderNameEditor
    {
        private FolderNameEditor.FolderBrowser m_obBrowser = null;
        private string m_strDescription;
        public FolderBrowser()
        {
            m_strDescription = "Select path";
            m_obBrowser = new FolderNameEditor.FolderBrowser();
        }

        public string DirectoryPath
        {
            get { return this.m_obBrowser.DirectoryPath; }
        }

        public DialogResult ShowDialog()
        {
            m_obBrowser.Description = m_strDescription;
            return m_obBrowser.ShowDialog();
        }

        public void SetNetworkSelect()
        {
            m_obBrowser.Style = FolderBrowserStyles.ShowTextBox;
            m_obBrowser.StartLocation = FolderBrowserFolder.NetworkNeighborhood;
        }

        public void SetDescription(string pDesc)
        {
            m_strDescription = pDesc;
        }
    }
}
