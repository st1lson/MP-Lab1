using System.IO;

namespace Task2
{
    internal class Program
    {
        private static readonly char[] Separators = { ' ', '\r', '\n', '\t' };

        private static void Main()
        {
            string[] data = new string[10];
            using (StreamReader reader = new("source.txt"))
            {
                string str = string.Empty;
                int dataIndex = 0;
                forReadStatement:
                char dataChar = (char)reader.Read();
                int checkIndex = 0;
                forCheckStatement:
                if (dataChar == Separators[checkIndex])
                {
                    data[dataIndex++] = str;
                    str = default;
                    goto forReadStatement;
                }

                checkIndex++;
                if (checkIndex < Separators.Length)
                {
                    goto forCheckStatement;
                }

                str += dataChar;
                if (reader.EndOfStream)
                {
                    data[dataIndex++] = str;
                }

                if (data.Length * 0.8 <= dataIndex)
                {
                    string[] newArray = new string[data.Length * 2];
                    int copyIndex = 0;
                    forCopyDataStatement:
                    newArray[copyIndex] = data[copyIndex];
                    copyIndex++;

                    if (copyIndex < dataIndex)
                    {
                        goto forCopyDataStatement;
                    }

                    data = newArray;
                }

                if (!reader.EndOfStream)
                {
                    goto forReadStatement;
                }
            }
        }
    }
}
