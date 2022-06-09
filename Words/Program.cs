using System;
using System.Collections.Generic;
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

        static void Main(string[] args)
        {
            bool player1 = true, player2 = false, gameStatus = true;
            List<String> usedWords = new List<String>();
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

            while (gameStatus)
            {
                Console.Write("Current player: ");
                if (player1)
                {
                    Console.Write("Player 1\n");
                }
                else
                {
                    Console.Write("Player 2\n");
                }

                Console.WriteLine("Enter word:");
                String enteredWord = Console.ReadLine().ToLower();

                if(!WordValidation(sourceWord, enteredWord) || usedWords.Contains(enteredWord))
                {
                    gameStatus = false;
                    if(player1)
                    {
                        Console.WriteLine("Wrong word!\nPlayer 2 wins.");
                    }
                    if (player2)
                    {
                        Console.WriteLine("Wrong word!\nPlayer 1 wins.");
                    }
                    Console.ReadKey();
                }
                else
                {
                    usedWords.Add(enteredWord);
                    if (player1)
                    {
                        player1 = false;
                        player2 = true;
                        continue;
                    }
                    if (player2)
                    {
                        player1 = true;
                        player2 = false;
                    }
                }
            }
        }
    }
}
