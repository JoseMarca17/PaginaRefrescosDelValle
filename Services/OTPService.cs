using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Data;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Services
{
    public class OTPService
    {
        private readonly AppDbContext _db;

        public OTPService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<string> GenerarYGuardarOTPAsync(int idUsuario)
        {
            // Invalida códigos anteriores del mismo usuario
            var anteriores = await _db.CodigosOTP
                .Where(c => c.IdUsuario == idUsuario && !c.Usado)
                .ToListAsync();

            foreach (var ant in anteriores)
                ant.Usado = true;

            var codigo = new Random().Next(100000, 999999).ToString();

            _db.CodigosOTP.Add(new CodigoOTP
            {
                IdUsuario = idUsuario,
                Codigo = codigo,
                FechaExpiracion = DateTime.Now.AddMinutes(5),
                Usado = false
            });

            await _db.SaveChangesAsync();
            return codigo;
        }

        public async Task<bool> ValidarOTPAsync(int idUsuario, string codigo)
        {
            var otp = await _db.CodigosOTP
                .Where(c => c.IdUsuario == idUsuario
                         && c.Codigo == codigo
                         && !c.Usado
                         && c.FechaExpiracion > DateTime.Now)
                .FirstOrDefaultAsync();

            if (otp == null) return false;

            otp.Usado = true;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task LimpiarOTPAsync(int idUsuario)
        {
            // Limpiar el OTP después de uso exitoso
            var otp = await _db.CodigosOTP
                .FirstOrDefaultAsync(o => o.IdUsuario == idUsuario && !o.Usado);
            
            if (otp != null)
            {
                _db.CodigosOTP.Remove(otp);
                await _db.SaveChangesAsync();
            }
        }

        // Método adicional para limpiar OTPs expirados (opcional)
        public async Task LimpiarOTPsExpiradosAsync()
        {
            var expirados = await _db.CodigosOTP
                .Where(o => o.FechaExpiracion < DateTime.Now && !o.Usado)
                .ToListAsync();

            if (expirados.Any())
            {
                _db.CodigosOTP.RemoveRange(expirados);
                await _db.SaveChangesAsync();
            }
        }
    }
}