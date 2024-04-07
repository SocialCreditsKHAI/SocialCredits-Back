using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using SocialCredits.Domain;
using SocialCredits.Domain.Models;
using System.Text;
using SocialCredits.Services.Interfaces;
using SocialCredits.Services;
using SocialCredits.Repositories.Interfaces;
using SocialCredits.Repositories.Repository;

namespace SocialCredits_Back
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IMongoDbSettings>(sp =>
            {
                var conf = builder.Configuration.GetSection("MongoDbConnection");
                return new MongoDbSettings
                {
                    ConnectionString = conf.GetValue<string>("ConnectionString")!,
                    DatabaseName = conf.GetValue<string>("DatabaseName")!
                };
            });
            
            
            builder.Services.AddScoped<IUserServices, UserService>();
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                {
                    policy.RequireRole("User");
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration.GetSection("JWTSettings:Audience").Value,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSettings:SecretKey").Value!)),
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowSpecificOrigins");
            app.Use(async (context, next) =>
            {
                string token = context.Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token).ToString();
                }
                await next.Invoke();
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseCookiePolicy();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
