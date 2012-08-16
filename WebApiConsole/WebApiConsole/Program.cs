using System;

namespace WebApiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var blogEntries = new WebApiAdapter(args[0]).Get();

            foreach (var blogEntry in blogEntries)
            {
                Console.WriteLine(blogEntry.Title);
            }

            Console.ReadLine();
        }
    }
}
