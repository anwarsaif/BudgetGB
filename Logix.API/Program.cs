using Autofac;
using Autofac.Extensions.DependencyInjection;
using Logix.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Logix.Application.Extensions;
using Logix.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
var configration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
builder.Services.AddPresistence(configration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{

    // ================= Application =======================
    builder.RegisterModule(new Logix.Application.DI.MainModule());

    // ====================== Infrastrucure ==========================
    builder.RegisterModule(new Logix.Infrastructure.DI.MainModule());



});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//================================ for authentication ============================
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
var secret = appSettingsSection.GetValue<string>("Secret");
var key = Encoding.UTF8.GetBytes(secret ?? "123");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "LogixMobile",
        ValidateAudience = true,
        ValidAudience = "LogixSever",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NetworkCard API",
        Version = "v1",
        Description = " API Services.",
        Contact = new OpenApiContact
        {
            Name = "Najmaldeen Alkamel"
        },
    });

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] {}
                }
            });
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<CustomAuthMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
