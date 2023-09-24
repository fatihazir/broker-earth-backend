using System;
using System.Net;
using System.Net.Mail;
using Core.Utilities.Results;

namespace Business.CCS
{
	public static class MailHelper
	{
		public static IResult SendMail(string receiverMail, string subject, string body)
		{
            string senderMailAddress = "fatih@poseisoft.com";
            string senderMailPassword = "FatihPassword35..";

            string smptLink = "smtppro.zoho.eu";
            int smtpPort = 587;

            var smtpClient = new SmtpClient(smptLink, smtpPort)
            {
                Credentials = new NetworkCredential(senderMailAddress, senderMailPassword),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(senderMailAddress, receiverMail, subject, body);
                return new SuccessResult("Mail sent");

            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }

                return new ErrorResult(errorMessage);
            }
        }
	}
}

