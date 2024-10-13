using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace DemoPL.Helpers
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("esraasherif9992@gmail.com", "Esraa321.com");
			Client.Send("esraasherif9992@gmail.com", email.To, email.Subject, email.Body);

           

        }

	}
}
