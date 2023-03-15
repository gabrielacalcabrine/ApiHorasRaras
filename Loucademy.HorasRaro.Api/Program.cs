using Loucademy.HorasRaro.API.Handlers;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Settings;
using Loucademy.HorasRaro.IoC;
using Loucademy.HorasRaro.Repository.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();


builder.Services
    .AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

var connectionString = builder.Configuration.GetConnectionString("Loucademy.HorasRaro.Api"); // variável local string para conexão com banco
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json").Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config).CreateLogger();
//talvez necessite da configuracao try catch em caso de possivel falha

builder.Services.AddRouting(options => options.LowercaseUrls = true);

NativeInjectorBootStrapper.RegisterAppDependencies(builder.Services); // configuraçao para buildar injeção de dependencias
NativeInjectorBootStrapper.RegisterAppDependenciesContext(builder.Services, connectionString); // configuração para buildar conexao com banco de dados

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1",
                       new Microsoft.OpenApi.Models.OpenApiInfo
                       {
                           Title = "Loucademy HorasRaro",
                           Version = "v1",
                           Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Loucademy" },
                           TermsOfService = new Uri("https://gitlab.com/gabrielacalcabrine/loucademy_horas_raro")
                       });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "Bearer"
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    //swagger.AddSecurityDefinition
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    swagger.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = true;
}).AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationHandler>();

var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
builder.Services.AddSingleton(appSettings);

var emailSettings = builder.Configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>();
builder.Services.AddSingleton(emailSettings);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.JwtSecurityKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddHttpClient<ITogglRepository, TogglRepository>(options =>
{
    options.BaseAddress = new Uri(appSettings.TogglApi); //configuração na program do toggl para integração
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();