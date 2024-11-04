using System;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Sqlite;
using SQLitePCL;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Timers;

namespace pobeda28
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Tickers> Tickers => Set<Tickers>();
        public DbSet<Prices> Prices => Set<Prices>();
        public DbSet<TodaysCondition> TodaysConditions => Set<TodaysCondition>();


        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp2848.db");
        }

    }
    public class TodaysCondition
    {
        public int Id { get; set; }
        public int tickerid { get; set; }
        
        public double state { get; set; }
    }

    public class Prices
    {
        public int Id { get; set; }
        public int tickerid { get; set; }
        public double price { get; set; }

        public string? date { get; set; }
    }

    public class Tickers
    {
        public int Id { get; set; }
        public string? ticker { get; set; }
    }

    public class Programm
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string apiKey = "T2g1U1dLMWJEV0d6WGVURXRGZG1qd2NPTWR3WjhMMVlWOFFMSzNHckFpQT0";
        static readonly Mutex mutex = new();
        public async static Task Main()
        {
            SQLitePCL.Batteries.Init();
            Tickers obj = new Tickers();

            List<string> tickers = [];

            using (StreamReader reader = new("ticker2848.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {//будет выполнять итерации до тех пор, пока ReadLine() не вернет null. Внутри цикла считываем каждую строку и добавляем ее в список tickers
                    tickers.Add(line);

                }


            }
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var tick in tickers)
                {
                    // Create a new Ticker object for each ticker
                    var newTicker = new Tickers { ticker = tick };
                    db.Tickers.Add(newTicker);
                    db.SaveChanges();
                }
            }
            Console.WriteLine("Считаны в базу данных тикеры");

            List<Task> tasks = [];
            int i = 1;
            foreach (string ticker in tickers)
            {

                tasks.Add(GetDataForTicker(ticker, i));
                System.Threading.Thread.Sleep(130);
                i++;
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("Считаны в базу");


            Console.WriteLine("Выберите ticker");
            string? tickrr;
            tickrr = Console.ReadLine();

            using (ApplicationContext db = new ApplicationContext())
            {
                var prid = db.Tickers.FirstOrDefault(t => t.ticker == tickrr);
                var aaaa = db.TodaysConditions.FirstOrDefault(t => t.Id == prid.Id);
                Console.WriteLine(aaaa.state);
            }
        }

        static async Task GetDataForTicker(string ticker, int i)
        {

            try
            {
                using HttpClient client = new();

                string url = $"https://api.marketdata.app/v1/stocks/candles/D/{ticker}/?from=2024-10-28&to=2024-11-03&token=RFU4aDItQlRLRjFuNDd5OVlMWGs4UGh6eXY5bldMWDRvS0xxOHctcmNLOD0";

                HttpResponseMessage response = await client.GetAsync(url);

                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic responceObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                double averagePrice = 0;
                if (responceObject != null && responceObject.t != null && responceObject.h != null && responceObject.l != null)
                {
                    List<long> timestamps = responceObject?.t?.ToObject<List<long>>() ?? new List<long>();
                    List<double> highs = responceObject?.h?.ToObject<List<double>>() ?? new List<double>();
                    List<double> lows = responceObject?.l?.ToObject<List<double>>() ?? new List<double>();
                    long tm = timestamps[timestamps.Count - 1];
                    DateTime startingDate = new DateTime(1970, 1, 1);
                    DateTime newDateTime = startingDate.AddSeconds(tm);
                    string dat = newDateTime.ToString();

                    if (timestamps.Count > 0) // Check for empty list
                    {
                        averagePrice += (highs[timestamps.Count - 1] + lows[timestamps.Count - 1]) / 2;
                        Console.WriteLine($"{i}, {averagePrice}, {tm} ");
                    }
                    else
                    {
                        Console.WriteLine($"No data found for ticker: {ticker}");
                    }
                    double y = (highs[timestamps.Count - 2] + lows[timestamps.Count - 2]) / 2;
                    double previousaveragePrice = averagePrice - y;
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var TodayCondition = new TodaysCondition
                        {
                            tickerid = i,
                            state = previousaveragePrice

                        };
                        db.TodaysConditions.Add(TodayCondition);
                        db.SaveChanges();

                        var newPrice = new Prices
                        {
                            tickerid = i,
                            price = averagePrice,
                            date = dat
                        };

                        db.Prices.Add(newPrice);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {ticker}: {ex.Message}");
            }

        }

        





                /*
                SQLitePCL.Batteries.Init();
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        TodaysCondition tb = new TodaysCondition() { tickerid = 2, state = "" };
                        Prices ps = new Prices();
                        Tickers ts = new Tickers();
                        db.TodaysConditions.Add(tb);
                        db.SaveChanges();
                        var users = db.TodaysConditions.ToList();
                        foreach (var user in users)
                        {
                            Console.WriteLine(user.Id);
                        }
                    }
                */































            }
}
