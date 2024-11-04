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

        // десериализуем объект
        using (FileStream fs = new FileStream("shivotnoer.xml", FileMode.OpenOrCreate))
        {
            Lion? levaaaaarr = xmlSerializer.Deserialize(fs) as Lion;
            Console.WriteLine($"Name: {levaaaaarr?.Name}  Country: {levaaaaarr?.Country} Hide: {levaaaaarr?.HideFromOtherAnimals} WhatAnimeal: Animal : {levaaaaarr?.WhatAnimal}");
        }

    }
}

