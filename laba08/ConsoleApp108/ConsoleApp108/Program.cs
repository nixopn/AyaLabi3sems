using System;

using System;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using ClassLibrary1;

class Programm
{
    public static void Main()
    {
        Lion leva = new Lion("aaaa", false, "leva");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Lion));
        using(TextWriter pisakaae = new StreamWriter("shivotnoer.xml"))
        {
            xmlSerializer.Serialize(pisakaae, leva);
        }
        Console.WriteLine("Сериализация завершена. Объект сохранен в animal.xml.");
    }
}

