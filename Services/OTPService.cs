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
    }
}