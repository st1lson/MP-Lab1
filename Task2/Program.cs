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

            int i = 0;
        firstSortStatement:
            int j = 0;
        secondSortStatement:
            bool needSwap = false;
            string firstWord = words[j].Item1;
            string secondWord = words[j + 1].Item1;
            if (secondWord is null)
            {
                goto compareWordsEnd;
            }

            if (firstWord is null)
            {
                needSwap = true;
                goto compareWordsEnd;
            }

            int length = secondWord.Length < firstWord.Length ? secondWord.Length : firstWord.Length;
            int wordCharIndex = 0;
        compareWords:
            char firstWordChar = firstWord[wordCharIndex];
            char secondWordChar = secondWord[wordCharIndex];

            if (firstWordChar > secondWordChar)
            {
                needSwap = true;
                goto compareWordsEnd;
            }

            if (firstWordChar < secondWordChar)
            {
                goto compareWordsEnd;
            }

            wordCharIndex++;
            if (wordCharIndex < length)
            {
                goto compareWords;
            }

        compareWordsEnd:
            if (needSwap)
            {
                (words[j], words[j + 1]) = (words[j + 1], words[j]);
            }

            j++;
            if (j < elementsCount - 1)
            {
                goto secondSortStatement;
            }

            i++;
            if (i < elementsCount)
            {
                goto firstSortStatement;
            }

            using StreamWriter writer = new("output.txt");
            int outputWordIndex = 0;
        forOutputStatement:
            if (words[outputWordIndex].Item3 > 0)
            {
                writer.Write($"{words[outputWordIndex].Item1} - ");
                int pageIndex = 0;
            forOutputPagesStatement:
                if (words[outputWordIndex].Item2[pageIndex] != 0)
                {
                    writer.Write($"{words[outputWordIndex].Item2[pageIndex]}, ");
                }

                pageIndex++;
                if (pageIndex < words[outputWordIndex].Item2.Length)
                {
                    goto forOutputPagesStatement;
                }

                writer.Write('\n');
            }

            outputWordIndex++;
            if (outputWordIndex < elementsCount)
            {
                goto forOutputStatement;
            }
        }
    }
}
