using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YourNamespace // Замените на ваше пространство имен
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

    public class Program
    {
        static async Task Main()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            using Socket tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                tcpListener.Bind(ipPoint);
                tcpListener.Listen();
                Console.WriteLine("Сервер запущен. Ожидание подключений... ");

                while (true)
                {
                    // получаем входящее подключение
                    using var tcpClient = await tcpListener.AcceptAsync();

                    var buffer = new List<byte>();
                    // буфер для считывания одного байта
                    var bytesRead = new byte[1];
                    // считываем данные до конечного символа
                    while (true)
                    {
                        var count = await tcpClient.ReceiveAsync(bytesRead);
                        // смотрим, если считанный байт представляет конечный символ, выходим
                        if (count == 0 || bytesRead[0] == '\n') break;
                        // иначе добавляем в буфер
                        buffer.Add(bytesRead[0]);
                    }

                    var tickrr = Encoding.UTF8.GetString(buffer.ToArray());
                    Console.WriteLine($"Получено сообщение: {tickrr}");
                    string? message;
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var prid = db.Tickers.FirstOrDefault(t => t.ticker == tickrr);
                        var todaystate = db.Prices.FirstOrDefault(t => t.Id == prid.Id);
                        Console.WriteLine(todaystate.price);
                        message = todaystate.price.ToString();
                    }

                    // определяем данные для отправки - текущее время
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await tcpClient.SendAsync(data);
                    Console.WriteLine($"Клиенту {tcpClient.RemoteEndPoint} отправлены данные");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}