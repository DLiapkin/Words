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

        public void PrintScore()
        {
            Console.WriteLine($"{this.Name}:\t{this.Score}");
        }

        public void ScoreWin()
        {
            this.Score++;
        }
    }
}
