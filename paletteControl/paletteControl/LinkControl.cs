using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace paletteControl
{
    public partial class LinkControl : UserControl
    {
        private LinkDataClass myldc;
        public LinkControl(LinkDataClass ldc)
        {
            InitializeComponent();
            this.myldc = ldc;
            this.ExecuteButton.Image = ldc.MyIcon.ToBitmap();
            this.DisposeButton.Image = SystemIcons.Error.ToBitmap();
            this.textBox1.Text = ldc.Execute;
            this.toolTip1.SetToolTip(this.ExecuteButton, ldc.MyLink);
            this.toolTip1.SetToolTip(this.DisposeButton, "削除");

            if (ldc.MyType == LinkDataClass.LType.EXE)
            {
                this.label1.Text = System.IO.Path.GetFileName(ldc.MyLink);
            }
            else if (ldc.MyType == LinkDataClass.LType.LINK)
            {
                string url = ldc.MyLink.Split('/')[ldc.MyLink.Split('/').Length - 1];
                string[] param = url.Split('?');
                this.label1.Text = param[0];
            }
            else
            {
                if (ldc.MyLink.Length > 10)
                {
                    this.label1.Text = ldc.MyLink.Substring(0, 10);
                }
                else
                {
                    this.label1.Text = ldc.MyLink;
                }
            }
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            if (this.myldc.MyType == LinkDataClass.LType.EXE)
            {
                if (this.textBox1.Text == "")
                {
                    try
                    {
                        System.Diagnostics.Process.Start(this.myldc.MyLink);
                    }catch(Exception ex){
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    string buf = this.textBox1.Text.Trim();
                    int indx;
                    if ( buf.StartsWith(@""""))
                    {
                        indx = buf.TrimStart('"').IndexOf('"');
                        //trimした分
                        indx++;
                    }else{
                        indx = buf.IndexOf(" ");
                    }
                    string prog = "";
                    string arg = "";
                    if (indx > 0)
                    {
                        indx++;
                        prog = buf.Substring(0,indx);
                        arg = string.Format(buf.Substring(indx), myldc.MyLink);
                    }
                    else
                    {
                        prog = this.textBox1.Text;
                        arg = this.myldc.MyLink;
                    }
                    
                    System.Diagnostics.Process.Start(prog, arg);
                }
            }
            else if (this.myldc.MyType == LinkDataClass.LType.LINK)
            {
                System.Diagnostics.Process.Start(this.myldc.MyLink);
            }
            else
            {
                Clipboard.SetDataObject(this.myldc.MyLink, true);
            }
        }

        private void DisposeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public LinkDataClass GetValues()
        {
            return new LinkDataClass(this.myldc.MyLink, this.myldc.MyIcon, this.myldc.MyType,this.textBox1.Text);
        }
    }
}
