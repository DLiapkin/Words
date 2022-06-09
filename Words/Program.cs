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

        private static bool WordValidation(String sourceWord, String enteredWord)
        {

        }

        static void Main(string[] args)
        {
            bool player1 = true, player2 = false, gameStatus = true;
            List<String> usedWords = new List<String>();
            String sourceWord;
            Console.WriteLine("Enter word to start the game.");
            sourceWord = Console.ReadLine();

            while(gameStatus)
            {
                Console.WriteLine("Enter word:");
                String enteredWord = Console.ReadLine();

                if(!WordValidation(sourceWord,enteredWord))
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
                }
                else
                {
                    usedWords.Add(enteredWord);
                    if (player1)
                    {
                        player1 = false;
                        player2 = true;
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
