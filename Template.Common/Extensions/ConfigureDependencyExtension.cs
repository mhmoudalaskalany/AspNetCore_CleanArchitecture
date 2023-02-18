﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Template.Common.Core;
using Template.Common.Extensions.Swagger.Headers;
using Template.Common.Helpers.EmailHelper;
using Template.Common.Helpers.FileHelpers.StorageHelper;
using Template.Common.Helpers.HttpClient;
using Template.Common.Helpers.HttpClient.RestSharp;
using Template.Common.Helpers.MailKitHelper;
using Template.Common.Helpers.MediaUploader;
using Template.Common.Helpers.TokenGenerator;
using Template.Common.Services;

namespace Template.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureDependencyExtension
    {
        public static IServiceCollection RegisterCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.RegisterFileServices();
            services.RegisterMainCore();
            services.RegisterEmailMetadata(configuration);
            services.AddApiDocumentationServices(configuration);
            services.RegisterAuthentication(configuration);
            services.RegisterHttpClientHelpers();
            return services;
        }

        private static void RegisterMainCore(this IServiceCollection services)
        {
            services.AddSingleton<MicroServicesUrls>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IResponseResult, ResponseResult>();
            services.AddTransient<IFinalResult, FinalResult>();
            services.AddSingleton<ISendMail, SendMail>();
            services.AddSingleton<ISendMailKit, SendMailKit>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IUploaderConfiguration, UploaderConfiguration>();
        }

        private static void RegisterHttpClientHelpers(this IServiceCollection services)
        {
            services.AddTransient<IRestSharpContainer, RestSharpContainer>();
        }

        private static void RegisterFileServices(this IServiceCollection services)
        {
            services.AddScoped<LocalStorageService>();
            services.AddScoped<PasswordLessStorageService>();
            services.AddScoped<Func<string, IStorageService>>(serviceProvider => key =>
            {
                return key switch
                {
                    "LocalStorage" => serviceProvider.GetService<LocalStorageService>(),
                    "PasswordLessStorage" => serviceProvider.GetService<PasswordLessStorageService>(),
                    _ => serviceProvider.GetService<PasswordLessStorageService>()
                };
            });
        }

        private static void RegisterEmailMetadata(this IServiceCollection services, IConfiguration configuration)
        {
            var notificationMetadata = configuration.GetSection("EmailMetadata").Get<EmailMetadata>();
            if (notificationMetadata != null) services.AddSingleton(notificationMetadata);
        }

        private static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? string.Empty)),
                };
            });
        }


        private static void AddApiDocumentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                var title = configuration["SwaggerConfig:Title"];
                var version = configuration["SwaggerConfig:Version"];
                var docPath = configuration["SwaggerConfig:DocPath"];
                options.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version });
                if (docPath != null)
                {
                    var filePath = Path.Combine(AppContext.BaseDirectory, docPath);
                    options.IncludeXmlComments(filePath);
                }

                var security = new OpenApiSecurityRequirement
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
                        new string[] { }

                    }
                };
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(security);
                options.OperationFilter<LanguageHeader>();
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
