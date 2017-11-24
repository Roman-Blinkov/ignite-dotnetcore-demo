using System;
using System.Linq;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Log;

namespace ignite_nuget_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new IgniteConfiguration
            {            
                Logger = new MyLogger()
            };

            var ignite = Ignition.Start(cfg);

            var cache = ignite.GetOrCreateCache<int, string>("foo");
            cache[1] = "Hello World!";

            Console.WriteLine(cache.Single());
        }
    }

    class MyLogger : ILogger
    {
        public bool IsEnabled(LogLevel level)
        {
            return level >= LogLevel.Debug;
        }

        public void Log(LogLevel level, string message, object[] args, IFormatProvider formatProvider, string category, string nativeErrorInfo, Exception ex)
        {
            if (!(category ?? "").StartsWith("org.apache"))
            {
                Console.WriteLine("|> " + message, args);
            }
        }
    }
}
