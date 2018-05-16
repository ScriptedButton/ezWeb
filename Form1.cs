using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ezWeb
{
    public partial class Form1 : Form
    {
        static string rootDir = Environment.CurrentDirectory;
        HtmlHandler handler = new HtmlHandler(rootDir);
        List <string> elementTypes = new List<string>{ "p", "h1", "hr", "nav", "custom", "container"};

        Uri uri = new Uri(rootDir + @"\index.html");

        public Form1()
        {
            InitializeComponent();
        }

        private void readFile()
        {
            string[] lines = System.IO.File.ReadAllLines(rootDir + "/index.html");

            listBox2.DataSource = lines;
            webBrowser1.Navigate(uri);
        }

        void createIndex()
        {
                if (checkBox1.Checked)
                {
                    handler.createIndex("bootstrap");
                }
                else
                {
                    handler.createIndex();
                }
        }

        public void handleInit()
        {
            if (!File.Exists(rootDir + @"\index.html"))
            {
                createIndex();
            }
            else
            {
                DialogResult prompt = MessageBox.Show("Index already exists, would you like to re-create?", "Notice", MessageBoxButtons.YesNo);
                if (prompt == DialogResult.Yes)
                {
                    createIndex();
                }
                else
                {
                    return;
                }
            }
            readFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(rootDir + @"\index.html"))
            {
                // handler.createIndex();
                MessageBox.Show("Desired index file not located, please initialize it using the Init File button.");
            }
            else
            {
                readFile();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                handler.setType(elementTypes[listBox1.SelectedIndex]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            handler.lineChanger(listBox2.SelectedIndices, true);
            readFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (handler.elementType == "container")
            {
                string toWrap = "";
                foreach (var i in listBox2.SelectedItems)
                {
                    toWrap += i.ToString() + "\n";
                }
                string formatted = String.Format("<div class=\"container\">\n{0}</div>", toWrap);
                handler.lineChanger(listBox2.SelectedIndices, true);
                handler.addElement(formatted);
                readFile();
            }
            else
            {
                handler.addElement(textBox1.Text);
                readFile();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            handleInit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            handler.lineChanger(listBox2.SelectedIndices, false, textBox1.Text);
            readFile();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            handler.insertAbove(listBox2.SelectedIndex, textBox1.Text);
            readFile();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            handler.insertBelow(listBox2.SelectedIndex, textBox1.Text);
            readFile();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore", rootDir + @"\index.html");
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                textBox1.Text = listBox2.SelectedItem.ToString();
            }
        }
    }
}
