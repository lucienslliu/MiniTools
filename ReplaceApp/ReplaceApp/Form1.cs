using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ReplaceApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // 在此添加代码,选择的路径为
                String text = folderBrowserDialog1.SelectedPath;
                textBox1.Text = text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String dir = textBox1.Text;
            DirectoryInfo theFolder = new DirectoryInfo(dir);
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();

            this.listBox1.Items.Clear();

            FileInfo[] fileInfo = theFolder.GetFiles();
            foreach (FileInfo NextFile in fileInfo)
            {
                //遍历文件
                String PathName = dir + "\\" + NextFile.Name;
                this.listBox1.Items.Add(PathName);
            }

            if (this.checkBox1.Checked == true)
            {
                //遍历文件夹
                foreach (DirectoryInfo NextFolder in dirInfo)
                {
                    FileInfo[] ChildfileInfo = NextFolder.GetFiles();
                    foreach (FileInfo NextFile in ChildfileInfo)
                    {
                        //遍历文件
                        String PathName = dir + "\\" + NextFolder.Name + "\\" + NextFile.Name;
                        this.listBox1.Items.Add(PathName);
                    }
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Length <= 0 ||
                this.textBox3.Text.Length <= 0)
            {
                return;
            }

            foreach (object obj in this.listBox1.Items)
            {
                String PathName = obj.ToString();

                // 替换
                String text = String.Empty;
                using (FileStream fs = new FileStream(PathName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        text = sr.ReadToEnd();
                        text = text.Replace(this.textBox2.Text, this.textBox3.Text);
                    }
                }

                using (FileStream wfs = new FileStream(PathName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(wfs))
                    {
                        sw.Write(text);
                    }
                }
            }

            MessageBox.Show("Replace Completed!");
        }
    }
}
