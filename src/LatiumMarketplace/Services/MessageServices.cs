using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

using System.Threading.Tasks;



namespace LatiumMarketplace.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Latium", "latiummarketplace@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };


            using (var client = new SmtpClient())
            {
                client.LocalDomain = "localhost:44376";
                await client.ConnectAsync("smtp.gmail.com", 465).ConfigureAwait(false);
                await client.AuthenticateAsync("latiummarketplace@gmail.com", "lmp123456!").ConfigureAwait(false);

                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }

        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
