using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2805();
            Welcome6075();
            Console.ReadKey();
        }

        static partial void Welcome6075();
        private static void Welcome2805()
        {
            Console.WriteLine("Enter your name: ");
            string a = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", a);
        }
    }


}
