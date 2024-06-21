using bendita_ajuda_back_end.DTOs.EmailSend;
using bendita_ajuda_back_end.Repositories.EmailService;
using System.Net;
using System.Net.Mail;

namespace bendita_ajuda_back_end.Repositories.EmailService
{
	public class EmailService
	{
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(EmailSendDto emailSendDto)
        {
            try
            {
                var username = _configuration["SMTP:Username"];
				var password = _configuration["SMTP:Password"];

                var client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(username, password)
                };

                var message = new MailMessage(from: username, to: emailSendDto.To, subject: emailSendDto.Subject, body: emailSendDto.Body);

                message.IsBodyHtml = true;
                await client.SendMailAsync(message);
                return true;
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

