using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace ZavršniRad.Areas.ModulAdministrator.Models
{
    public class EmailServices
    {
       public static async Task SendEmailAsync(string email,string subject,string message)
        {
            try
            {
                var _email = "medinatest@hotmail.com";
                var _epass = ConfigurationManager.AppSettings["EmailPassword"];
                var _dispName = "Stomatološka ordinacija dr. Kebo";
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(_email);
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;

                using(SmtpClient smtp=new SmtpClient())
                {
                    smtp.Host = "smtp.live.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);

                }
              
                
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}