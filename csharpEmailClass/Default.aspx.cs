using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csharpEmailClass
{
    public partial class Default : System.Web.UI.Page
    {
        string ErrorMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblResult.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (tbEmailAddress.Text != "")
            {
                if (sendEmail())
                {
                    lblResult.Text = "Email sent!";
                    lblResult.Visible = true;
                }
                else
                {
                    lblResult.Text = "Email failed...<br />" + ErrorMessage;
                    lblResult.Visible = true;
                }
            }
            else
            {
                lblResult.Text = "no emaila address entered!";
            }
        }

        private bool sendEmail()
        {
            List<string> MyEmailList = new List<string>();
            MyEmailList.Add(tbEmailAddress.Text);
            EmailAttachment MyEmailAttachment = new EmailAttachment();

            using (FileStream fileStream = File.OpenRead(Server.MapPath("/AttachmentExample.txt")))
            {
                MemoryStream memStream = new MemoryStream();
                memStream.SetLength(fileStream.Length);
                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
                MyEmailAttachment.AttachmentMemoryStream = memStream;
                MyEmailAttachment.AttachmentFileInfo = new FileInfo(Server.MapPath("/AttachmentExample.txt"));
            }

            List<EmailAttachment> MyEmailAttachmentList = new List<EmailAttachment>();
            MyEmailAttachmentList.Add(MyEmailAttachment);

            Email MyEmail = new Email(MyEmailList, MyEmailAttachmentList, "Test subject", "Test body", null, ConfigurationManager.AppSettings["EmailFrom"]);
            if (!MyEmail.SendEmail())
            {
                ErrorMessage = MyEmail.ErrorMessage;
                return false;
            }
            return true;
        }
    }
}