using System;
using System.IO;

namespace Task2
{
    internal class Program
    {
        private static void Main()
        {
            string[] data = File.ReadAllLines("input.txt");
            (string, int[], int)[] words = new (string, int[], int)[10];
            int lineIndex = 0;
            forStatement:
                string line = data[lineIndex];
                if (line == "")
                {
                    goto forStatementEnd;
                }

                int charIndex = 0;
                string str = string.Empty;
                forReadLineStatement:
                    char wordChar = line[charIndex];
                    if (wordChar is >= 'A' and <= 'Z')
                    {
                        wordChar = (char)(wordChar + 32);
                        goto addChar;
                    }

                    if (wordChar is >= 'a' and <= 'z')
                    {
                        goto addChar;
                    }

                    if (str is null || str.Length == 0)
                    {
                        goto addCharEnd;
                    }

                    // add word
                    Console.WriteLine(str);
                    str = default;
                    goto addCharEnd;

                    addChar:
                        str += wordChar;

                    addCharEnd:

                    charIndex++;
                    if (charIndex < line.Length)
                    {
                        goto forReadLineStatement;
                    }

                    forStatementEnd:
                    lineIndex++;
                    charIndex = 0;
                    if (lineIndex < data.Length)
                    {
                        goto forStatement;
                    }
        }
    }
}
