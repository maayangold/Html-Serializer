using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlSerializerProject
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; } =
        new List<string>();
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public Selector(string tagName, string id, List<string> classes)
        {
            TagName = tagName;
            Id = id;
            Classes = classes;
        }
        public Selector()
        {

        }
        public static Selector ConvertQuery(string query)
        {
            Selector rootSelector = new Selector();
            Selector selector = rootSelector;
            //Check if the query starts with a space
            if (query.StartsWith(" "))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("The space at the beginning of the query has been replaced with 'html'...");
                Console.ForegroundColor = ConsoleColor.White;
                int spaceIndex = query.IndexOf(" ");
                query = query.Substring(0, spaceIndex) + "html " + query.Substring(spaceIndex + 1);


            }

            string[] inners = query.Split(' ');
            foreach (var inner in inners)
            {

                string[] selectors = new Regex("(?=[#\\.])").Split(inner).Where(s => s.Length > 0).ToArray();
                foreach (string s in selectors)
                {
                    if (s.StartsWith("#"))
                        selector.Id = s.Substring(1);
                    else if (s.StartsWith("."))
                        selector.Classes.Add(s.Substring(1));
                    else if (HtmlHelper.Helper.HtmlTags.Contains(s))
                    {
                        selector.TagName = s;

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("NOT EXIST...");
                        Console.ForegroundColor = ConsoleColor.White;
                        return null;

                    }

                }
                selector.Child = new Selector();
                selector.Child.Parent = selector;
                selector = selector.Child;
            }
            selector.Parent.Child = null;
            return rootSelector;

        }
        public override string ToString()
        {
            string print = "selector: ";
            if (TagName != null) print += "TagName " + TagName + " ";
            if (Id != null) print += "id= " + Id + " ";
            if (Classes.Count > 0)
            {
                print += " classes: ";
                foreach (var c in Classes)
                    print += c + " ";
            }
            print += " children: ";
            Selector selector = this;
            while (selector.Child != null)
            {

                print += selector.Child.TagName + " ";
                selector = selector.Child;
            }


            return print;
        }
    }
}
