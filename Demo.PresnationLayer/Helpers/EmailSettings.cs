using Demo.DataAcessLayer.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PresnationLayer.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com",587);  
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("mohye20@gmail.com", "fkid tcgt birf ioks");
            Client.Send("mohye20@gmail.com",email.To,email.Subject,email.Body);

        }
    }
}