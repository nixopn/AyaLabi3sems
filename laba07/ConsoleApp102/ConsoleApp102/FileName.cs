using System;
using System.Reflection;
using System.Xml.Linq;
using ClassLibrary1;

class Programm
{
    public static void Main()
    {
        Lion leva = new Lion("aaaa", false, "leva");
        Console.WriteLine(leva.Country);
        leva.SayHello();

        
        var root = new XElement("Classes");






        foreach (var cls in Assembly.LoadFrom("ClassLibrary1.dll").GetTypes().Where(t => t.IsClass))
        {
            var commentAttr = cls.GetCustomAttribute<Commentatrobiuterter>();
            var comment = commentAttr?.comment ?? "Нет комментария";

            var classElement = new XElement("Class",
                new XAttribute("Name", cls.Name),
                new XAttribute("Comment", comment));

            root.Add(classElement);
        }

        var xmlFilePath = "Classes.xml";
        root.Save(xmlFilePath);

        Console.WriteLine($"XML файл сохранен: {xmlFilePath}");
    }
}

