using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace base26Rename
{
    public partial class Form1 : Form
    {
        public char[] numBase = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public OpenFileDialog ofd = new OpenFileDialog();
        public FolderBrowserDialog fb = new FolderBrowserDialog();
        public string outDir = string.Empty;
        public List<FileInfo> files = new List<FileInfo>();
        public Form1()
        {
            ofd.Multiselect = true;
            InitializeComponent();
        }
        public static string IntToString(int value, char[] baseChars)
        {
            int temp = value;
            string result = string.Empty;
            int targetBase = baseChars.Length;

            do
            {
                result = baseChars[value % targetBase] + result;
                value = value / targetBase;
            }
            while (value > 0);
            if(temp < 26) return "a"+result;
            else return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (files.Count == 0) { MessageBox.Show("Please select atleast one file", "No files selected"); return; }
            if (outDir == string.Empty) { MessageBox.Show("Please select output directory", "No output directory selected"); return; }
            foreach(FileInfo file in files) 
            {
                int num = 0;
                try 
                {
                    num=Convert.ToInt32(Path.GetFileNameWithoutExtension(file.FullName));
                }
                catch 
                {
                    MessageBox.Show("Invaild file name " + file, "Invalid file");
                    return;
                }
                try { File.Copy(file.FullName, outDir + @"\" + IntToString(num-1, numBase) + file.Extension); }
                catch { }
            }
            MessageBox.Show("Done!", "Finished");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fb.ShowDialog() == DialogResult.OK) 
            {
                outDir = fb.SelectedPath;
                label1.Text = outDir;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                files.Clear();
                foreach (string fName in ofd.FileNames)
                {
                    files.Add(new FileInfo(fName));
                }
            }
        }
    }
}
