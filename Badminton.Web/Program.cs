using Badminton.Web.DTO.Payment.Request;
using Badminton.Web.DTO.Payment.Response;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Badminton.Web.Repository;
using Badminton.Web.Services;
using Badminton.Web.Services.OTP;
using Badminton.Web.VnPay.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.IO;
using System.Text;

namespace Badminton.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // JWT Configuration
            builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));
            var secretKey = builder.Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                    ClockSkew = TimeSpan.Zero,
                };
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.ExpireTimeSpan = TimeSpan.FromHours(3); // Set cookie expiration time to 3 hours
                options.SlidingExpiration = true;
            });

            // CORS Configuration
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("corspolicy", build =>
                {
                    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                });
            });

            // Swagger Configuration
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
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            // Add services to the container
            builder.Services.AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               });

            builder.Services.AddDbContext<CourtSyncContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register Redis ConnectionMultiplexer as a Singleton

            // Add repositories and services
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<ICourtRepository, CourtRepository>();
            builder.Services.AddScoped<IEvaluateRepository, EvaluateRepository>();
            builder.Services.AddScoped<ISubCourtRepository, SubCourtRepository>();
            builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();
            builder.Services.AddScoped<VnpayService>();
            builder.Services.AddScoped<VnpayPayResponse>();
            builder.Services.AddScoped<VnpayPayRequest>();
            builder.Services.Configure<VnpayConfig>(builder.Configuration.GetSection("Vnpay"));
            // Add IHttpContextAccessor
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IEmailService, EmailService>(sp => new EmailService(
                smtpServer: "smtp.gmail.com",
                smtpPort: 587,
                smtpUsername: "courtb454@gmail.com",
                smtpPassword: "nurs kcxs wnuh qvlp"
            ));

            // Other service registrations

            var app = builder.Build();

            // Configure middleware, static files, authentication, authorization, etc.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("corspolicy");

            app.UseHttpsRedirection();

            var uploadsPath = Path.Combine(app.Environment.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/Uploads"
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
