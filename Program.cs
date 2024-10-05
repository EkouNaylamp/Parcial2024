using Microsoft.EntityFrameworkCore; // Para el uso de DbContext
using Parcial2024.Data; // Ajusta esto según tu espacio de nombres
using Parcial2024.Services; // Esto es para registrar el servicio CoinGecko

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores con vistas
builder.Services.AddControllersWithViews();

// Configuración de la base de datos (PostgreSQL)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de CoinGeckoService en el contenedor de dependencias
builder.Services.AddHttpClient<CoinGeckoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Enrutamiento predeterminado
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Remesas}/{action=Listar}/{id?}");

app.Run();