using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializerProject
{
    internal static class ExtensionFunctions
    {
        static int Counter = 0;
        public static IEnumerable<HtmlElement> Query(this HtmlElement element, Selector selector)
        {

            Counter = 0;
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();
            if (selector != null)
                foreach (HtmlElement el in element.Descendants())
                {
                    ExtentionQuery(el, selector, result);
                }
            Console.WriteLine($"there is {result.Count()} elements");
            return result;
        }
        //extention function
        public static void ExtentionQuery(HtmlElement el, Selector selector, HashSet<HtmlElement> result)
        {

            if (!((selector.TagName == null || el.Name.Equals(selector.TagName)) &&
                (selector.Id == null || selector.Id.Equals(el.Id)) &&
                (el.Classes.Intersect(selector.Classes).Count() == selector.Classes.Count())))
                return;

            if (selector.Child == null)
            {
                result.Add(el);
                Counter++;

            }

            else

                foreach (var child in el.Descendants())
                {
                    ExtentionQuery(child, selector.Child, result);
                }

        }

    }
}
