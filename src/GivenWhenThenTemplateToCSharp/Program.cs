using System;
using System.Linq;

namespace GivenWhenThenTemplateToCSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var feature = args.First();
            Console.WriteLine(Environment.CurrentDirectory);
            Console.WriteLine(feature);
            Console.ReadLine();
        }
    }
}