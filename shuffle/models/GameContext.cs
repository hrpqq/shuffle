using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shuffle.models
{
    public class GameContext
    {
        public ShuffleService ShuffleSvc { get; }

        public GameContext(int round)
        {
            var gameSetting = new GameSettingService().GetSetting();
            Round = round;
            Attendees = gameSetting.Attendees.Where(a => a.Available).ToList();
            Rule = gameSetting.Rule;
            RulesDescription = gameSetting.RulesDescription;
            GameDescription = gameSetting.GameDescription;
            ShuffleSvc = new ShuffleService();
        }


        public int Round { get; set; }
        public List<Attendee> Attendees { get; }

        public GameRule Rule { get; set; }

        public string[] RulesDescription { get; set; }

        public string GameDescription { get; set; }

        public bool IsValid(out string msg)
        {
            var attendeesCount = Attendees.Count;
            var rolesCount = Rule.Role2Count.Values.Sum();
            if (attendeesCount != rolesCount)
            {
                msg = $"Attendees count does not match roles count. Attendees: {attendeesCount}, roles: {rolesCount}.";
                return false;
            }

            if (!string.IsNullOrEmpty(Rule.JudgeEmail) && !Attendees.Select(a => a.Email).Contains(Rule.JudgeEmail))
            {
                msg = $"Judge's email: {Rule.JudgeEmail} does not exist in attendees list, please check.";
                return false;
            }
            msg = string.Empty;
            return true;
        }

        public Game GetGame()
        {
            var players = ShuffleSvc.Shuffle(Attendees, Rule);
            players = players.Select(p =>
            {
                p.GameDescription = this.GameDescription;
                p.RoleDescription = RulesDescription.FirstOrDefault(rd => rd.Contains(p.Role.ToString()));
                return p;
            }).ToList();
            players = ShuffleService.Shuffle<Player>(players).ToList();
            Player.ParentList = players;
            players.ForEach(p => p.NameNumMapList = string.Join("<br>", players.Select(q => 
            {
                if (p.Role == Role.Judge)
                    return $"{players.IndexOf(q)}--{q.Attendee.Name}--{q.Role}";
                else
                    return $"{players.IndexOf(q)}--{q.Attendee.Name}  {AddRoleIndecator(players.IndexOf(q))}";
            }
            )));
            players.ForEach(q => { q.Company = players.Where(p => p.Role == q.Role 
                                                                    && q.Role == Role.Wolf 
                                                                    && p.Attendee.Name!=q.Attendee.Name).ToList(); });

            return new Game(Round++, players);
        }

        private string AddRoleIndecator(int index)
        {
            var sb_buttons = new StringBuilder();
            sb_buttons.Append("<br>");
            sb_buttons.Append("<input type=\"checkbox\" id=\"retainQuestion\">淘汰</input>");
            sb_buttons.Append($"<input type=\"radio\" name=\"role{index}\">法官</input>");
            sb_buttons.Append($"<input type=\"radio\" name=\"role{index}\">狼人</input>");
            sb_buttons.Append($"<input type=\"radio\" name=\"role{index}\">先知</input>");
            sb_buttons.Append($"<input type=\"radio\" name=\"role{index}\">猎人</input>");
            sb_buttons.Append($"<input type=\"radio\" name=\"role{index}\">守卫</input>");
            sb_buttons.Append($"<input type=\"radio\" name=\"role{index}\">村民</input>");
            sb_buttons.Append($"<input type=\"radio\" name=\"role{index}\" checked>未知</input>");
            return sb_buttons.ToString();
        }
    }
}