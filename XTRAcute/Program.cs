using System;
using System.IO;
using System.Text;

namespace XTRAcute
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Filename argument expected.");

            var parser = new Parser();
            // Open and parse the executable
            var filepath = args[0];
            var bytes = File.ReadAllBytes(filepath + ".exe");
            var parsedExe = parser.ParseExe(bytes);

            // Write the parsed executable to a text file in the same directory
            var outputFile = File.Create(filepath + ".txt");
            var writeBytes = Encoding.UTF8.GetBytes(parsedExe);
            outputFile.Write(writeBytes, 0, writeBytes.Length);

            // Prevent immediate program exit for debug purposes
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
