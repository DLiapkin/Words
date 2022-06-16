using System;
using System.Collections.Generic;
using System.Timers;

namespace Words
{
    class WordsGame
    {
        private Player firstPlayer;
        private Player secondPlayer;

        private string sourceWord;
        private List<string> usedWords;

        // borders for source word
        private const int leftBorder = 8;
        private const int rightBorder = 30;

        public string SourceWord
        {
            get => sourceWord;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(value);
                }
                if (value.Length > leftBorder && value.Length < rightBorder)
                {
                    this.sourceWord = value.ToLower();
                }
                else
                {
                    throw new ArgumentException(value);
                }
            }
        }

        //public WordsGame(Player player1, Player player2, string sourceWord)
        //{
        //    this.firstPlayer = player1;
        //    this.secondPlayer = player2;
        //    this.SourceWord = sourceWord;
        //}

        public WordsGame(Player player1, Player player2)
        {
            this.firstPlayer = player1;
            this.secondPlayer = player2;
        }

        /// <summary>
        /// Checks the entered word so that it consists of the letters of the source word
        /// </summary>
        /// <param name="sourceWord">A string that contains source word</param>
        /// <param name="enteredWord">A string that contains source word</param>
        /// <returns>true if the <paramref ref="enteredWord"/> parameter was valid; otherwise, false.</returns>
        private static bool ValidateWord(string sourceWord, string enteredWord)
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

        /// <summary>
        /// Summarizes the results of the game
        /// </summary>
        /// <param name="player">Variable that represents current player that lost the game</param>
        private void SummarizeResult(Player player)
        {
            if (player.Equals(this.firstPlayer))
            {
                Console.WriteLine($"Player {secondPlayer.Name} wins.");
                secondPlayer.ScoreWin();
            }
            else
            {
                Console.WriteLine($"Player {firstPlayer.Name} wins.");
                firstPlayer.ScoreWin();
            }
        }

        /// <summary>
        /// Executes entered command
        /// </summary>
        /// <param name="command">A string that represents entered command</param>
        /// <param name="currentPlayer">Variable that represents the current player</param>
        private void RunCommand(string command, Player currentPlayer)
        {
            if (command.Equals("/show-words"))
            {
                foreach (string word in this.usedWords)
                {
                    Console.WriteLine(word + " ");
                }
            }
            else if (command.Equals("/score"))
            {
                currentPlayer.PrintScore();
            }
            else if (command.Equals("/total-score"))
            {
                Console.WriteLine("Not implemented yet");
            }
        }

        /// <summary>
        /// Main game cycle
        /// </summary>
        private void GameLoop()
        {
            this.usedWords = new List<string>();
            bool isCommand = false;
            string enteredWord;
            Player currentPlayer = firstPlayer;
            Timer aTimer;
            while (true)
            {
                // setting the Timer
                aTimer = new Timer(30000);
                aTimer.Elapsed += delegate
                {
                    Console.WriteLine("Your time is out!");
                    SummarizeResult(currentPlayer);
                    Console.ReadKey();
                    Environment.Exit(0);
                };
                aTimer.Start();

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Source word is {this.sourceWord}.");
                    Console.WriteLine($"Current player: {currentPlayer.Name}");
                    Console.WriteLine("Enter the word. You have only 30 seconds.");
                    Console.WriteLine("Or you can use these commands /show-words, /score, /total-score.");
                    enteredWord = Console.ReadLine().ToLower();
                    aTimer.Stop();
                    if (enteredWord.Equals("/show-words") || enteredWord.Equals("/score") || enteredWord.Equals("/total-score"))
                    {
                        RunCommand(enteredWord, currentPlayer);
                        isCommand = true;
                        Console.ReadKey();
                        aTimer.Start();
                    }
                    else
                    {
                        isCommand = false;
                    }
                }
                while (isCommand);

                if (!ValidateWord(this.sourceWord, enteredWord) || this.usedWords.Contains(enteredWord))
                {
                    SummarizeResult(currentPlayer);
                    break;
                }
                else
                {
                    this.usedWords.Add(enteredWord);
                    if (currentPlayer.Equals(firstPlayer))
                    {
                        currentPlayer = secondPlayer;
                    }
                    else
                    {
                        currentPlayer = firstPlayer;
                    }
                }
            }
            // disposing of the Timer
            aTimer.Stop();
            aTimer.Close();
        }

        public void Start()
        {
            bool isFinished = false;
            while (!isFinished)
            {
                Console.WriteLine($"Enter word to start the game. Word length must be in range from {leftBorder} to {rightBorder} letters.");
                this.SourceWord = Console.ReadLine().ToLower();
                this.GameLoop();
                string variant;
                Console.WriteLine("Do you want to rematch?\n Yes or no?");
                variant = Console.ReadLine().ToLower();
                if (!variant.Equals("yes"))
                {
                    isFinished = true;
                }
            }
            Console.ReadKey();
        }
    }
}