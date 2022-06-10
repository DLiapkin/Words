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

        // enum that represents the player's turn
        private enum Turn
        {
            FirstPlayer,
            SecondPlayer
        }

        // borders for source word
        const int leftBorder = 8;
        const int rightBorder = 30;

        /// <summary>
        /// Checks the entered word so that it consists of the letters of the source word
        /// </summary>
        /// <param name="sourceWord">A string that contains source word</param>
        /// <param name="enteredWord">A string that contains source word</param>
        /// <returns>true if the <see cref="enteredWord"/> parameter was valid; otherwise, false.</returns>
        private static bool ValidateWord(String sourceWord, String enteredWord)
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
        /// <param name="player">An enum that indicates player phase</param>
        private static void PrintWinner(Turn player)
        {
            if (player == Turn.FirstPlayer)
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
        private static void GameLoop(String sourceWord)
        {
            List<String> usedWords = new List<String>();
            Turn player = Turn.FirstPlayer;
            Timer aTimer;
            while (true)
            {
                // setting the Timer
                aTimer = new Timer(30000);
                aTimer.Elapsed += delegate
                {
                    Console.WriteLine("Your time is out!");
                    PrintWinner(player);
                    Console.ReadKey();
                    Environment.Exit(0);
                };
                aTimer.Start();

                Console.Write("Current player: ");
                if (player == Turn.FirstPlayer)
                {
                    Console.Write("Player 1\n");
                }
                else
                {
                    Console.Write("Player 2\n");
                }
                Console.WriteLine("Enter the word. You have only 30 seconds.");
                String enteredWord = Console.ReadLine().ToLower();

                if (!ValidateWord(sourceWord, enteredWord) || usedWords.Contains(enteredWord))
                {
                    PrintWinner(player);
                    break;
                }
                else
                {
                    usedWords.Add(enteredWord);
                    if (player == Turn.FirstPlayer)
                    {
                        player = Turn.SecondPlayer;
                    }
                    else
                    {
                        player = Turn.FirstPlayer;
                    }
                }
            }
            // disposing of the Timer
            aTimer.Stop();
            aTimer.Close();
        }

        static void Main(string[] args)
        {
            String sourceWord;
            Console.WriteLine($"Enter word to start the game. Word length must be in range from {leftBorder} to {rightBorder} letters.");
            while (true)
            {
                sourceWord = Console.ReadLine().ToLower();
                if (sourceWord.Length > leftBorder && sourceWord.Length < rightBorder)
                {
                    break;
                }
                Console.WriteLine("Incorrect word length!\nTry again!");
            }
            GameLoop(sourceWord);
            Console.ReadKey();
        }
    }
}
