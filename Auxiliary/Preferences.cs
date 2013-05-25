using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FlickrSync
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            comboBoxMethod.SelectedIndex = (int) SyncFolder.StringToMethod(Properties.Settings.Default.Method);
            comboBoxOrderType.SelectedIndex = (int)SyncFolder.StringToOrderType(Properties.Settings.Default.OrderType);
            checkBoxNoDelete.Checked = Properties.Settings.Default.NoDelete;
            checkBoxNoDeleteTags.Checked = Properties.Settings.Default.NoDeleteTags;
            checkBoxShowThumbnailImages.Checked = Properties.Settings.Default.UseThumbnailImages;

            checkBoxUseProxy.Checked = Properties.Settings.Default.ProxyUse;
            SetProxyState();
            textBoxProxyHost.Text = Properties.Settings.Default.ProxyHost;
            textBoxProxyPort.Text = Properties.Settings.Default.ProxyPort;
            textBoxProxyUser.Text = Properties.Settings.Default.ProxyUser;
            textBoxProxyPass.Text = Properties.Settings.Default.ProxyPass;

            comboBoxMsgLevel.SelectedIndex=(int) FlickrSync.StringToMsgLevel(Properties.Settings.Default.MessageLevel);
            comboBoxLogLevel.SelectedIndex = (int)FlickrSync.StringToLogLevel(Properties.Settings.Default.LogLevel);
            labelLogFile.Text=Properties.Settings.Default.LogFile;

            buttonLogFile.Visible = (((FlickrSync.LogLevel) comboBoxLogLevel.SelectedIndex)!=FlickrSync.LogLevel.LogNone);
            labelLogFile.Visible = (((FlickrSync.LogLevel) comboBoxLogLevel.SelectedIndex)!=FlickrSync.LogLevel.LogNone);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if ((FlickrSync.LogLevel)comboBoxLogLevel.SelectedIndex == FlickrSync.LogLevel.LogNone &&
                (FlickrSync.MessagesLevel)comboBoxMsgLevel.SelectedIndex == FlickrSync.MessagesLevel.MessagesNone)
            {
                if (MessageBox.Show("It is highly recommended that you keep some type of information, either on application messages or on a log file. Are you sure you want to disable both?", "Confirmation", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                    return;
            }

            Properties.Settings.Default.Method = ((SyncFolder.Methods)comboBoxMethod.SelectedIndex).ToString();
            Properties.Settings.Default.OrderType = ((SyncFolder.OrderTypes)comboBoxOrderType.SelectedIndex).ToString();
            Properties.Settings.Default.NoDelete = checkBoxNoDelete.Checked;
            Properties.Settings.Default.NoDeleteTags = checkBoxNoDeleteTags.Checked;
            Properties.Settings.Default.UseThumbnailImages = this.checkBoxShowThumbnailImages.Checked;
            Properties.Settings.Default.ProxyUse = checkBoxUseProxy.Checked;
            Properties.Settings.Default.ProxyHost = textBoxProxyHost.Text;
            Properties.Settings.Default.ProxyPort = textBoxProxyPort.Text;
            Properties.Settings.Default.ProxyUser = textBoxProxyUser.Text;
            Properties.Settings.Default.ProxyPass = textBoxProxyPass.Text;
            Properties.Settings.Default.MessageLevel = ((FlickrSync.MessagesLevel)comboBoxMsgLevel.SelectedIndex).ToString();
            Properties.Settings.Default.LogLevel = ((FlickrSync.LogLevel)comboBoxLogLevel.SelectedIndex).ToString();
            Properties.Settings.Default.LogFile = labelLogFile.Text;
            Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetProxyState()
        {
            textBoxProxyHost.Enabled = checkBoxUseProxy.Checked;
            textBoxProxyPass.Enabled = checkBoxUseProxy.Checked;
            textBoxProxyPort.Enabled = checkBoxUseProxy.Checked;
            textBoxProxyUser.Enabled = checkBoxUseProxy.Checked;
            labelPassword.Enabled = checkBoxUseProxy.Checked;
            labelPort.Enabled = checkBoxUseProxy.Checked;
            labelUser.Enabled = checkBoxUseProxy.Checked;
        }

        private void checkBoxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            SetProxyState();
        }

        private void comboBoxLogLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonLogFile.Visible = (((FlickrSync.LogLevel)comboBoxLogLevel.SelectedIndex) != FlickrSync.LogLevel.LogNone);
            labelLogFile.Visible = (((FlickrSync.LogLevel)comboBoxLogLevel.SelectedIndex) != FlickrSync.LogLevel.LogNone);
        }

        private void buttonLogFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.CheckPathExists = true;
            fd.OverwritePrompt = true;
            fd.AddExtension = true;
            fd.DefaultExt = "txt";
            fd.Filter = "Text file (*.txt)|*.txt";

            if (fd.ShowDialog() == DialogResult.OK)
                labelLogFile.Text=fd.FileName;
        }
    }
}