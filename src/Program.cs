using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Multi_Paradigm_Programming
{
    internal class Program
    {
        private static readonly string[] BannedWords = { "the", "in", "a", "an", "for", "of", "at", "by" };
        private static readonly string[] Separators = { " ", "\r", "\n", "\t" };

        private static void Main()
        {
            string[] data;
            using (StreamReader reader = new("source.txt"))
            {
                data = reader.ReadToEnd().Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            }

            (string, int)[] distinctWords = new (string, int)[1];
            int index = 0;
            int elementsCount = 0;
            forStatement:
                string word = data[index];
                if (word is null)
                {
                    throw new ArgumentNullException();
                }

                if (distinctWords.Length * 0.8 <= elementsCount)
                {
                    (string, int)[] newArray = new (string, int)[distinctWords.Length * 2];
                    int copyIndex = 0;
                    forCopyStatement:
                        newArray[copyIndex] = distinctWords[copyIndex];
                        copyIndex++;
                    
                        if (copyIndex < elementsCount)
                        {
                            goto forCopyStatement;
                        }

                    distinctWords = newArray;
                }

                int charIndex = 0;
                StringBuilder lowerCaseWordBuilder = new()
                {
                    Length = word.Length
                };
                forWordStatement:
                    if (word[charIndex] >= 'A' && word[charIndex] <= 'Z')
                    {
                        lowerCaseWordBuilder[charIndex] = (char)(word[charIndex] + 32);
                    }
                    else
                    {
                        lowerCaseWordBuilder[charIndex] = word[charIndex];
                    }

                    charIndex++;

                    if (charIndex < word.Length)
                    {
                        goto forWordStatement;
                    }

                string lowerCaseWord = lowerCaseWordBuilder.ToString();

                if (!BannedWords.Contains(lowerCaseWord))
                {
                    int findIndex = 0;
                    forFindElementStatement:
                        if (distinctWords[findIndex].Item1 == lowerCaseWord)
                        {
                            distinctWords[findIndex].Item2++;
                        }

                        findIndex++;
                        if (findIndex < elementsCount)
                        {
                            goto forFindElementStatement;
                        }

                        distinctWords[elementsCount++] = (lowerCaseWord, 1);
                }

                index++;

                if (index < data.Length)
                {
                    goto forStatement;
                }

            for (int i = 0; i < elementsCount; i++)
            {
                Console.WriteLine($"{distinctWords[i].Item1} - {distinctWords[i].Item2}");
            }
        }
    }
}
