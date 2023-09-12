using Backend_EctoTec.Core.Entities;
using System.Net.Mail;
using System;
using Microsoft.Extensions.Options;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Buffers.Text;
using System.Net.NetworkInformation;
using System.Net.Mime;

namespace Backend_EctoTec_API.Email
{
    public class SendEmailService
    {
        private readonly MailSettings _mailSettings;
        public SendEmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public void SendEmail(GreenLeavesDto address)
        {
            try
            {
                string head = "<p>Estimado " + "<strong>" + address.Name + "</strong>" + ",</p>";
                string body = "<p>Hemos recibido tus datos y nos pondremos en contacto con usted a la brevedad posible. " +
                    "Enviaremos un correo con información a su cuenta: " + address.Email + "</p>";

                MailMessage oMailMessage = new MailMessage
                {
                    From = new MailAddress(_mailSettings.SmtpUsername),
                    IsBodyHtml = true,
                };
                oMailMessage.To.Add(_mailSettings.To);

                body = $@"
                        <html>
                            <head>
                                <title>{{subject}}</title>
                            </head>
                            <img id=""greenLeaves"" border=""0"" alt=""GreenLeaves"" src=""cid:greenLeaves"" width=""100%"">
                            <body>
                                <p>{head}</p>
                                <p>{body}</p>
                            </body>
                            <footer style=""width:100%; text-align: end;"">
                                <strong style=""margin:0;"">Atte:</strong>
                                <p style=""color: #005400; margin:0;"">Green Leaves</p>
                                <p style=""margin:0;"">{address.Address} a {address.Date}</p>
                            </footer>
                        </ html >
                    ";

                string textBody = body;
                AlternateView plainTextView = AlternateView.CreateAlternateViewFromString(textBody, null, MediaTypeNames.Text.Plain);

                string htmlBody = "<html><body>" + textBody + "</body></html>";
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                
                string imagePathF = @"img/greenLeavesLogo.jpg";//aqui la ruta de tu imagen
                LinkedResource greenLeavesIMG = new LinkedResource(imagePathF);
                greenLeavesIMG.ContentId = "greenLeaves";
                htmlView.LinkedResources.Add(greenLeavesIMG);

                oMailMessage.AlternateViews.Add(htmlView);
                
                oMailMessage.Body = body;

                SmtpClient oSmtpClient = new SmtpClient();
                oSmtpClient.EnableSsl = true;
                oSmtpClient.UseDefaultCredentials = false;
                oSmtpClient.Host = _mailSettings.SmtpServer;
                oSmtpClient.Port = _mailSettings.SmtpPort;
                oSmtpClient.Credentials = new NetworkCredential(_mailSettings.SmtpUsername, _mailSettings.SmtpPassword);


                oSmtpClient.Send(oMailMessage);
                oSmtpClient.Dispose();


                //var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "img", "greenLeavesLogo.jpg");
                /*if (File.Exists(imagePath))
                {
                    var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    var base64Image = Convert.ToBase64String(bytes);
                    
                    stream.Dispose();
                }*/
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
