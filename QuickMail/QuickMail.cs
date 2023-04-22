using System;
using MimeKit;

namespace QuickMail
{
    public class QuickMail
    {
        const string fromAddress = "username@gmail.com";
        private string sendAddress = "";
        private string bodyText = "";

        public QuickMail()
        {
        }

        public string SendAddress
        {
            set { sendAddress = value; }
            get { return sendAddress; }
        }

        public string BodyText
        {
            set { bodyText = value; }
            get { return bodyText; }
        }

        public async Task SendMailAsync()
        {// ref: https://feeld-uni.com/entry/2022/03/04/074344
            var msg = new MimeMessage();

            // Prepare the sender
            var from = new MailboxAddress("", fromAddress);

            // Set the sender's information (add)
            msg.From.Add(from);

            // Prepare the recipient
            var to = new MailboxAddress("", sendAddress);

            // Set the recipient's information (add)
            msg.To.Add(to);

            // Prepare the Cc
            //var cc = new MailboxAddress("CcName", "CcNameAddress");

            // Set the Cc's information (add)
            //msg.Cc.Add(cc);

            // Set the subject
            string[] bodyTextArray = bodyText.Split("\n", StringSplitOptions.None);
            msg.Subject = bodyTextArray[0];

            // Set the body text
            // Set the format of the text in the body.
            var textPart = new TextPart(MimeKit.Text.TextFormat.Plain);
            textPart.Text = bodyText;

            var multipart = new Multipart("mixed");
            multipart.Add(textPart);
            msg.Body = multipart;

            // Send the email
            using (var sc = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    // Connect to the SMTP server
                    await sc.ConnectAsync("smtp.gmail.com", 587);

                    // Authenticate with SMTP
                    await sc.AuthenticateAsync("username", "pass");

                    // Send the mail
                    await sc.SendAsync(msg);

                    // Disconnect from the SMTP server
                    await sc.DisconnectAsync(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
