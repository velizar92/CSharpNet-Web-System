namespace CSharpNet_Web_System.Services.Email
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                // TODO: Consider making EmailSenderException wrapper or smth.
                // Have in mind also that errors shown to the User should be translated as well if having multiple languages, so something similar "throw new EmailSenderException("error_resource_translation");"
                throw new Exception("Null SendGridKey");
            }
            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);

            // TODO: Replace this one with an official GMAIL account for the website.
            var from = new EmailAddress("velizar9209@gmail.com", subject);
            var to = new EmailAddress(toEmail);          

            var msg = MailHelper.CreateSingleEmail(
                        from,
                        to,
                        subject,
                        message,
                        message
                        );

            // TODO: Research on how to log exceptions in Azure or somewhere else. I.e when email sender is not working.

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            //msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
