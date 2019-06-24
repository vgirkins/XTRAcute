using System;
using System.IO;
using System.Text;

namespace XTRAcute
{
    class Program
    {
        static string parseExe(byte[] exe)
        {
            // TODO

            // MS-DOS 2.0 Compatible EXE Header

            // unused

            // OEM Identifier

                // OEM Information

                // Offset to PE Header

            // MS-DOS 2.0 Stub Program and Relocation Table

            // unused

            // PE Header (aligned on 8-byte boundary)

            // Section Headers

            // Image Pages:

                // import info

                // export info

                // base relocations

                // resource info            
            return "";
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Filename argument expected.");

            // Open and parse the executable
            var filename = args[0];
            var bytes = File.ReadAllBytes(filename + ".exe");
            var parsedExe = parseExe(bytes);

            // Write the parsed executable to a text file in the same directory
            var outputFile = File.Create(filename + ".txt");
            var writeBytes = Encoding.UTF8.GetBytes(parsedExe);
            outputFile.Write(writeBytes, 0, writeBytes.Length);
        }
    }
}
