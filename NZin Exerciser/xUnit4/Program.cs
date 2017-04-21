using System;
using System.Reflection;
using NUnit.Common;
using NUnitLite;


namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Running unit tests...");
            
            var writer = new ExtendedTextWrapper(Console.Out);
            new AutoRun(typeof(Program).GetTypeInfo().Assembly).Execute(args, writer, Console.In);
        }
    }
}
