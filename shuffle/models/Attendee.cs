using Newtonsoft.Json;

namespace shuffle.models
{
    public class Attendee
    {
        [JsonProperty("name")] public string Name;
        [JsonProperty("email")] public string Email;

        public override string ToString()
        {
            return $"name: {Name}, email: {Email}";
        }
    }
}