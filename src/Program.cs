using System.IO;
using System.Text;

namespace Multi_Paradigm_Programming
{
    internal class Program
    {
        private static readonly string[] BannedWords = { "the", "in", "a", "an", "for", "of", "at", "by" };
        private static readonly char[] Separators = { ' ', '.', ',', '!', '?', '\r', '\n', '\t' };

        private static void Main()
        {
            string[] data = new string[10];
            using (StreamReader reader = new("input.txt"))
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

            (string, int)[] distinctWords = new (string, int)[1];
            int index = 0;
            int elementsCount = 0;
        forStatement:
            string word = data[index];
            if (word is null)
            {
                index++;
                if (index == data.Length - 1)
                {
                    goto forStatementEnd;
                }

                goto forStatement;
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

            int banWordIndex = 0;
        forBanWordCheckStatement:
            if (lowerCaseWord == BannedWords[banWordIndex])
            {
                index++;
                goto forStatement;
            }

            banWordIndex++;
            if (banWordIndex < BannedWords.Length)
            {
                goto forBanWordCheckStatement;
            }

            int findIndex = 0;
        forFindElementStatement:
            if (distinctWords[findIndex].Item1 == lowerCaseWord)
            {
                distinctWords[findIndex].Item2++;
                goto forFindElementStatementEnd;
            }

            findIndex++;
            if (findIndex < elementsCount)
            {
                goto forFindElementStatement;
            }

            distinctWords[elementsCount++] = (lowerCaseWord, 1);

        forFindElementStatementEnd:
            index++;

            if (index < data.Length)
            {
                goto forStatement;
            }

        forStatementEnd:
            int i = 0;
        forSortStatement:
            int j = 0;
        forSortSecondStatement:
            if (distinctWords[j].Item2 < distinctWords[j + 1].Item2)
            {
                (distinctWords[j], distinctWords[j + 1]) = (distinctWords[j + 1], distinctWords[j]);
            }

            j++;
            if (j < distinctWords.Length - 1)
            {
                goto forSortSecondStatement;
            }

            i++;
            if (i < distinctWords.Length)
            {
                goto forSortStatement;
            }

            int elementIndex = 0;
            using StreamWriter writer = new("output.txt");
        forOutputStatement:
            if (distinctWords[elementIndex].Item2 != 0)
            {
                writer.WriteLine($"{distinctWords[elementIndex].Item1} - {distinctWords[elementIndex].Item2}");
            }

            elementIndex++;
            if (elementIndex < distinctWords.Length)
            {
                goto forOutputStatement;
            }
        }
    }
}
