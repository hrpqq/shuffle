using Newtonsoft.Json;

namespace shuffle.models
{
    public class EmailSetting
    {
        [JsonProperty("API_KEY")]
        public string API_KEY { get; set; } 
        [JsonProperty("FromEmail")]
        public string FromEmail { get; set; } 
        [JsonProperty("FromName")]
        public string FromName { get; set; }
        
    }
}