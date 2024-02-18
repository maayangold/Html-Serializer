using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace HtmlSerializerProject
{
    internal class HtmlSerializer
    {
        public async Task<string> Load(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var html = await response.Content.ReadAsStringAsync();
            return html;
        }
        public HtmlElement Serialize(string html)
        {
            var compressedHtml = Regex.Replace(Regex.Replace(html, @" {2,}", ""), @"[\t\r\n]", "");
            string[] htmlLines = new Regex("<(.*?)>").Split(compressedHtml).Where(line => line.Length > 0).ToArray();
            HtmlElement tree = new HtmlElement();
            if (htmlLines[1].Split(' ')[0].Contains("html"))
            {
                tree = CreateTree(htmlLines);
            }
            else
                Console.WriteLine("Error: invalid web address...");
            Console.WriteLine("YAY!!! BARUCH HASHEM!");
            return tree;

        }


        public HtmlElement CreateTree(string[] htmlLines)
        {
            HtmlElement root = CreateHtmlElement(htmlLines[1], "html", null);

            foreach (var htmlLine in htmlLines)
            {
                if (htmlLine == htmlLines[0] || htmlLine == htmlLines[1])
                    continue;
                string tagName = htmlLine.Split(' ')[0];

                if (tagName == "/html")
                {
                    break;
                }
                if (tagName.StartsWith("/"))
                {
                    root = root.Parent;
                    continue;
                }
                //inner html
                if (!HtmlHelper.Helper.HtmlTags.Contains(tagName))
                {
                    if (root != null)
                        root.InnerHtml += htmlLine;
                    continue;

                }

                HtmlElement child = CreateHtmlElement(htmlLine, tagName, root);


                root.Children.Add(child);

                //אם זו תגית סיגרה עצמית היא אח לתגית הבאה באיטרציה ולא אבא

                if (!HtmlHelper.Helper.SelfClosingTags.Contains(tagName) && !htmlLine.EndsWith('/'))
                    root = child;


            }
            return root;
        }

        public HtmlElement CreateHtmlElement(string htmlLine, string name, HtmlElement parent)
        {
            HtmlElement el = new HtmlElement(name, parent);
            //var matches = Regex.Matches(htmlLine, @"(\w+)='([^']+)'");

            MatchCollection matches = Regex.Matches(htmlLine, @"(\S+?)\s*=\s*""([^""]*?)""");

            foreach (Match match in matches)
            {
                string attributeName = match.Groups[1].Value;
                string attributeValue = match.Groups[2].Value;

                switch (attributeName)
                {
                    case "class":
                        el.Classes.AddRange(attributeValue.Split(' '));
                        break;
                    case "id":
                        el.Id = attributeValue;
                        break;
                    default:
                        el.Attributes.Add(attributeName + "=" + attributeValue);
                        break;
                }

            }
            Console.WriteLine(el);
            return el;
        }



    }




}
