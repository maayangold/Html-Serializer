using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.Serialization
    ;//for Deserialize function
namespace HtmlSerializerProject
{
    internal class HtmlHelper
    {
        private static readonly HtmlHelper _helper = new HtmlHelper();
        public static HtmlHelper Helper => _helper;

        public string[] HtmlTags { get; set; }
        public string[] SelfClosingTags { get; set; }

        private HtmlHelper()
        {
            HtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Tags\\HtmlTags.json"));
            SelfClosingTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Tags\\HtmlSelfCloseTags.json"));
        }
    }
}
