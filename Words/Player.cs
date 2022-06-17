using System;
using System.Text.Json.Serialization;

namespace Words
{
    class Player
    {
        private string name;
        private int score = 0;

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(value);
                }
                else
                {
                    name = value;
                }
            }
        }

        public int Score
        {
            get => score;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                else
                {
                    score = value;
                }
            }
        }

        [JsonConstructor]
        public Player(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public Player(string playerName)
        {
            Name = playerName;
        }

        /// <summary>
        /// Prints players Name and Score in formatted form
        /// </summary>
        public void PrintScore()
        {
            Console.WriteLine($"{Name}:\t{Score}");
        }

        /// <summary>
        /// Scores win for player
        /// </summary>
        public void ScoreWin()
        {
            Score++;
        }
    }
}
