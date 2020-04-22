using System.Collections.Generic;
using Newtonsoft.Json;

namespace shuffle.models
{
    public class GameRule
    {
        [JsonProperty("Role2Count")]
        public Dictionary<Role, int> Role2Count { get; set; }
        [JsonProperty("JudgeEmail")]
        public string JudgeEmail { get; set; }
    }
}