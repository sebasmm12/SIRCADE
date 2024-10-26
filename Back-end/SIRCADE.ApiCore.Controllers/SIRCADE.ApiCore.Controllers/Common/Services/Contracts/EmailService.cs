using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.ApiCore.Controllers.Common.Services.Contracts;

public class EmailService
    (IConfiguration configuration) : IEmailService
{
    public async Task SendAsync(UserNotification notifications)
    {
        using var client = new SmtpClient();

        await client.ConnectAsync(configuration["EmailConfiguration:SmtpClient"], int.Parse(configuration["EmailConfiguration:Port"]!), true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        await client.AuthenticateAsync(configuration["EmailConfiguration:SenderEmail"], configuration["EmailConfiguration:SenderPassword"]);

        var mailMessage = new MimeMessage();
        mailMessage.From.Add(MailboxAddress.Parse(configuration["EmailConfiguration:SenderEmail"]));
        mailMessage.To.Add(MailboxAddress.Parse(notifications.ReceiverUser.Detail.Email));
        mailMessage.Subject = notifications.Subject;
        mailMessage.Body = new TextPart("plain") { Text = notifications.Message };

        await client.SendAsync(mailMessage);

        await client.DisconnectAsync(true);
    }
}