using API;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Microsoft.CodeAnalysis.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebAPI.Extensions;
using WebAPI.Extensions.AutoMapper;
using WebAPI.Services.CloudStorageService;
using WebAPI.Utils.ConfigOptions;

var builder = WebApplication.CreateBuilder(args);


// Config muiltyple langauge
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.ConfigureSqlServiceContext(builder.Configuration);
builder.Services.ConfigureRepositoryWrapper();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//config gg clound storage
builder.Services.Configure<GCSConfigOptions>(builder.Configuration);
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//config identityserver
builder.Services.AddIdentityServer()
         .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
         .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
        .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
        .AddTestUsers(InMemoryConfig.GetUsers())
        .AddInMemoryClients(InMemoryConfig.GetClients())
        .AddDeveloperSigningCredential(); ;

builder.Services.AddAuthentication("Bearer")
   .AddJwtBearer("Bearer", opt =>
   {
       opt.RequireHttpsMetadata = false;
       opt.Authority = "https://localhost:5001";
       opt.Audience = "companyApi";
   });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "SampleAPI");
    });
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

var app = builder.Build();

var supportedCultures = new[] { "en-US", "ar" };
var localizationOptions =
    new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthUsePkce();
    });
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseIdentityServer();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
