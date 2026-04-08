using Microsoft.EntityFrameworkCore;
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
            // Apuntamos directo al DbSet generado por EF
            var anteriores = await _db.CodigosOtps
                .Where(c => c.IdUsuario == idUsuario && !c.Usado)
                .ToListAsync();

            foreach (var ant in anteriores)
                ant.Usado = true;

            var codigo = new Random().Next(100000, 999999).ToString();

            // Usamos la clase con la sintaxis exacta de EF: CodigosOtp
            _db.CodigosOtps.Add(new CodigosOtp
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
            var otp = await _db.CodigosOtps
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
            var otp = await _db.CodigosOtps
                .FirstOrDefaultAsync(o => o.IdUsuario == idUsuario && !o.Usado);
            
            if (otp != null)
            {
                _db.CodigosOtps.Remove(otp);
                await _db.SaveChangesAsync();
            }
        }

        public async Task LimpiarOTPsExpiradosAsync()
        {
            var expirados = await _db.CodigosOtps
                .Where(o => o.FechaExpiracion < DateTime.Now && !o.Usado)
                .ToListAsync();

            if (expirados.Any())
            {
                _db.CodigosOtps.RemoveRange(expirados);
                await _db.SaveChangesAsync();
            }
        }
    }
}