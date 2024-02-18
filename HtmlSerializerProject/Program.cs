// See https://aka.ms/new-console-template for more information
using HtmlSerializerProject;
using System;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

HtmlSerializer serializer = new HtmlSerializer(); var html = await serializer.Load("https://learn.malkabruk.co.il/practicode/home");

Console.WriteLine("***************THE_HTML_TREE_IS:***************");
var dom = serializer.Serialize(html);

Console.WriteLine("***************SELECT_1:***************");
var result1 = dom.Query(Selector.ConvertQuery("body div li.md-tabs__item.md-tabs__item--active"));
result1.ToList().ForEach(x => Console.WriteLine(x));

Console.WriteLine("***************SELECT_2:***************");
var result2 = dom.Query(Selector.ConvertQuery("#__drawer.md-toggle"));
result2.ToList().ForEach(x => Console.WriteLine(x));

Console.WriteLine("***************SELECT_3:***************");
var result3 = dom.Query(Selector.ConvertQuery(".md-tabs__item"));
result3.ToList().ForEach(x => Console.WriteLine(x));
//include checking by $$(".md-tabs__item") at the orginal page!
Console.WriteLine("***************SELECT_4:***************");
var result4 = dom.Query(Selector.ConvertQuery("span"));
result4.ToList().ForEach(x => Console.WriteLine(x));
Console.WriteLine("***************SELECT_5:***************");
var result5 = dom.Query(Selector.ConvertQuery(" span"));
result5.ToList().ForEach(x => Console.WriteLine(x));

Console.WriteLine("***************SELECT_6:***************");
var result6 = dom.Query(Selector.ConvertQuery("kkk"));
result6.ToList().ForEach(x => Console.WriteLine(x));

Console.ReadLine();