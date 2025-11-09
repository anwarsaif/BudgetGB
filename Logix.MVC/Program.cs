using Autofac;
using Autofac.Extensions.DependencyInjection;
using DinkToPdf.Contracts;
using DinkToPdf;
using Logix.Application.Common;
using Logix.Application.Extensions;
using Logix.Application.Helpers;
using Logix.Application.Helpers.Acc;
using Logix.Infrastructure.Extensions;
using Logix.MVC.Filters;
using Logix.MVC.Helpers;
using Logix.MVC.Services.CsvService;
using Logix.MVC.Services.ExcelService;
using Logix.MVC.Services.ExportService;
using Logix.MVC.Services.HtmlService;
using Logix.MVC.Services.JsonService;
using Logix.MVC.Services.XmlService;
using Logix.MVC.Services.YamlService;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using QuestPDF.Infrastructure;
//using QuestPDF.Drawing;

QuestPDF.Settings.License = LicenseType.Community;
//FontManager.RegisterFont(new FileStream("wwwroot/fonts/NotoNaskhArabic-Bold.ttf", FileMode.Open));
//FontManager.RegisterFont(new FileStream("wwwroot/fonts/NotoNaskhArabic-Regular.ttf", FileMode.Open));
var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.AllowSynchronousIO = true;
//});


ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
// Add services to the container.

builder.Services.AddControllersWithViews(
    options =>
    {
        options.Filters.Add<ModifyDecimalInputsAttribute>();
    })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();



builder.Services.AddApplication();
var configration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
builder.Services.AddPresistence(configration);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

/*var key = Encoding.UTF8.GetBytes("hdb32wsjmncghtjkmdlket54edw3uizp");
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
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
*/
//builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Logix API",
        Version = "v1",
        Description = " API Services.",
        Contact = new OpenApiContact
        {
            Name = "Logix Contact"
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

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{

    // ================= Application =======================
    builder.RegisterModule(new Logix.Application.DI.MainModule());

    // ====================== Infrastrucure ==========================
    builder.RegisterModule(new Logix.Infrastructure.DI.MainModule());  

    

});

builder.Services.AddScoped<IExportService, ExportService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IHtmlService, HtmlService>();
builder.Services.AddScoped<IJsonService, JsonService>();
builder.Services.AddScoped<IXmlService, XmlService>();
builder.Services.AddScoped<IYamlService, YamlService>();

// ================== Helpers Registeration ===================================
builder.Services.AddScoped<IDDListHelper, DDListHelper>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<IApiDDLHelper, ApiDDLHelper>();
builder.Services.AddScoped<IFilesHelper, FilesHelper>();
builder.Services.AddTransient<ISessionHelper, SessionHelper>();
builder.Services.AddTransient<IPermissionHelper, PermissionHelper>();
builder.Services.AddTransient<ISysConfigurationHelper, SysConfigurationHelper>();
builder.Services.AddTransient<IScreenPropertiesHelper, ScreenPropertiesHelper>();
builder.Services.AddTransient<IGetAccountIDByCodeHelper, GetAccountIDByCodeHelper>();
builder.Services.AddTransient<IGetRefranceByCodeHelper, GetRefranceByCodeHelper>();
builder.Services.AddTransient<IGetAccJournaCodeHelper, GetAccJournaCodeHelper>();
builder.Services.AddTransient<IEmailService, EmailService>();
// ==================Application Layer Helpers Registeration ===================================
builder.Services.AddTransient<ISysConfigurationAppHelper, SysConfigurationAppHelper>();
builder.Services.AddTransient<Logix.MVC.Helpers.IWorkflowHelper, Logix.MVC.Helpers.WorkflowHelper>();
builder.Services.AddTransient<Logix.Application.Helpers.IWorkflowHelper,  Logix.Application.Helpers.WorkflowHelper >();
builder.Services.AddTransient<IEmailAppHelper, EmailAppHelper>();


builder.Logging.ClearProviders();
builder.Logging.AddConsole(options => { options.LogToStandardErrorThreshold = LogLevel.Warning;
options.IncludeScopes = true;
}) ;
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);



//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

var localizationService = app.Services.GetService<ILocalizationService>();
localizationService.ConfigureLocalization(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());
    app.UseDeveloperExceptionPage();
  
    //app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());

    //app.UseSwagger();
    //app.UseSwaggerUI();



    app.UseExceptionHandler("/Home/Error");

}
app.UseStaticFiles();

app.UseRouting(); 
app.UseSession();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<SessionMiddleware>();
app.UseAuthentication();
//app.UseCookiePolicy();
app.UseAuthorization();
app.UseRequestLocalization();
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();
app.Run();
