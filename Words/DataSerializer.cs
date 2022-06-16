using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO;

namespace Words
{
    static class DataSerializer
    {
        public static void Serialize(Player data, string filename)
        {
            if(File.Exists(filename))
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

        public static Player DeserializeOneElement(string name, string filename)
        {
            if (!File.Exists(filename)) 
            { 
                throw new ArgumentException(filename);
            }
            if (string.IsNullOrEmpty(name))
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
