using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ezWeb
{
    class HtmlHandler
    {
        public string homeDir;
        public string elementType = "p";

        public HtmlHandler(string HomeDir)
        {
            homeDir = HomeDir;
        }

        public string formatElement(string text)
        {
            string elem = String.Format("<{0}>" + text + "</{0}>", elementType);
            if (elementType == "hr")
            {
                elem = "<hr>";
            }
            else if (elementType == "nav")
            {
                string links = "";
                links += "<a class=\"nav-item nav-link\" href=\"#\">Features</a>";
                elem = String.Format("<nav class=\"navbar navbar-expand-lg navbar-light bg-light\">\r\n  <a class=\"navbar-brand\" href=\"#\">Navbar</a>\r\n  <button class=\"navbar-toggler\" type=\"button\" data-toggle=\"collapse\" data-target=\"#navbarNavAltMarkup\" aria-controls=\"navbarNavAltMarkup\" aria-expanded=\"false\" aria-label=\"Toggle navigation\">\r\n    <span class=\"navbar-toggler-icon\"></span>\r\n  </button>\r\n  <div class=\"collapse navbar-collapse\" id=\"navbarNavAltMarkup\">\r\n    <div class=\"navbar-nav\">\r\n      {0}\r\n</div>\r\n  </div>\r\n</nav>", links);
            }
            else if (elementType == "custom")
            {
                elem = text;
            }
            else if (elementType == "container")
            {
                elem = text;
            }
            return elem;
        }

        public void addElement(string element)
        {
            using (FileStream fs = new FileStream(homeDir + "/index.html", FileMode.Append))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    string elem = formatElement(element);
                    w.WriteLine(elem);
                }
            }
        }

        public void lineChanger(ListBox.SelectedIndexCollection line_to_edit, bool remove = false, string newText = "")
        {
            List<string> arrLine = File.ReadAllLines(homeDir + "/index.html").ToList();
            if (remove)
            {
                if (line_to_edit.Count > 1)
                {
                    for (int i = line_to_edit.Count - 1; i >= 0; i--)
                    {
                        arrLine.RemoveAt(i);
                    }
                }
                else
                {
                    arrLine.RemoveAt(line_to_edit[0]);
                }
            }
            else
            {
                string elem = formatElement(newText);
                arrLine[line_to_edit[0]] = elem;
            }
            File.WriteAllLines(homeDir + "/index.html", arrLine);
        }

        public void insertAbove(int index, string text)
        {
            List<string> arrLine = File.ReadAllLines(homeDir + "/index.html").ToList();
            string elem = formatElement(text);
            arrLine.Insert(index, elem);
            File.WriteAllLines(homeDir + "/index.html", arrLine);
        }

        public void insertBelow(int index, string text)
        {
            List<string> arrLine = File.ReadAllLines(homeDir + "/index.html").ToList();
            string elem = formatElement(text);
            arrLine.Insert(index+1, elem);
            File.WriteAllLines(homeDir + "/index.html", arrLine);
        }

        public void setType(string type)
        {
            elementType = type;
        }

        public void createIndex(string args="")
        {
            if (args == "bootstrap")
            {
                string template = homeDir + @"/templates/bootstrap.txt";
                string[] template_text = System.IO.File.ReadAllLines(template);
                System.IO.File.WriteAllLines(homeDir + @"/index.html", template_text);
            }
            else
            {
                string template = homeDir + @"/templates/default.txt";
                string[] template_text = System.IO.File.ReadAllLines(template);
                System.IO.File.WriteAllLines(homeDir + @"/index.html", template_text);
            }
        }
    }
}
