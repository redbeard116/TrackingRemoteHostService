using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using TrackingRemoteHostService.Models.Config;
using TrackingRemoteHostService.Services.AuthService;
using TrackingRemoteHostService.Services.UserService;
using TrackingRemoteHostService.Services.HostsService;
using TrackingRemoteHostService.Services.ScheduleService;
using TrackingRemoteHostService.Services.UserScheduleService;
using TrackingRemoteHostService.Services.HistoryService;
using TrackingRemoteHostService.Services.BackgroundTaskQueue;
using TrackingRemoteHostService.Services.PingService;

namespace TrackingRemoteHostService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton(appSettings);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = appSettings.AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = appSettings.AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = appSettings.AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddSingleton<DbContextOptions<Services.DbService.EfCoreService>>(provider =>
            {
                var contextOptions = new DbContextOptionsBuilder<Services.DbService.EfCoreService>();
                contextOptions.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                return contextOptions.Options;
            });
            services.AddDbContext<Services.DbService.EfCoreService>(ServiceLifetime.Transient);

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IHostsService, HostsService>();
            services.AddSingleton<IScheduleService, ScheduleService>();
            services.AddSingleton<IUserScheduleService, UserScheduleService>();
            services.AddSingleton<IHistoryService, HistoryService>();
            services.AddSingleton<IPingService, PingService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackingRemoteHostService", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddHostedService<SchedulesHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackingRemoteHostService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
