using System;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace WebUI.Helpers
{
	public  class EmailHelper
	{
        private  readonly IConfiguration _configuration;

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  void SendConfirmationEmail(string email, string FirstName, string LastName, string confirmationToken)
		{


            var emailAddress = _configuration.GetSection("EmailSettings:EmailAddress").Value;
            var emailPassword = _configuration.GetSection("EmailSettings:Password").Value;
            string senderEmail = emailAddress;
            string senderPassword = emailPassword;

            //Create the SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.ethereal.email", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            try
            {
                // Create the email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail);
                mailMessage.To.Add(email);
                mailMessage.Subject = $"Message from - {email}";
                mailMessage.Body = $"<h1>Hello, {FirstName} {LastName}</h1> <br> <p> <a href='https://localhost:7144/auth/confirmation/{confirmationToken}?email={email}'>Hesabi testiq edin</a></p>";
                // Specify that the body contains HTML
                mailMessage.IsBodyHtml = true;
                //  https://localhost:7144/auth/confirmtaion/43301d2f-7161-4a6d-9b94-cb3b3c9ae527?email=ehmed@compar.edu.az
                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
	}
}

