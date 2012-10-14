using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace csharpEmailClass
{
    public class Email
    {
        /// <summary>
        /// Email contstuct.  Asks for the compoents to build an email.
        /// </summary>
        /// <param name="CurrentEmailAddresses">A list string of email addresses</param>
        /// <param name="CurrentAttachmentList">A list of instances of an object that contains a Memorystream attachment.</param>
        /// <param name="CurrentEmailSujbect">Email subject.</param>
        /// <param name="CurretEmailBody">Email body.</param>
        /// <param name="CurrentEmailCss">CSS to go into the email head.</param>
        /// <param name="CurrentEmailFrom">Email from address.</param>
        public Email(List<string> CurrentEmailAddresses, List<EmailAttachment> CurrentAttachmentList, string CurrentEmailSujbect, string CurretEmailBody, string CurrentEmailCss, string CurrentEmailFrom)
        {
            emailAddresses = CurrentEmailAddresses;
            emailCss = CurrentEmailCss;
            emailSubject = CurrentEmailSujbect;
            emailBody = CurretEmailBody;
            attachmentList = CurrentAttachmentList;
            emailFrom = CurrentEmailFrom;
        }

        private string emailFrom;
        private List<EmailAttachment> attachmentList;
        private string emailSubject;
        private string emailBody;
        private List<string> emailAddresses;
        private string emailCss;
        public string ErrorMessage = "";

        /// <summary>
        /// Method to send the email built from the parameters above.
        /// </summary>
        /// <returns>True if email sends, returns false if email fails.</returns>
        public bool SendEmail()
        {
            MailMessage MyMailMessage = new MailMessage();
            MyMailMessage.From = new MailAddress(emailFrom);
            MyMailMessage.ReplyTo = new MailAddress(emailFrom);

            foreach (string emailAddress in emailAddresses)
            {
                try
                {
                    MyMailMessage.To.Add(emailAddress);
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.ToString();
                    return false;
                }

            } //loops through email address list

            MyMailMessage.Subject = emailSubject;

            if (attachmentList.Count >= 1)
            {

                foreach (EmailAttachment EmailAttachment in attachmentList)
                {
                    if (EmailAttachment.AttachmentMemoryStream != null)
                    {
                        //Attachment//
                        string AttachmentFileName = EmailAttachment.AttachmentFileInfo.Name;
                        Attachment MyEmailAttachment = new Attachment(EmailAttachment.AttachmentMemoryStream, AttachmentFileName);
                        MyEmailAttachment.ContentDisposition.FileName = AttachmentFileName;
                        MyMailMessage.Attachments.Add(MyEmailAttachment);
                    }
                }
            }

            string MailBodyHtml = "<!DOCTYPE HTML>" +
                "<html>" +
                "<head>" +
                "<style type=\"text/css\">" +
                emailCss +
                "</style>" +
                "</head><body><div id=\"emailBody\">" + emailBody + "</div>" +
                "</body></html>";

            string MailBodyPlain = emailBody;

            AlternateView plainView = AlternateView.CreateAlternateViewFromString(MailBodyPlain, null, "text/plain");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(MailBodyHtml, null, "text/html");

            MyMailMessage.AlternateViews.Add(plainView);
            MyMailMessage.AlternateViews.Add(htmlView);


            SmtpClient MySmtpClient = new SmtpClient(ConfigurationManager.AppSettings["smtpClient"]);
            try
            {
                MySmtpClient.Send(MyMailMessage);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                return false;
            }
        }
    }
}