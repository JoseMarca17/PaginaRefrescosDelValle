using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Data;
using RefrescosDelValle.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Autenticación por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

// Servicios propios
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<OTPService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // ANTES de Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"); // arranca en login

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // aplica migrations automático

    if (!db.Usuarios.Any())
    {
        db.Usuarios.Add(new RefrescosDelValle.Models.Entities.Usuario
        {
            NombreCompleto = "Administrador",
            Email = "marcajose1703@gmail.com", // el tuyo
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            Rol = "Admin",
            Activo = true,
            FechaCreacion = new DateTime(2025, 1, 1)
        });
        db.SaveChanges();
    }
}


app.Run();