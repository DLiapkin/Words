using System;

namespace Words
{
    class Program
    {
        static void Main(string[] args)
        {
            string playerName;
            Player player1, player2;
            Console.WriteLine($"Enter name of the first player.");
            playerName = Console.ReadLine();
            player1 = DataSerializer.DeserializeOneElement(playerName, "playersScore.json");
            if (player1 == null)
            {
                player1 = new Player(playerName);
            }
            Console.WriteLine($"Enter name of the second player.");
            playerName = Console.ReadLine();
            player2 = DataSerializer.DeserializeOneElement(playerName, "playersScore.json");
            if (player2 == null)
            {
                player2 = new Player(playerName);
            }
            WordsGame newGame = new WordsGame(player1, player2);
            newGame.Start();
        }
    }
}
