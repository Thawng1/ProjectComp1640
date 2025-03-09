
﻿using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _config;
    private readonly IConfigurationSection _emailSettings;

    public EmailService(IConfiguration config)
    {
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

 
}