using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTRAcute
{
    public class Parser
    {
        const int SIGOFFSETLOCATION = 0x3c;
        private int signatureOffset;
        private int sectionTableSize;
        private int symbolTablePointer;
        private int numSymbols;
        private int optionalHeaderSize;
        
        public string ParseExe(byte[] exe)
        {
            var readable = "";

            /* File Headers */

            // MS-DOS stub
            signatureOffset = exe.Skip(SIGOFFSETLOCATION).Take(1).First();         // Offset of PE Signature
            readable += parseField(exe, "MS-DOS Stub", 0, signatureOffset);            // TODO find doc for this application and parse it better
            readable += parseField(exe, "Signature Offset", SIGOFFSETLOCATION, 1);

            // PE Signature
            readable += parseField(exe, "Signature", signatureOffset, 4, true);

            // COFF File Header
            var coffFileHeaderOffset = signatureOffset + 4;

            readable += parseField(exe, "Machine", coffFileHeaderOffset, 2);    // TODO map to readable value from MS docs

            sectionTableSize = toInt(exe, coffFileHeaderOffset + 2, 2);
            readable += parseField(exe, "NumberOfSections", coffFileHeaderOffset + 2, 2);

            readable += parseField(exe, "TimeDateStamp", coffFileHeaderOffset + 4, 4);  // TODO parse into readable timestamp

            symbolTablePointer = toInt(exe, coffFileHeaderOffset + 8, 4);
            readable += parseField(exe, "PointerToSymbolTable", coffFileHeaderOffset + 8, 4);

            numSymbols = toInt(exe, coffFileHeaderOffset + 12, 4);
            readable += parseField(exe, "NumberOfSymbols", coffFileHeaderOffset + 12, 4);

            optionalHeaderSize = toInt(exe, coffFileHeaderOffset + 16, 2);
            readable += parseField(exe, "SizeOfOptionalHeader", coffFileHeaderOffset + 16, 2);

            readable += parseField(exe, "Characteristics", coffFileHeaderOffset + 18, 2);   // TODO map to readable value from MS docs

            // Optional header

            /* Section Table (Section Headers - 40 bytes each) */

            /* Other Contents */

            return readable;
        }

        private string parseField(byte[] bytes, string fieldName, int offset, int length, bool asString = false)
        {
            var fieldBytes = bytes.Skip(offset).Take(length).ToArray();
            string fieldVal;
            if (asString)
                fieldVal = Encoding.UTF8.GetString(fieldBytes);
            else
                fieldVal = string.Join(" ", fieldBytes);

            return $"{fieldName}: {fieldVal}\n";
        }

        private int toInt(byte[] bytes, int offset, int length)
        {
            var intBytes = bytes.Skip(offset).Take(length).ToArray();
            return BitConverter.ToInt32(intBytes, 0);
        }
    }
}
