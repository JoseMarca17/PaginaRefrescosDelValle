using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace RefrescosDelValle.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCodigoOTPAsync(string destinatario, string codigo)
        {
            var settings = _config.GetSection("EmailSettings");

            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Refrescos del Valle", settings["SenderEmail"]));
            mensaje.To.Add(MailboxAddress.Parse(destinatario));
            mensaje.Subject = "Tu código de verificación";

            mensaje.Body = new TextPart("html")
            {
                Text = $@"
                <div style='font-family:Arial,sans-serif;max-width:480px;margin:auto;padding:24px;border:1px solid #ddd;border-radius:8px;'>
                    <h2 style='color:#1a73e8;'>Refrescos del Valle</h2>
                    <p>Tu código de verificación es:</p>
                    <div style='font-size:36px;font-weight:bold;letter-spacing:8px;color:#333;margin:16px 0;'>{codigo}</div>
                    <p style='color:#888;font-size:13px;'>Este código expira en <strong>5 minutos</strong>. No lo compartas con nadie.</p>
                </div>"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(settings["SmtpServer"], int.Parse(settings["SmtpPort"]!), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(settings["SenderEmail"], settings["SenderPassword"]);
            await smtp.SendAsync(mensaje);
            await smtp.DisconnectAsync(true);
        }
    }
}