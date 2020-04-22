using System;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using shuffle.models;

namespace shuffle
{
    public class NotificationService
    {
        private readonly SendGridClient _client;
        private readonly EmailAddress _from;

        public NotificationService()
        {
            var emailSetting = new SendGridSettingService().GetSetting();
            _client = new SendGridClient(emailSetting.API_KEY);
            _from = new EmailAddress(emailSetting.FromEmail, emailSetting.FromName);
        }

        public async Task Notify(Game game)
        {
            var subject = ComposeSubject(game.Round);
            var emails = game.Players.Select(player => new GameEmail(player));
            var sendMsgTasks = emails.Select(email =>
                    MailHelper.CreateSingleEmail(_from, email.EmailAddress, subject, email.PlainTextContent, email.HtmlContent))
                .Select(msg => _client.SendEmailAsync(msg))
                .ToList();
            var response = await Task.WhenAll(sendMsgTasks);
        }

        public static string ComposeSubject(int round)
        {
            return $"Werewolf kill round {round}";
        }
    }
}