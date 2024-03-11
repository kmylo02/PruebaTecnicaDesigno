using BusinessLogic.Implement;
using BusinessLogic.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkUnit.Implement;
using WorkUnit.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Api")));

// Agregar el servicio de migraciones de Entity Framework Core
builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<IFactoryLogic, FactoryLogic>();
builder.Services.AddScoped<IFactoryAbstractRepository, FactoryAbstractRepository>();
builder.Services.AddScoped<IFactoryAbstractInfrastructure, FactoryAbstractInfrastructure>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//mapper
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "FrontendUI",
        builder =>
        {
            builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});
var app = builder.Build();
// Obtiene el servicio de DbContext y aplica migraciones
using (var scope = app.Services.CreateScope())
{
    try
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<DataContext>();
        dbContext.Database.GenerateCreateScript();
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
        // Aplicar las migraciones pendientes
        Console.WriteLine("Migraciones aplicadas correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al aplicar migraciones: {ex.Message}");
    }

}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

app.UseCors("FrontendUI");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
