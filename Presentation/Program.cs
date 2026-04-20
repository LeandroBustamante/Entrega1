using Application.Interfaces.Information;
using Application.Interfaces.Reservations;
using Application.Services;
using Application.UseCase;
using Application.UseCases.Events.GetEvents;
using Application.UseCases.Reservations.CreateReservation;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURACION DE CORS PARA PERMITIR PETICIONES DESDE EL FRONTEND
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// CONFIGURACION DE SWAGGER PARA GENERAR LA DOCUMENTACION DE LA API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // INCLUIR COMENTARIOS XML SI EXISTEN PARA MOSTRARLOS EN SWAGGER
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// CONFIGURACION DEL DB CONTEXT CON SQL SERVER
// CONFIGURACION DEL DB CONTEXT LEYENDO EL APPSETTINGS
builder.Services.AddDbContext<EventContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// INYECCION DE DEPENDENCIAS: REGISTRAMOS NUESTRAS CAPAS 
builder.Services.AddScoped<IEventQuery, EventQuery>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IReservationCommand, ReservationCommand>();
builder.Services.AddScoped<IReservationService, ReservationService>();
// Registramos el nuevo caso de uso (Handler)
builder.Services.AddScoped<ICreateReservationHandler, CreateReservationHandler>();
builder.Services.AddScoped<ICreateReservationHandler, CreateReservationHandler>();
builder.Services.AddScoped<IGetEventsHandler, GetEventsHandler>();

var app = builder.Build();
Console.WriteLine("hola mundo");

// CONFIGURAR EL PIPELINE DE HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ACTIVAR CORS PARA QUE EL FRONTEND PUEDA CONSUMIR LA API
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();