using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

public class ContactoController : Controller
{
    private readonly IConfiguration _config;

    public ContactoController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Enviar(string nombre, string apellido, string email, string asunto, string mensaje)
    {
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(mensaje))
            return Json(new { success = false, error = "Completa los campos requeridos." });

        try
        {
            var smtpHost     = _config["EmailSettings:SmtpServer"];
            var smtpPort     = int.Parse(_config["EmailSettings:SmtpPort"]);
            var smtpUser     = _config["EmailSettings:SenderEmail"];
            var smtpPass     = _config["EmailSettings:SenderPassword"];
            var destinatario = _config["EmailSettings:SenderEmail"];

            var cuerpo = $@"
                <h2 style='font-family:sans-serif;color:#C8281E;'>Nuevo mensaje de contacto</h2>
                <table style='font-family:sans-serif;font-size:14px;border-collapse:collapse;width:100%'>
                    <tr><td style='padding:8px;font-weight:bold;color:#555'>Nombre:</td><td style='padding:8px'>{nombre} {apellido}</td></tr>
                    <tr style='background:#f9f9f9'><td style='padding:8px;font-weight:bold;color:#555'>Email:</td><td style='padding:8px'><a href='mailto:{email}'>{email}</a></td></tr>
                    <tr><td style='padding:8px;font-weight:bold;color:#555'>Asunto:</td><td style='padding:8px'>{asunto}</td></tr>
                    <tr style='background:#f9f9f9'><td style='padding:8px;font-weight:bold;color:#555;vertical-align:top'>Mensaje:</td><td style='padding:8px'>{mensaje.Replace("\n", "<br/>")}</td></tr>
                </table>
                <p style='font-family:sans-serif;font-size:12px;color:#aaa;margin-top:24px'>Enviado desde el formulario de contacto · Refrescos del Valle</p>
            ";

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From       = new MailAddress(smtpUser, "Refrescos del Valle · Web"),
                Subject    = $"[Contacto Web] {asunto} — {nombre}",
                Body       = cuerpo,
                IsBodyHtml = true,
                ReplyToList = { new MailAddress(email, nombre) }
            };
            mail.To.Add(destinatario);

            await client.SendMailAsync(mail);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = "Error al enviar. Intenta más tarde." });
        }
    }
}