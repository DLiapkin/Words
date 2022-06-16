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

        public WordsGame(Player player1, Player player2)
        {
            this.firstPlayer = player1;
            this.secondPlayer = player2;
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
            DataSerializer.Serialize(this.firstPlayer, "playersScore.json");
            DataSerializer.Serialize(this.secondPlayer, "playersScore.json");
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

                if (!WordValidator.ValidateWord(this.sourceWord, enteredWord) || this.usedWords.Contains(enteredWord))
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