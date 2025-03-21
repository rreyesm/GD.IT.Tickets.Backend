using System.Text;
using System.Text.Json;
using GD.RequestSystem.DAL.EF;
using GD.RequestSystem.Entities.AuthModels;
using GD.RequestSystem.Entities.Shared;
using GD.RequestSystem.WebAPI.Services;
using GD.RequestSystem.DAL.EF; 
using GD.RequestSystem.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Para Db context configuracion del Appseting y el JWT
var appSettingsSection = builder.Configuration.GetSection("JWT");
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddOptions();
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TickeApi", Version = "v2" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description = "Introduce el token JWT en el formato: Bearer[space]Token",
        In = ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Configuración JWT
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("*") // Permite solicitudes desde este origenhttps:http://localhost:44396
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        }
    );
});

#region Dbcontext
builder.Services.AddDbContext<RequestSystemDbContext>();
builder.Services.AddDbContext<GeneralDbContext>();
#endregion Dbcontext
#region Servicios
builder.Services.AddScoped<IUserService, UserService>();//Autenticacion
builder.Services.AddScoped<ITicketService, TicketService>();//General Ticket
builder.Services.AddScoped<IPetitionService, PetitionService>();//Peticiones
builder.Services.AddScoped<IResponseService, ResponseService>();//Respuestas
builder.Services.AddScoped<IFilterService, FilterService>();//Filtros
builder.Services.AddScoped<IStatusService, StatusService>();//Status
builder.Services.AddScoped<IRolesService, RolesService>();//User Roles
builder.Services.AddScoped<IDocumentService, DocumentService>();//Documentos


#endregion Servicios

var app = builder.Build();

// Aplica la política CORS antes de otros middlewares
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
