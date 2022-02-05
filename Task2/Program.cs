using System;
using System.IO;
using System.Linq;

namespace Task2
{
    internal class Program
    {
        private static void Main()
        {
            string[] data = File.ReadAllLines("input.txt");
            (string, int[], int)[] words = new (string, int[], int)[10];
            int lineIndex = 0;
            int elementsCount = 0;
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

                    int wordsCheckIndex = 0;
                    checkWords:
                        if (words[wordsCheckIndex].Item1 == str)
                        {
                            int count = words[wordsCheckIndex].Item3;
                            if (count > 100)
                            {
                                words[wordsCheckIndex].Item3++;
                                goto checkWordsEnd;
                            }

                            words[wordsCheckIndex].Item2[count - 1] = lineIndex / 45 + 1;
                            words[wordsCheckIndex].Item3++;
                            goto checkWordsEnd;
                        }

                        wordsCheckIndex++;
                        if (wordsCheckIndex < elementsCount)
                        {
                            goto checkWords;
                        }

                        if (words.Length * 0.8 <= elementsCount)
                        {
                            (string, int[], int)[] newArray = new (string, int[], int)[words.Length * 2];
                            int copyIndex = 0;
                            forCopyDataStatement:
                                newArray[copyIndex] = words[copyIndex];
                                copyIndex++;

                                if (copyIndex < elementsCount)
                                {
                                    goto forCopyDataStatement;
                                }

                            words = newArray;
                        }

                        int[] pages = new int[100];
                        pages[0] = lineIndex / 45 + 1;
                        (string, int[], int) word = (str, pages, 1);
                        words[elementsCount++] = word;

                    checkWordsEnd:
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
                    if (lineIndex < data.Length)
                    {
                        goto forStatement;
                    }

                    int wordIndex = 0;
            removeExtraOccurrences:
                if (words[wordIndex].Item3 > 100)
                {
                    words[wordIndex] = default;
                }

                wordIndex++;
                if (wordIndex < elementsCount)
                {
                    goto removeExtraOccurrences;
                }

            // sort here

            foreach (var valueTuple in words.OrderBy(w => w.Item1))
            {
                if (valueTuple.Item3 > 0)
                {
                    Console.Write($"{valueTuple.Item1} - ");
                    foreach (var k in valueTuple.Item2)
                    {
                        if (k != 0)
                        {
                            Console.Write($"{k}, ");
                        }
                    }

                    Console.Write("\n");
                }
            }
        }
    }
}
