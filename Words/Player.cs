using System;
using System.Text.Json;

namespace Words
{
    class Player
    {
        private string name;
        private int score;

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

        public Player(string playerName)
        {
            Name = playerName;
        }

        Player(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public void PrintScore()
        {
            Console.WriteLine($"{this.name}:\t{this.score}");
        }

    }
}
