using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FlickrSync
{
    public delegate bool ButtonCall(int id);

    public partial class CustomMsgBox : Form
    {
        ArrayList buttons;
        string title;
        string text;

        public CustomMsgBox(string ptext,string ptitle)
        {
            InitializeComponent();

            buttons=new ArrayList();
            title = ptitle;
            text = ptext;
        }

        public void AddButton(string Text, int Width, int id, ButtonCall call)
        {
            buttons.Add(new CustomButton(Text, Width, id,call));
        }

        private void CustomMsgBox_Load(object sender, EventArgs e)
        {
            labelMessage.Text = text;
            this.Text = title;

            int start = -20;
            foreach (CustomButton cbtn in buttons)
                start += cbtn.width + 20;
            start = Size.Width / 2 - start / 2;

            foreach (CustomButton cbtn in buttons)
            {
                Button btn = new Button();
                btn.Text = cbtn.text;
                btn.Location = new System.Drawing.Point(start, labelMessage.Location.Y+labelMessage.Size.Height+12);
                btn.Size = new System.Drawing.Size(cbtn.width, 23);
                btn.Click += new System.EventHandler(button_Click);

                this.Controls.Add(btn);
                start += cbtn.width + 20;
            }
        }

        public bool ButtonCallOK(int id)
        {
            return true;
        }

        private void button_Click(object sender, EventArgs e)
        {
            foreach (CustomButton cbtn in buttons)
                if (cbtn.text == ((Button) sender).Text)
                    if (cbtn.call(cbtn.ID))
                        Dispose();
        }
    }

    public class CustomButton
    {
        public string text;
        public int width;
        public int ID;
        public ButtonCall call;

        public CustomButton(string ptext, int pwidth, int pid, ButtonCall pcall)
        {
            text = ptext;
            width = pwidth;
            ID = pid;
            call = pcall;
        }
    }
}
