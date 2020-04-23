using Newtonsoft.Json;

namespace shuffle.models
{
    public enum Role
    {
        [JsonProperty("Judge")]
        Judge,
        [JsonProperty("Wolf")]
        Wolf,
        [JsonProperty("Villager")]
        Villager,
        [JsonProperty("Prophet")]
        Prophet,
        [JsonProperty("Hunter")]
        Hunter,
        [JsonProperty("Witch")]
        Witch,
        [JsonProperty("Cupid")]
        Cupid,
        [JsonProperty("Guard")]
        Guard,
    }
}