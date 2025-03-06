using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _config;
    private readonly IConfigurationSection _emailSettings;

    public EmailService(IConfiguration config)
    {
        _config = config;
        _emailSettings = config.GetSection("EmailSettings");
    }


    public async Task SendEmailAsync(string toEmail, string subject, string content)
    {
        var smtpClient = new SmtpClient
        {
            Host = _emailSettings["MailServer"]!,
            Port = int.Parse(_emailSettings["MailPort"]!),
            EnableSsl = true,
            Credentials = new NetworkCredential(_emailSettings["Sender"], _emailSettings["Password"])
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings["Sender"]!, _emailSettings["SenderName"]),
            Subject = subject,
            Body = content,
            IsBodyHtml = true
        };
        mailMessage.To.Add(toEmail);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception)
        {
            throw;
        }
    }

    //public void SendEmail(string toEmail, string subject, string body)
    //{
    //    var smtpServer = _config["EmailSettings:SmtpServer"];
    //    var port = int.Parse(_config["EmailSettings:Port"]);
    //    var senderEmail = _config["EmailSettings:SenderEmail"];
    //    var senderName = _config["EmailSettings:SenderName"];
    //    var senderPassword = _config["EmailSettings:SenderPassword"];

    //    var smtpClient = new SmtpClient(smtpServer)
    //    {
    //        Port = port,
    //        Credentials = new NetworkCredential(senderEmail, senderPassword),
    //        EnableSsl = true
    //    };

    //    var mailMessage = new MailMessage
    //    {
    //        From = new MailAddress(senderEmail, senderName),
    //        Subject = subject,
    //        Body = body,
    //        IsBodyHtml = true
    //    };

    //    mailMessage.To.Add(toEmail);

    //    smtpClient.Send(mailMessage);
    ////}
}
