using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FlickrSync
{
    public partial class Login : Form
    {
        string user;
        string pass;

        public Login(string _user,string _pass)
        {
            InitializeComponent();
            textBoxUser.Text = _user;
            textBoxPass.Text = _pass;
        }

        public string GetUser()
        {
            return user;
        }

        public string GetPass()
        {
            return pass;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            user = textBoxUser.Text;
            pass = textBoxPass.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Preferences pref = new Preferences();
            if (pref.ShowDialog() == DialogResult.OK)
            {
                textBoxUser.Text = Properties.Settings.Default.ProxyUser;
                textBoxPass.Text = Properties.Settings.Default.ProxyPass;
            }
        }

    }
}