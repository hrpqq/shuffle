using SendGrid.Helpers.Mail;

namespace shuffle.models
{
    public class GameEmail
    {
        public GameEmail(Player player)
        {
            this.EmailAddress = new EmailAddress(player.Attendee.Email, player.Attendee.Email);
            this.PlainTextContent = $"Your role is {player.Role}.";
            this.HtmlContent = $"Your role is <strong>{player.Role}</strong></br>{player.GameDescription}</br>{player.RoleDescription}";
        }

        public string HtmlContent { get; set; }

        public string PlainTextContent { get; set; }

        public EmailAddress EmailAddress { get; set; }
    }
}