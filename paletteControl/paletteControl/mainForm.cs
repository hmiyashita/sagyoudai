using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;

namespace paletteControl
{
    public partial class mainForm : Form
    {

        System.Drawing.Icon bricon;

        public mainForm()
        {
            InitializeComponent();
            bricon =  System.Drawing.Icon.ExtractAssociatedIcon(GetDefaultExePath(@"http\shell\open\command"));
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                // ドラッグ中のファイルやディレクトリの取得
                string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string d in drags)
                {
                    if (System.IO.File.Exists(d))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else if (System.IO.Directory.Exists(d))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }
            }
            else if(e.Data.GetDataPresent(DataFormats.Text))
            {
                // ドラッグ中のファイルやディレクトリの取得
                string link = (string)e.Data.GetData(DataFormats.Text);
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            List<LinkDataClass> ldc = new List<LinkDataClass>();

            // ドラッグ中のファイルやディレクトリの取得
            string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);
            System.Drawing.Icon myicon;
            if (drags != null)
            {
                foreach (string tmp in drags)
                {
                    if (System.IO.Directory.Exists(tmp))
                    {

                        SHFILEINFO shinfo = new SHFILEINFO();
                         //Use this to get the small Icon
                        IntPtr hImgSmall = Win32.SHGetFileInfo(tmp, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                        myicon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
                    }
                    else
                    {
                        myicon = System.Drawing.Icon.ExtractAssociatedIcon(tmp);
                    }
                    ldc.Add(new LinkDataClass(tmp, myicon,LinkDataClass.LType.EXE));
                }
            }
            else
            {
                string tmp = (string)e.Data.GetData(DataFormats.UnicodeText);
                if (tmp.StartsWith("http://") || tmp.StartsWith("https://"))
                {
                    ldc.Add(new LinkDataClass(tmp, bricon,LinkDataClass.LType.LINK));
                }
                else
                {
                    ldc.Add(new LinkDataClass(tmp, SystemIcons.Information,LinkDataClass.LType.TEXT));
                }
            }


            LinkControl lc = new LinkControl(ldc[0]);
            lc.Name = Guid.NewGuid().ToString();

            this.flowLayoutPanel1.Controls.Add(lc);            
        }

        private string GetDefaultExePath(string keyPath)
        {
            string path = "";

            // レジストリ・キーを開く
            // 「HKEY_CLASSES_ROOT\xxxxx\shell\open\command」
            RegistryKey rKey = Registry.ClassesRoot.OpenSubKey(keyPath);
            if (rKey != null)
            {
                // レジストリの値を取得する
                string command = (string)rKey.GetValue(String.Empty);
                if (command == null)
                {
                    return path;
                }

                // 前後の余白を削る
                command = command.Trim();
                if (command.Length == 0)
                {
                    return path;
                }

                // 「"」で始まる長いパス形式かどうかで処理を分ける
                if (command[0] == '"')
                {
                    // 「"～"」間の文字列を抽出
                    int endIndex = command.IndexOf('"', 1);
                    if (endIndex != -1)
                    {
                        // 抽出開始を「1」ずらす分、長さも「1」引く
                        path = command.Substring(1, endIndex - 1);
                    }
                }
                else
                {
                    // 「（先頭）～（スペース）」間の文字列を抽出
                    int endIndex = command.IndexOf(' ');
                    if (endIndex != -1)
                    {
                        path = command.Substring(0, endIndex);
                    }
                    else
                    {
                        path = command;
                    }
                }
            }

            return path;
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            Console.Write("test2");
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            Console.Write("test1");
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (test())
            {
                Console.WriteLine("naka");
            }
            else
            {
                Console.WriteLine("soto");
            }
        }

        private bool test()
        {
            return this.ClientRectangle.Contains(this.PointToClient(Cursor.Position));
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                DataContractSerializer ser = new DataContractSerializer(typeof(ArrayLinkDataClass));
                string fileName = openFileDialog1.FileName;
                XmlReader xr = XmlReader.Create(fileName);
                ArrayLinkDataClass t;
                try
                {
                    t = (ArrayLinkDataClass) ser.ReadObject(xr);
                }catch(Exception ex){
                    MessageBox.Show("ファイルの読み込みに失敗しました。", "読み込み", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                xr.Close();

                //削除
                foreach (Control c in  this.flowLayoutPanel1.Controls){
                    c.Dispose();
                }
                foreach (LinkDataClass f in t.list)
                {
                    
                    if (f.MyType == LinkDataClass.LType.LINK)
                    {
                        f.MyIcon = bricon;
                    }
                    else if (f.MyType == LinkDataClass.LType.EXE)
                    {
                        if (System.IO.Directory.Exists(f.MyLink))
                        {

                            SHFILEINFO shinfo = new SHFILEINFO();
                            //Use this to get the small Icon
                            IntPtr hImgSmall = Win32.SHGetFileInfo(f.MyLink, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                            f.MyIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
                        }
                        else
                        {
                            if (System.IO.File.Exists(f.MyLink))
                            {
                                f.MyIcon = System.Drawing.Icon.ExtractAssociatedIcon(f.MyLink);
                            }
                            else
                            {
                                f.MyIcon = SystemIcons.Information;
                            }
                        }
                    }
                    else
                    {
                        f.MyIcon = SystemIcons.Information;
                    }
                    LinkControl lc = new LinkControl(f);
                    lc.Name = Guid.NewGuid().ToString();
                    this.flowLayoutPanel1.Controls.Add(lc);           
                }                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                //List<LinkDataClass> links = new List<LinkDataClass>();
                System.Collections.ArrayList f = new System.Collections.ArrayList();
                foreach (Control t in this.flowLayoutPanel1.Controls)
                {
                    if (t is LinkControl)
                    {
                        f.Add(((LinkControl)t).GetValues());
                    }
                    
                }
                DataContractSerializer ser = new DataContractSerializer(typeof(ArrayLinkDataClass));

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new System.Text.UTF8Encoding(false);

                try
                {

                    XmlWriter xw = XmlWriter.Create(fileName, settings);

                    ser.WriteObject(xw, new ArrayLinkDataClass(f));
                    xw.Close();
                }catch(Exception ex){
                    MessageBox.Show("ファイルの書き込みに失敗しました。", "書きこみ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }
        [DataContract]
        [KnownType(typeof(LinkDataClass))]
        public class ArrayLinkDataClass
        {
            [DataMember]
            public System.Collections.ArrayList list;
            public ArrayLinkDataClass(System.Collections.ArrayList data)
            {
                list = data;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("閉じますがよろしいですか？", "閉じる", MessageBoxButtons.OKCancel,MessageBoxIcon.Information) == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Control factive = this.ActiveControl;
            if (factive is LinkControl)
            {
                LinkControl uactive = (LinkControl)factive;
                if (uactive.ActiveControl is TextBox)
                {
                    return;
                }
            }
            if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.V)
            {
                Console.WriteLine("key");
                IDataObject data = Clipboard.GetDataObject();
                string str = (string)data.GetData(DataFormats.Text);
                if (! string.IsNullOrEmpty(str))
                {
                    
                    LinkControl lc = new LinkControl(new LinkDataClass(str, SystemIcons.Information, LinkDataClass.LType.TEXT));
                    lc.Name = Guid.NewGuid().ToString();
                    this.flowLayoutPanel1.Controls.Add(lc);    
                }
            }
            this.Focus();
        }

        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.menuStrip1.Focus();
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };

    class Win32
    {
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1; // 'Small icon

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
    }
}

