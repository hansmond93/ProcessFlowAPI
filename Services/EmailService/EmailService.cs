using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;
        private readonly IConfiguration _config;
        public EmailService(IOptions<EmailConfig> emailConfig, IConfiguration config)
        {
            _emailConfig = emailConfig.Value;
            _config = config;
        }

        public async Task SendEmail(string email, string subject, string message)
        {
            using(var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _config.GetSection("EmailSettings:Username").Value,
                    Password = _config.GetSection("EmailSettings:Password").Value
                };

                client.UseDefaultCredentials = false;
                client.Credentials = credential;
                client.Host = _config.GetSection("EmailSettings:Host").Value;
                client.Port = Int32.Parse(_config.GetSection("EmailSettings:Port").Value);
                client.EnableSsl = true;    //add this to email config
               


                using(var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_config.GetSection("EmailSettings:From").Value);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;

                    try
                    {
                        client.Send(emailMessage);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    
                }
            }
            await Task.CompletedTask;
        }
    }
}
