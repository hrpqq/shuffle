using SendGrid.Helpers.Mail;
using System.Text;

namespace shuffle.models
{
    public class GameEmail
    {
        public GameEmail(Player player)
        {
            this.EmailAddress = new EmailAddress(player.Attendee.Email, player.Attendee.Email);
            this.PlainTextContent = $"Your role is {player.Role}.";
            var sb = new StringBuilder();
            sb.AppendLine($"Your role is <strong>{player.Role}</strong>");
            sb.AppendLine($"</br>{player.GameDescription}");
            sb.AppendLine($"</br>{player.RoleDescription}");
            sb.AppendLine($"</br>所有玩家:{player.NameNumMapList}");
            sb.AppendLine($"</br>你的狼人队友:{player.GetCompanyStr()}");
            this.HtmlContent =sb.ToString();
        }

        public string HtmlContent { get; set; }

        public string PlainTextContent { get; set; }

        public EmailAddress EmailAddress { get; set; }
    }
}