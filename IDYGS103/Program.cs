using IDYGS103.Context;
using IDYGS103.Services.Interfaces;
using IDYGS103.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IUsuarioServices, UsuarioService>();
builder.Services.AddTransient<IRolesServices, RolesServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Genera el documento JSON en /openapi/v1.json
    app.MapOpenApi();

    // Habilita la interfaz gráfica de Swagger UI
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/api/saludo", () => "¡Hola desde .NET 10!");
app.MapControllers();

app.Run();