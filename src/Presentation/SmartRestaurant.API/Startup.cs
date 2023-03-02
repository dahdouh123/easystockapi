using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartRestaurant.API.Configurations;
using SmartRestaurant.API.Swagger;
using SmartRestaurant.Application;
using SmartRestaurant.Application.Common.Configuration;
using SmartRestaurant.Application.Common.Tools;
using SmartRestaurant.Application.Email;
using SmartRestaurant.Infrastructure;
using SmartRestaurant.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using SmartRestaurant.Application.Common.Configuration.SocialMedia;
using IHostApplicationLifetime = Microsoft.Extensions.Hosting.IHostApplicationLifetime;
using SmartRestaurant.Application.CurrencyExchange;
using SmartRestaurant.API.Scheduler;
using SmartRestaurant.API.Middlewares;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using SmartRestaurant.Infrastructure.Email;
using SmartRestaurant.Infrastructure.Services;

namespace SmartRestaurant.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddDirectoryBrowser();
            services.AddInfrastructure(Configuration);
            services.AddIdentityInfrastructure(Configuration);
            services.AddHttpContextAccessor();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<SmtpConfig>(Configuration.GetSection("Smtp"));
            services.Configure<WebPortal>(Configuration.GetSection("WebPortal"));
            services.Configure<EmailTemplates>(Configuration.GetSection("EmailTemplates"));
            services.Configure<Authentication>(Configuration.GetSection("Authentication"));
            services.Configure<FirebaseConfig>(Configuration.GetSection("FirebaseConfig"));
            services.Configure<Odoo>(Configuration.GetSection("Odoo"));
            CORSConfiguration.AddCORSConfiguation(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Smart Restaurant api v1", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                        Array.Empty<string>()
                    }
                });
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();


                c.ExampleFilters();
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AddRequiredHeaderParameter>();

                c.AddFluentValidationRules();
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            services.AddControllersWithViews()
                .AddFluentValidation(c =>
                {
                    c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
                })
                .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;});
            services.AddMediator();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            CORSConfiguration.UseCORS(app, env);

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Restaurant api v1");
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);

            });
            var defaultDateCulture = "fr-FR";
            var cultureInfo = new CultureInfo(defaultDateCulture);
            cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureInfo),
                SupportedCultures = new List<CultureInfo> {
                    cultureInfo,
                },
                SupportedUICultures = new List<CultureInfo> {
                    cultureInfo,
                }
            });
            app.UseRouting();
            app.UseMiddleware<AuthorizeNonFrozenFoodBusinessesMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            lifetime.ApplicationStarted.Register(OnApplicationStarted);

        }
        public void OnApplicationStarted()
        {
            CurrencyExchangeApi.ImportFromOnlineApi();
            new CommissionScheduler().ExecuteCommissionMonthlyTasks();
        }
    }
}