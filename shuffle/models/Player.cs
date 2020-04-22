namespace shuffle.models
{
    public class Player
    {
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

        public override string ToString()
        {
            return $"{Attendee}, role: {Role}";
        }
    }
}