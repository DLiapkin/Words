using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO;

namespace Words
{
    /// <summary>
    /// Static class that helps with player's serialization and deserialization
    /// </summary>
    static class DataSerializer
    {
        /// <summary>
        /// Serializes player in json file with entered name
        /// </summary>
        /// <param name="data">An object that represents player</param>
        /// <param name="filename">A string that represents json file's name</param>
        public static void Serialize(Player data, string filename)
        {
            if (data == null)
            {
                throw new ArgumentNullException("Player is null!");
            }
            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("Invalid filename!");
            }

            if (File.Exists(filename))
            {
                string jsonData = File.ReadAllText(filename);
                string resultData;
                string serializedData = JsonSerializer.Serialize<Player>(data);
                JsonNode jsonDocument = JsonNode.Parse(jsonData);
                JsonArray players = jsonDocument.Root.AsArray();
                foreach (var player in players)
                {
                    if (((string)player["Name"]) == data.Name)
                    {
                        player["Score"] = data.Score;
                        resultData = players.ToString();
                        File.WriteAllText(filename, resultData);
                        return;
                    }
                }
                JsonNode serializedDataNode = JsonNode.Parse(serializedData);
                players.Add(serializedDataNode);
                resultData = players.ToString();
                File.WriteAllText(filename, resultData);
            }
            else
            {
                List<Player> jsonDataList = new List<Player>();
                jsonDataList.Add(data);
                string jsonData = JsonSerializer.Serialize(jsonDataList);
                File.WriteAllText(filename, jsonData);
            }
        }

        /// <summary>
        /// Deserializes one player by it's name from file with entered name
        /// </summary>
        /// <param name="name">A string that represents player's name</param>
        /// <param name="filename">A string that represents json file's name</param>
        /// <returns>A player if there was a record with such <paramref ref="name"/> parameter; otherwise, null.</returns>
        public static Player DeserializeOneElement(string name, string filename)
        {
            if (!File.Exists(filename)) 
            { 
                throw new ArgumentException(filename);
            }
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException(name);
            }

            Player requestedPlayer = null;
            string jsonData = File.ReadAllText(filename);
            JsonNode jsonDocument = JsonNode.Parse(jsonData);
            JsonArray players = jsonDocument.Root.AsArray();
            foreach (var player in players)
            {
                if (((string)player["Name"]) == name)
                {
                    requestedPlayer = JsonSerializer.Deserialize<Player>(player);
                }
            }

            return requestedPlayer;
        }

        /// <summary>
        /// Deserializes all players from file with entered name
        /// </summary>
        /// <param name="filename">A string that represents json file's name</param>
        /// <returns>A list of players.</returns>
        public static List<Player> DeserializeAll(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new ArgumentException(filename);
            }

            List<Player> requestedPlayers = new List<Player>();
            string jsonData = File.ReadAllText(filename);
            JsonNode jsonDocument = JsonNode.Parse(jsonData);
            JsonArray players = jsonDocument.Root.AsArray();
            requestedPlayers = JsonSerializer.Deserialize<List<Player>>(players);

            return requestedPlayers;
        }
    }
}
