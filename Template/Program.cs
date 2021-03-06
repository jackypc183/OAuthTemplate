using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Template;
using Template.Dal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    string commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
    string commentsFile = Path.Combine(baseDirectory, commentsFileName);
    c.IncludeXmlComments(commentsFile);
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                       {
                                         new OpenApiSecurityScheme
                                         {
                                           Reference = new OpenApiReference
                                           {
                                             Type = ReferenceType.SecurityScheme,
                                             Id = "Bearer"
                                           }
                                          },
                                          new string[] { }
                                        }
                                      });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TemplateDBContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddSingleton<JwtHelpers>(); 
builder.Services
     .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
        // ?????????????A?^?????Y?|?]?t WWW-Authenticate ???Y?A?o???|?????????????????~???]
        options.IncludeErrorDetails = true; // ?w?]???? true?A?????|?S?O????

        options.TokenValidationParameters = new TokenValidationParameters
         {
            // ?z?L?o?????i?A?N?i?H?q "sub" ???????]?w?? User.Identity.Name
            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            // ?z?L?o?????i?A?N?i?H?q "roles" ?????A???i?? [Authorize] ?P?_????
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

            // ?@?????????|???? Issuer
            ValidateIssuer = true,
             ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),

            // ?q?`???????n???? Audience
            ValidateAudience = false,
            //ValidAudience = "JwtAuthDemo", // ???????N?????n???g

            // ?@?????????|???? Token ??????????
            ValidateLifetime = true,

            // ?p?G Token ???]?t key ?~???n?????A?@?????u?????????w
            ValidateIssuerSigningKey = false,

            // "1234567890123456" ?????q IConfiguration ???o
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey")))

         };
     });
IoCConfig.Configure(builder.Configuration, builder.Services);
builder.Services.AddCors();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
