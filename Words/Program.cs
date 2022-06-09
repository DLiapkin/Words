using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words
{
    class Program
    {
        protected Program()
        {

        }

        /// <summary>
        /// Checks the entered word so that it consists of the letters of the source word
        /// </summary>
        /// <param name="sourceWord">A string that contains source word</param>
        /// <param name="enteredWord">A string that contains source word</param>
        /// <returns>true if the <see cref="enteredWord"/> parameter was valid; otherwise, false.</returns>
        private static bool WordValidation(String sourceWord, String enteredWord)
        {
            if (string.IsNullOrEmpty(sourceWord))
            {
                throw new ArgumentException(sourceWord);
            }

            if (string.IsNullOrEmpty(enteredWord))
            {
                throw new ArgumentException(enteredWord);
            }
 
            foreach (char ch in enteredWord)
            {
                bool isPresent = false;
                foreach(char letter in sourceWord)
                {
                    if(ch == letter)
                    {
                        isPresent = true;
                    }
                }
                if(!isPresent)
                {
                    return false;
                }
            }

            int counter;
            for (int i = 0; i < enteredWord.Length; i++)
            {
                counter = 0;
                char currentLetter = enteredWord[i];
                foreach (char ch in sourceWord)
                {
                    if (currentLetter == ch)
                    {
                        counter++;
                    }
                }
                foreach (char ch in enteredWord)
                {
                    if (currentLetter == ch)
                    {
                        counter--;
                    }
                }

                if (counter < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Says who has won the game
        /// </summary>
        /// <param name="phase">A bool that indicates player phase; true - first player, false - second</param>
        private static void WhoWon(bool phase)
        {
            if (phase)
            {
                Console.WriteLine("Wrong word!\nPlayer 2 wins.");
            }
            else
            {
                Console.WriteLine("Wrong word!\nPlayer 1 wins.");
            }
        }

        /// <summary>
        /// Main game cycle
        /// </summary>
        /// <param name="sourceWord">A String that contains source word</param>
        private static void GameCycle(String sourceWord)
        {
            bool playerPhase = true;
            List<String> usedWords = new List<String>();
            while (true)
            {
                Console.Write("Current player: ");
                if (playerPhase)
                {
                    Console.Write("Player 1\n");
                }
                else
                {
                    Console.Write("Player 2\n");
                }

                Console.WriteLine("Enter word:");
                String enteredWord = Console.ReadLine().ToLower();

                if (!WordValidation(sourceWord, enteredWord) || usedWords.Contains(enteredWord))
                {
                    WhoWon(playerPhase);
                    break;
                }
                else
                {
                    usedWords.Add(enteredWord);
                    if (playerPhase)
                    {
                        playerPhase = false;
                    }
                    else
                    {
                        playerPhase = true;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            String sourceWord;
            Console.WriteLine("Enter word to start the game. Word length must be in range from 8 to 30 letters.");
            while (true)
            {
                sourceWord = Console.ReadLine().ToLower();
                if (sourceWord.Length > 8 && sourceWord.Length < 30)
                {
                    break;
                }
                Console.WriteLine("Incorrect word length!\nTry again!");
            }
            GameCycle(sourceWord);
            Console.ReadKey();
        }
    }
}
