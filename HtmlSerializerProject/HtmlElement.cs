using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace HtmlSerializerProject
{

    internal class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement(string name, HtmlElement parent)
        {
            Name = name;
            Attributes = new List<string>();
            Classes = new List<string>();
            InnerHtml = "";
            Parent = parent;
            Children = new List<HtmlElement>();
        }

        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            InnerHtml = "";
            Children = new List<HtmlElement>();
        }

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> q = new Queue<HtmlElement>();
            q.Enqueue(this);
            while (q.Count > 0)
            {
                HtmlElement el = q.Dequeue();
                yield return el;
                foreach (HtmlElement child in el.Children)
                    q.Enqueue(child);

            }
        }


        public IEnumerable<HtmlElement> Ancestors()
        {

            HtmlElement parent = this.Parent;
            while (parent != null)
            {
                yield return parent;
                parent = parent.Parent;
            }


        }

        public override string? ToString()
        {
            string print = "< ";
            print += Name + " ";
            if (Id != null) print += "id= " + Id + " ";
            if (Classes.Count > 0)
            {
                print += "class= ";
                foreach (string c in Classes)
                    print += c + " ";
            }
            //if (Attributes.Count > 0)
            //{
            //    print += "attributes: ";
            //    foreach (string a in Attributes)
            //        print += a + " ";
            //}

            print += " >";
            return print;
        }



    }
}
