using System.Collections.Generic;
using System.Linq;

namespace shuffle.models
{
    public class Player
    {
        public string NameNumMapList { get; set; }

        public static List<Player> ParentList { get; set; }

        public List<Player> Company { get; set; }

        public string CompanyStr { get { return GetCompanyStr(); } }

        public Player(Attendee attendee, Role role,string roleDes = null,string gameDes = null)
        {
            Attendee = attendee;
            Role = role;
            RoleDescription = roleDes;
            GameDescription = gameDes;
        }
        public Attendee Attendee { get; set; }
        public Role Role { get; set; }

        public string RoleDescription { get; set; }

        public string GameDescription { get; set; }

        public string GetCompanyStr()
        {
            string returnVal = "";
            if (Company != null && Company.Count() > 0)
            {
                returnVal = string.Join("  ", Company.Select(q => $"{ParentList.IndexOf(q)}--{q.Attendee.Name}"));
            }
            return returnVal;
        }

        public override string ToString()
        {
            return $"{Attendee}, role: {Role}";
        }
    }
}