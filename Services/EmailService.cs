using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;


namespace RoastHiveMvc.Services
{
    public class EmailService
    {
        private readonly string _apiKey;

        public EmailService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("inforoasthive@gmail.com", "Roast Hive"),
                Subject = subject,
                PlainTextContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Add the auto-reply logic
            var autoReplyMessage = @"
            Dear Customer,

            Thank you for contacting Roast Hive. This is an automated response to confirm that we have received your message and our team is working on providing you with a comprehensive reply.

            We value your time and strive to respond to all inquiries within 24 hours. If you have any further questions or need immediate assistance, please feel free to contact us at [email protected]

            Sincerely,
            Roast Hive Team";

            msg.AddBcc(new EmailAddress("inforoasthive@gmail.com")); // Add a BCC address for the auto-reply
            msg.SetReplyTo(new EmailAddress("inforoasthive@gmail.com")); // Set the reply-to address for the auto-reply
            msg.PlainTextContent = autoReplyMessage;

            var response = await client.SendEmailAsync(msg);

            // Handle the response if needed
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                // Error occurred while sending email
                // Handle accordingly
            }
        }
    }
}