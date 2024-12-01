using System.Net.Http.Json;
using WebAPIModels.Models;

string uri = $"https://localhost:7026/api/Customers";
HttpClient client = new HttpClient();

var chapter = await client.GetFromJsonAsync<IEnumerable<Employee>>(uri);
if (chapter is not null)
{
    foreach(var c in chapter)
    {
        Console.WriteLine($"{c.City}: {c.TitleOfCourtesy} : {c.Country} ");
    }
}

Employee customer = new Employee()
{
    Address = "Russia",
    City = "Moscow",
    Country = "RU",
    PostalCode = "451042",
    Region = "Rus"
};
var result = await client.PostAsJsonAsync<Employee>(uri, customer);

Console.ReadLine();



