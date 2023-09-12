namespace Backend_EctoTec_API.Email
{
    public class MailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string To { get; set; }
    }
}
