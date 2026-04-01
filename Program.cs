using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Data;
using RefrescosDelValle.Services;

var builder = WebApplication.CreateBuilder(args);

// ========== CONFIGURACIÓN DE SERVICIOS ==========

// MVC
builder.Services.AddControllersWithViews();

// Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ========== AUTENTICACIÓN Y AUTORIZACIÓN ==========
// Configuración de autenticación por cookies (UNA SOLA VEZ)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

// Configuración de autorización con políticas por rol
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => 
        policy.RequireRole("SuperAdmin", "AdminSeguridad", "AdminProduccion", "AdminInventario", "AdminVentas", "AdminCompras", "AdminRRHH"));
    
    options.AddPolicy("Seguridad", policy => 
        policy.RequireRole("SuperAdmin", "AdminSeguridad"));
    
    options.AddPolicy("Produccion", policy => 
        policy.RequireRole("SuperAdmin", "AdminProduccion"));
    
    options.AddPolicy("Inventario", policy => 
        policy.RequireRole("SuperAdmin", "AdminInventario"));
    
    options.AddPolicy("Ventas", policy => 
        policy.RequireRole("SuperAdmin", "AdminVentas"));
    
    options.AddPolicy("Compras", policy => 
        policy.RequireRole("SuperAdmin", "AdminCompras"));
    
    options.AddPolicy("RRHH", policy => 
        policy.RequireRole("SuperAdmin", "AdminRRHH"));
});

// Servicios propios
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<OTPService>();

// ========== CONSTRUCCIÓN DE LA APLICACIÓN ==========
var app = builder.Build();

// ========== CONFIGURACIÓN DEL PIPELINE ==========

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// IMPORTANTE: Authentication y Authorization deben ir en este orden
app.UseAuthentication(); // PRIMERO autenticación
app.UseAuthorization();  // LUEGO autorización

// Mapeo de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard/{action=Index}/{id?}",
    defaults: new { controller = "Dashboard" });

// ========== SEED DATA (CREAR USUARIO ADMIN SI NO EXISTE) ==========
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Aplicar migraciones automáticamente
    db.Database.Migrate();

    // Crear usuario administrador si no existe
    if (!db.Usuarios.Any())
    {
        db.Usuarios.Add(new RefrescosDelValle.Models.Entities.Usuario
        {
            NombreCompleto = "Administrador",
            Email = "marcajose1703@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            Rol = "SuperAdmin",  // Cambiado de "Admin" a "SuperAdmin" para que tenga todos los permisos
            Activo = true,
            FechaCreacion = DateTime.Now
        });
        db.SaveChanges();
    }
}

app.Run();