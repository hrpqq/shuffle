using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using shuffle.models;

namespace shuffle
{
    public class GameSettingService
    {
        public GameSetting GetSetting()
        {
            var settingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "assets", "game-setting.json");
            var settingJson = File.ReadAllText(settingFilePath);
            return JsonConvert.DeserializeObject<GameSetting>(settingJson);
        }
    }
}