using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using Newtonsoft.Json;
using System.Threading;

// Абстрактный интерфейс наблюдателя
public interface Observer
{
    void Update(string filePath); 
}

// Конкретный наблюдатель
public class FileChangeObserver : Observer
{
    public void Update(string filePath)
    {
        Console.WriteLine($"File changed: {filePath}");
    }
}

// Наблюдаемый объект
public class FileSystemWatcher
{
    private readonly string _directory;
    private readonly List<Observer> _observers = new List<Observer>();
    private HashSet<string> _lastState;
    private readonly System.Timers.Timer _timer;

    public FileSystemWatcher(string directory)
    {
        _directory = directory;
        _lastState = GetDirectoryState();
        _timer = new System.Timers.Timer(400); 
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
    }

    private HashSet<string> GetDirectoryState()
    {
        var files = Directory.GetFiles(_directory);
        return new HashSet<string>(files);
    }

    public void Attach(Observer observer)
    {
        _observers.Add(observer);
    }

    public void Detach(Observer observer)
    {
        _observers.Remove(observer);
    }

    private void NotifyObservers(string filePath)
    {
        foreach (var observer in _observers)
        {
            observer.Update(filePath);
        }
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        var currentState = GetDirectoryState();
        var newFiles = currentState.Except(_lastState);
        var deletedFiles = _lastState.Except(currentState);

        foreach (var file in newFiles)
        {
            NotifyObservers(file);
        }

        foreach (var file in deletedFiles)
        {
            NotifyObservers(file);
        }

        _lastState = currentState;
    }
}

public interface ILoggerRepository : IDisposable
{
    void LogToFile(string message); // Запись в текстовый файл
    void LogToJson(string message); // Запись в JSON-файл
    void Save(); // Сохранение изменений (если необходимо)
}

public class FileLoggerRepository : ILoggerRepository
{
    private readonly string _filePath;

    public FileLoggerRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void LogToFile(string message)
    {
        using (StreamWriter writer = new StreamWriter(_filePath, true))
        {
            writer.WriteLine(message);
        }
    }

    public void LogToJson(string message)
    {
        // Не реализовано для текстового файла

    }



    public void Save()
    {
        // Нет необходимости сохранять, так как запись происходит сразу
    }

    public void Dispose()
    {
        // Ресурсы не требуют освобождения
    }
}

public class JsonLoggerRepository : ILoggerRepository
{
    private readonly string _jsonPath;
    private readonly List<string> _logEntries;

    public JsonLoggerRepository(string jsonPath)
    {
        _jsonPath = jsonPath;
        _logEntries = new List<string>();
    }

    public void LogToFile(string message)
    {
        // Не реализовано для JSON-файла

    }

    public void LogToJson(string message)
    {
        _logEntries.Add(message);
    }



    public void Save()
    {
        var logObject = new { Logs = _logEntries };
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(logObject, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_jsonPath, json);
    }

    public void Dispose()
    {
        Save();
    }
}









public class SingleRandomizer
{
    private static SingleRandomizer _instance;
    private static readonly object _lock = new object();
    private readonly Random _random;

    private SingleRandomizer()
    {
        _random = new Random();
    }

    public static SingleRandomizer Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingleRandomizer();
                    }
                }
            }
            return _instance;
        }
    }

    public int NextNumber()
    {
        return _random.Next(1, 101);
    }
}








public class MyLogger
{
    private readonly ILoggerRepository _repository;

    public MyLogger(ILoggerRepository repository)
    {
        _repository = repository;
    }

    public void Log(string message)
    {
        _repository.LogToFile(message);
        _repository.LogToJson(message);
        _repository.Save();
    }
}







class Program
{
    static void Main()
    {

        var watcher = new FileSystemWatcher(@"C:\Users\eisne\Downloads\CRUDRazor-20241220T152016Z-001");
        var observer = new FileChangeObserver();
        watcher.Attach(observer);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        var fileRepo = new FileLoggerRepository("log.txt");
        var jsonRepo = new JsonLoggerRepository("log.json");






























        // Использование FileLoggerRepository
        using (var fileLogger = new FileLoggerRepository("log.txt"))
        {
            var logger = new MyLogger(fileLogger);
            logger.Log("This is a log message.");
        }

        // Использование JsonLoggerRepository
        using (var jsonLogger = new JsonLoggerRepository("log.json"))
        {
            var logger = new MyLogger(jsonLogger);
            logger.Log("This is a log message.");
        }

        Console.WriteLine("Logs written successfully.");

        var randomizer1 = SingleRandomizer.Instance;
        var randomizer2 = SingleRandomizer.Instance;

        Console.WriteLine(randomizer1.NextNumber()); 
        Console.WriteLine(randomizer2.NextNumber()); 
        Console.WriteLine(randomizer1 == randomizer2); 
    }
}