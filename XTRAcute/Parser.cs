using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTRAcute
{
    public class Parser
    {
        // Constants
        const int SIGOFFSETLOCATION = 0x3c;
        
        // Methods
        public string ParseExe(byte[] exe)
        {
            var readable = "";

            /* File Headers */

            // MS-DOS stub
            var signatureOffset = exe.Skip(SIGOFFSETLOCATION).Take(1).First();         // Offset of PE Signature
            readable += parseField(exe, "MS-DOS Stub", 0, signatureOffset);
            readable += parseField(exe, "PE Signature Offset", SIGOFFSETLOCATION, 1);

            // PE Signature
            readable += parseField(exe, "Signature", signatureOffset, 4, true);
            // COFF File Header
            // Image-only header

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

            return $"{fieldName}: {fieldVal}";
        }
    }
}
