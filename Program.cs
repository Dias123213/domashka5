using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = Logger.GetInstance();

            // Меняем уровень логирования
            logger.SetLogLevel(LogLevel.INFO);

            // Меняем путь к файлу (необязательно)
            logger.SetLogFilePath("app_log.txt");

            // Многопоточное тестирование
            Parallel.For(0, 5, i =>
            {
                var log = Logger.GetInstance();
                log.Log($"Сообщение от потока {i}", LogLevel.INFO);

                if (i % 2 == 0)
                    log.Log($"Предупреждение в потоке {i}", LogLevel.WARNING);

                if (i % 3 == 0)
                    log.Log($"Ошибка в потоке {i}", LogLevel.ERROR);
            });

            Console.WriteLine("=== Логи из файла ===");
            logger.ReadLogs();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
