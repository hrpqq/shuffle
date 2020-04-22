using System.Collections.Generic;
using System.Text;

namespace shuffle.models
{
    public class Game
    {
        public Game(int round, List<Player> players)
        {
            Round = round;
            Players = players;
        }

        public List<Player> Players { get; }

        public int Round { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"------Round {Round}--------");
            Players.ForEach(player=>sb.AppendLine($"{player}"));
            return sb.ToString();
        }
    }
}