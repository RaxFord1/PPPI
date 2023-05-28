using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using PracticeAPI.Services.AuthService;
using PracticeAPI.Services.CharacterService;
using PracticeAPI.Services.GameAccountService;
using PracticeAPI.Services.PasswordService;
using PracticeAPI.Services.QuestService;
using PracticeAPI.Services.VersionedServices.V1;
using PracticeAPI.Services.VersionedServices.V2;
using PracticeAPI.Services.VersionedServices.V3;
using System.Text;
using PracticeAPI.Data;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddDbContext<PracticeContext>();

builder.Services.AddScoped<ICharacterService, CharacterService>(); // add singleton to save data state between requests
builder.Services.AddScoped<IQuestService, QuestService>();
builder.Services.AddScoped<IGameAccountService, GameAccountService>();
builder.Services.AddSingleton<IPasswordService, PasswordService>(); // singleton to be consumed by other singleton services
builder.Services.AddSingleton<IAuthService, AuthService>();

builder.Services.AddScoped<INumberService, NumberService>();
builder.Services.AddScoped<IStringService, StringService>();
builder.Services.AddScoped<IExcelFileService, ExcelFileService>();

//builder.Services.AddApiVersioning(apiVersioningOptions =>
//{
//    apiVersioningOptions.ApiVersionReader = new UrlSegmentApiVersionReader();
//});
//builder.Services.AddVersionedApiExplorer();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Authorization:TokenKey").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Practice API v1.0", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Practice API v2.0", Version = "v2" });
    c.SwaggerDoc("v3", new OpenApiInfo { Title = "Practice API v3.0", Version = "v3" });

    c.AddSecurityDefinition(
        name: "token",
        securityScheme: new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization
        }
    );
    c.AddSecurityRequirement(
        securityRequirement: new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "token"
                    },
                },
                Array.Empty<string>()
            }
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Practice API V1.0");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Practice API V2.0");
        options.SwaggerEndpoint("/swagger/v3/swagger.json", "Practice API V3.0");

        options.RoutePrefix = "swagger";
        options.DocumentTitle = "Practice API";
    });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

//app.UseApiVersioning();

app.MapControllers();

app.Run();
