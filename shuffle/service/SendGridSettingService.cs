using System.IO;
using Newtonsoft.Json;
using shuffle.models;

namespace shuffle
{
    public class SendGridSettingService
    {
        public EmailSetting GetSetting()
        {
            var settingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "email-setting.json");
            var settingJson = File.ReadAllText(settingFilePath);
            return JsonConvert.DeserializeObject<EmailSetting>(settingJson);
        }
    }
}