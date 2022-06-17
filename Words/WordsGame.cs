using System;
using System.Collections.Generic;
using System.Timers;

namespace Words
{
    class WordsGame
    {
        private Player firstPlayer;
        private Player secondPlayer;
        private Player currentPlayer;

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
                    throw new ArgumentException("Entered source word is either null or empty!", value);
                }
                if (value.Length > leftBorder && value.Length < rightBorder)
                {
                    sourceWord = value.ToLower();
                }
                else
                {
                    throw new ArgumentException($"Entered source word length doesn't match with interval.", value);
                }
            }
        }

        // handles different console closing signals
        private static SignalHandler signalHandler;

        public WordsGame(Player player1, Player player2)
        {
            this.firstPlayer = player1;
            this.secondPlayer = player2;
        }

        /// <summary>
        /// Summarizes the results of the game
        /// </summary>
        private void SummarizeResult()
        {
            if (currentPlayer.Equals(firstPlayer))
            {
                Console.WriteLine($"Player {secondPlayer.Name} wins.");
                secondPlayer.ScoreWin();
            }
            else
            {
                Console.WriteLine($"Player {firstPlayer.Name} wins.");
                firstPlayer.ScoreWin();
            }
            DataSerializer.Serialize(firstPlayer, "playersScore.json");
            DataSerializer.Serialize(secondPlayer, "playersScore.json");
        }

        /// <summary>
        /// Executes entered command
        /// </summary>
        /// <param name="command">A string that represents entered command</param>
        private void RunCommand(string command)
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
                Console.WriteLine("Score table\nName\tScore");
                currentPlayer.PrintScore();
            }
            else if (command.Equals("/total-score"))
            {
                Console.WriteLine("Total score table\nName\tScore");
                List<Player> players = DataSerializer.DeserializeAll("playersScore.json");
                foreach (Player player in players)
                {
                    player.PrintScore();
                }
            }
        }

        /// <summary>
        /// Main game cycle
        /// </summary>
        private void GameLoop()
        {
            usedWords = new List<string>();
            bool isCommand = false;
            string enteredWord;
            currentPlayer = firstPlayer;
            Timer aTimer;
            // setting the signal on console closing handler
            signalHandler += HandleConsoleSignal;
            ConsoleHelper.SetSignalHandler(signalHandler, true);
            while (true)
            {
                // setting the Timer
                aTimer = new Timer(30000);
                aTimer.Elapsed += delegate
                {
                    Console.WriteLine("Your time is out!");
                    SummarizeResult();
                    Console.ReadKey();
                    Environment.Exit(0);
                };
                aTimer.Start();

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Source word is {sourceWord}.");
                    Console.WriteLine($"Current player: {currentPlayer.Name}");
                    Console.WriteLine("Enter the word. You have only 30 seconds.");
                    Console.WriteLine("Or you can use these commands /show-words, /score, /total-score.");
                    enteredWord = Console.ReadLine().ToLower();
                    aTimer.Stop();
                    if (enteredWord.Equals("/show-words") || enteredWord.Equals("/score") || enteredWord.Equals("/total-score"))
                    {
                        RunCommand(enteredWord);
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

                if (!WordValidator.ValidateWord(sourceWord, enteredWord) || usedWords.Contains(enteredWord))
                {
                    SummarizeResult();
                    break;
                }
                else
                {
                    usedWords.Add(enteredWord);
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

        /// <summary>
        /// Word game's entry point
        /// </summary>
        public void Start()
        {
            bool isFinished = false;
            while (!isFinished)
            {
                Console.WriteLine($"Enter word to start the game. Word length must be in range from {leftBorder} to {rightBorder} letters.");
                SourceWord = Console.ReadLine().ToLower();
                GameLoop();
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

        /// <summary>
        /// Executes results summarizing on console closing
        /// </summary>
        /// <param name="consoleSignal">Signal which is representing by what means console was closed</param>
        private void HandleConsoleSignal(ConsoleSignal consoleSignal)
        {
            SummarizeResult();
        }
    }
}