using Microsoft.EntityFrameworkCore;
using SuperRH.Data; // Certifique-se que o namespace do seu AppDbContext é este

var builder = WebApplication.CreateBuilder(args);

// 1. Configura a conexão com o SQL Server Express
// Importante: Deve vir ANTES do builder.Build()
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Importante para carregar CSS/JS da pasta wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// No .NET 9+, o MapStaticAssets substitui o UseStaticFiles para otimização, 
// mas manter UseStaticFiles acima garante compatibilidade com seu layout atual.
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();