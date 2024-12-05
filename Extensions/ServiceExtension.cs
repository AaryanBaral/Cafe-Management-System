using System.Text;
using Cafe_Management_System.Configurations;
using Cafe_Management_System.Data;
using Cafe_Management_System.Entities;
using Cafe_Management_System.Exceptions;
using Cafe_Management_System.Services.CloudinaryService;
using Cafe_Management_System.Services.JwtService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Cafe_Management_System.Extensions;

public static class ServiceExtension
{
    public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddIdentityConfiguration();
        services.AddExceptionHandler<GlobalException>();
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddJwtAuthentication(configuration);
        services.ConfigureCloudinary();
    }

    
    
    private static void AddIdentityConfiguration(this IServiceCollection services)
    {
        // Configure Identity for Admins
        services.AddIdentityCore<Admins>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // Configure Identity for Users
        services.AddIdentityCore<Users>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;

            // User settings
            options.User.RequireUniqueEmail = true;
        });

        // If you need to add custom user managers
        services.AddScoped<UserManager<Users>>();
        services.AddScoped<UserManager<Admins>>();
    }
     private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
            {
                var secret = configuration.GetSection("JwtConfig:Secret").Value;
                if (string.IsNullOrEmpty(secret))
                {
                    throw new InvalidOperationException("JWT secret is missing in configuration");
                }
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    //used to validate token using different options
                    ValidateIssuerSigningKey = true, //to validate the tokens signing key
                    IssuerSigningKey = new SymmetricSecurityKey(key), // we compare if it matches our key or not
                    ValidateIssuer = false, // it issued to validate the issuer
                    ValidateAudience = false, // it issued to validate the issuer
                    RequireExpirationTime = false, //it sets the token is not expired 
                    ValidateLifetime = true // it sets that the token is valid for lifetime
                };
    
                // Add the Authentication scheme and configurations
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                // Add the jwt configurations as what should be done and how to do it
                .AddJwtBearer(jwt =>
                {
                    jwt.SaveToken = true; // saves the generated token to http context
                    jwt.TokenValidationParameters = tokenValidationParameters;
                });
                services.AddSingleton(tokenValidationParameters);
            }
}