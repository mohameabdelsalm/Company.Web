using Company.Data.Entites;
using Company.Data.Migrations;
using Company.web.Entites;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Net.Mail;

namespace Company.Web.Helper
{
	public static class EmailSetting
	{
		//private MailSettings _options;

		//public EmailSettings(IOptions<MailSettings> options)
		//{
		//	_options = options.Value;
		//}
		public static void SendEmail(SendEmail input)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("abdelsalamm789@gmail.com", "ubzgywfwgbygange");
			client.Send("abdelsalamm789@gmail.com", input.To, input.Subject, input.Body);
		}


		#region MailKit
		//public void SendMail(Email email)
		//{
		//	var mail = new MimeMessage
		//	{
		//		Sender = MailboxAddress.Parse(_options.Email),
		//		Subject = email.Subject,

		//	};

		//	mail.To.Add(MailboxAddress.Parse(email.To));
		//	mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

		//	var builder = new BodyBuilder();
		//	builder.TextBody = email.Body;

		//	mail.Body = builder.ToMessageBody();

		//	using var smtp = new SmtpClient();

		//	smtp.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);

		//	smtp.Authenticate(_options.Email, _options.Password);

		//	smtp.Send(mail);

		//	smtp.Disconnect(true); 
		#endregion


}
}

