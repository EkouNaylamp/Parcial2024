using Microsoft.EntityFrameworkCore; // Para el uso de DbContext
using Parcial2024.Data; // Ajusta esto según tu espacio de nombres

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Cambia "DefaultConnection" si es necesario

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Asegúrate de tener la vista Error.cshtml
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Remesas}/{action=Listar}/{id?}"); // Cambia esto si deseas que la vista predeterminada sea Remesas/Listar

app.Run();
