﻿using System;
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
            foreach(char ch in enteredWord)
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
            for(int i = 0; i < enteredWord.Length; i++)
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
                    if(currentLetter == ch)
                    {
                        counter--;
                    }
                }

                if(counter < 0)
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
            Console.WriteLine("Enter word to start the game.");
            sourceWord = Console.ReadLine();

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
                String enteredWord = Console.ReadLine();

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