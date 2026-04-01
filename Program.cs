using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Data;
using RefrescosDelValle.Services;

var builder = WebApplication.CreateBuilder(args);

// ========== CONFIGURACIÓN DE SERVICIOS ==========
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ========== AUTENTICACIÓN Y AUTORIZACIÓN ==========
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
    
    options.AddPolicy("Seguridad", policy => policy.RequireRole("SuperAdmin", "AdminSeguridad"));
    options.AddPolicy("Produccion", policy => policy.RequireRole("SuperAdmin", "AdminProduccion"));
    options.AddPolicy("Inventario", policy => policy.RequireRole("SuperAdmin", "AdminInventario"));
    options.AddPolicy("Ventas", policy => policy.RequireRole("SuperAdmin", "AdminVentas"));
    options.AddPolicy("Compras", policy => policy.RequireRole("SuperAdmin", "AdminCompras"));
    options.AddPolicy("RRHH", policy => policy.RequireRole("SuperAdmin", "AdminRRHH"));
});

// Servicios propios
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<OTPService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard/{action=Index}/{id?}",
    defaults: new { controller = "Dashboard" });

// ========== INICIALIZACIÓN SEGURA DE DATOS (SEEDER) ==========
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Llamamos a nuestro inicializador seguro en lugar de hardcodear aquí
        await DbInitializer.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        // En un entorno real, aquí usarías un ILogger para registrar el fallo crítico
        Console.WriteLine($"Error crítico al inicializar la BD: {ex.Message}");
    }
}

app.Run();