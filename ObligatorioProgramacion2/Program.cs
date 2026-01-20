using Microsoft.Data.SqlClient;
using AccesoDatos;

var builder = WebApplication.CreateBuilder(args);

// 🔹 NECESARIO para Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 🔹 SQL Connection
builder.Services.AddScoped<SqlConnection>(sp =>
    new SqlConnection(
        builder.Configuration.GetConnectionString("EventosDB")
    ));

// 🔹 Repositorios
builder.Services.AddScoped<ClienteRepositorio>();
builder.Services.AddScoped<EmpleadoRepositorio>();
builder.Services.AddScoped<EventoRepositorio>();
// Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // 🔥 OBLIGATORIO y ANTES de Authorization

app.UseAuthorization();

app.MapRazorPages();
app.Run();
