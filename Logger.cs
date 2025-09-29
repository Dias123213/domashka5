using System;
using System.IO;
using System.Threading;

namespace SingletonLogger
{
    // Уровни логирования
    public enum LogLevel
    {
        INFO,
        WARNING,
        ERROR
    }

    public sealed class Logger
    {
        private static Logger _instance = null;
        private static readonly object _lock = new object();

        private string _logFilePath = "log.txt"; // путь к файлу по умолчанию
        private LogLevel _currentLogLevel = LogLevel.INFO;

        // Приватный конструктор (нельзя создать извне)
        private Logger() { }

        // Метод для получения единственного экземпляра
        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock) // блокировка для потокобезопасности
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }

        // Метод изменения уровня логирования
        public void SetLogLevel(LogLevel level)
        {
            _currentLogLevel = level;
        }

        // Метод изменения пути к файлу логов
        public void SetLogFilePath(string path)
        {
            _logFilePath = path;
        }

        // Метод записи логов
        public void Log(string message, LogLevel level)
        {
            if (level < _currentLogLevel)
                return;

            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

            lock (_lock) // блокировка на запись в файл
            {
                File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
            }

            Console.WriteLine(logMessage); // выводим ещё и в консоль
        }

        // Чтение логов
        public void ReadLogs()
        {
            if (File.Exists(_logFilePath))
            {
                Console.WriteLine("=== Содержимое логов ===");
                Console.WriteLine(File.ReadAllText(_logFilePath));
            }
            else
            {
                Console.WriteLine("Файл логов отсутствует.");
            }
        }
    }
}
