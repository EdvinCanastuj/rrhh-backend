using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net.Mail;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;

    }



    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Notificaciones", "edvinjcv66@gmail.com"));
            message.To.Add(new MailboxAddress("Anonimo", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                var smtpServer = _configuration["SmtpSettings:Server"];
                var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
                var smtpUsername = _configuration["SmtpSettings:Username"];
                var smtpPassword = _configuration["SmtpSettings:Password"];
                var useSsl = bool.Parse(_configuration["SmtpSettings:UseSsl"]);

                Console.WriteLine($"Use SSL: {useSsl}");

                // Usa el puerto 587 y SecureSocketOptions.None
                await client.ConnectAsync(smtpServer, smtpPort, useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls, cancellationToken: default);


                // Desactivar la validación del certificado SSL/TLS de forma temporal para pruebas
                ///Console.WriteLine("Desactivando temporalmente la validación del certificado SSL/TLS");
                Console.WriteLine("Activando  la validación del certificado SSL/TLS");
                //client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.CheckCertificateRevocation = false; // Deshabilitar la verificación de revocación del certificado


                await client.AuthenticateAsync(smtpUsername, smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}");
            // Agregar más detalles o registros de depuración según sea necesario
        }
    }
}