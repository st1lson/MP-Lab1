using System.IO;

namespace Multi_Paradigm_Programming
{
    internal class Program
    {
        private static readonly string[] BannedWords = { "the", "in", "a", "an", "for", "of", "at", "by" };

        private static void Main()
        {
            string[] data = new string[10];
            int elementsCount = 0;
            using (StreamReader reader = new("input.txt"))
            {
                string str = default;
            forReadStatement:
                char dataChar = (char)reader.Read();
                if (dataChar is >= 'A' and <= 'Z')
                {
                    dataChar = (char)(dataChar + 32);
                    goto addChar;
                }

                if (dataChar is >= 'a' and <= 'z')
                {
                    goto addChar;
                }

                if (str is null || str.Length == 0)
                {
                    goto addCharEnd;
                }

                if (data.Length * 0.8 <= elementsCount)
                {
                    string[] newArray = new string[data.Length * 2];
                    int copyIndex = 0;
                forCopyDataStatement:
                    newArray[copyIndex] = data[copyIndex];
                    copyIndex++;

                    if (copyIndex < elementsCount)
                    {
                        goto forCopyDataStatement;
                    }

                    data = newArray;
                }

                data[elementsCount++] = str;
                str = default;
                goto addCharEnd;

            addChar:
                str += dataChar;

            addCharEnd:

                if (!reader.EndOfStream)
                {
                    goto forReadStatement;
                }
            }

            (string, int)[] distinctWords = new (string, int)[1];
            int index = 0;
            int uniqueElements = 0;
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

            if (distinctWords.Length * 0.8 <= uniqueElements)
            {
                (string, int)[] newArray = new (string, int)[distinctWords.Length * 2];
                int copyIndex = 0;
            forCopyStatement:
                newArray[copyIndex] = distinctWords[copyIndex];
                copyIndex++;

                if (copyIndex < uniqueElements)
                {
                    goto forCopyStatement;
                }

                distinctWords = newArray;
            }

            int banWordIndex = 0;
        forBanWordCheckStatement:
            if (word == BannedWords[banWordIndex])
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
            if (distinctWords[findIndex].Item1 == word)
            {
                distinctWords[findIndex].Item2++;
                goto forFindElementStatementEnd;
            }

            findIndex++;
            if (findIndex < uniqueElements)
            {
                goto forFindElementStatement;
            }

            distinctWords[uniqueElements++] = (word, 1);

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
