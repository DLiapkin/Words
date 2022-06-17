using System;

namespace Words
{
    /// <summary>
    /// Static class that helps with validating the word
    /// </summary>
    static class WordValidator
    {
        /// <summary>
        /// Checks the entered word so that it consists of the letters of the source word
        /// </summary>
        /// <param name="sourceWord">A string that contains source word</param>
        /// <param name="enteredWord">A string that contains source word</param>
        /// <returns>true if the <paramref ref="enteredWord"/> parameter was valid; otherwise, false.</returns>
        public static bool ValidateWord(string sourceWord, string enteredWord)
        {
            if (string.IsNullOrEmpty(sourceWord))
            {
                throw new ArgumentException(sourceWord);
            }
            if (string.IsNullOrEmpty(enteredWord))
            {
                throw new ArgumentException(enteredWord);
            }

            // checks for the presence of letters in the source word
            foreach (char ch in enteredWord)
            {
                if (!sourceWord.Contains(ch))
                {
                    return false;
                }
            }

            // checks if the number of letters in the entered word and the source word match
            int counter;
            for (int i = 0; i < enteredWord.Length; i++)
            {
                char currentLetter = enteredWord[i];
                counter = CountLetters(currentLetter, sourceWord) - CountLetters(currentLetter, enteredWord);
                if (counter < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Counts a number of occurences of letter in the word
        /// </summary>
        /// <param name="letter">A letter that needs to be found</param>
        /// <param name="word">A word in which searching will be proceeded</param>
        /// <returns>int that represents the number of occurences</returns>
        private static int CountLetters(char letter, string word)
        {
            int counter = 0;
            string temporary = word;
            int index = temporary.IndexOf(letter);
            while (index != -1)
            {
                if (index == word.Length - 1)
                {
                    counter++;
                    break;
                }
                temporary = temporary.Substring(index + 1);
                counter++;
                index = temporary.IndexOf(letter);
            }
            return counter;
        }
    }
}
