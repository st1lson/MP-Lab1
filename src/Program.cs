using System;
using System.Collections.Generic;
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

            Dictionary<string, int> distinctWords = new();
            int index = 0;
            forStatement:
                string word = data[index];
                if (word is null)
                {
                    throw new ArgumentNullException();
                }

                int charIndex = 0;
                StringBuilder lowerCaseWordBuilder = new()
                {
                    Length = word.Length
                };
                forWordStatement:
                    if (word[charIndex] >= 'A' || word[charIndex] <= 'Z')
                    {
                        lowerCaseWordBuilder[charIndex] = (char)(word[charIndex] + 40);
                    }

                    lowerCaseWordBuilder[charIndex] = word[charIndex];
                    charIndex++;

                if (charIndex < word.Length)
                {
                    goto forWordStatement;
                }

                string lowerCaseWord = lowerCaseWordBuilder.ToString();

                if (!BannedWords.Contains(lowerCaseWord))
                {
                    if (distinctWords.ContainsKey(lowerCaseWord))
                    {
                        distinctWords[lowerCaseWord]++;
                    }
                    else
                    {
                        distinctWords.Add(lowerCaseWord, 1);
                    }
                }

                index++;

            if (index < data.Length)
            {
                goto forStatement;
            }

            List<KeyValuePair<string, int>> sortedWords = distinctWords.OrderByDescending(w => w.Value).ToList();

            foreach (var (key, value) in sortedWords)
            {
                Console.WriteLine($"{key} - {value}");
            }
        }
    }
}
