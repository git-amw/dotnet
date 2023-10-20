using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using verify.DataLayer.Data;
using verify.DataLayer.Interfaces;
using verify.DataLayer.Repo;
using verify.services.Models;
using EmailService = verifyapi.Services.EmailService;
using IEmailService = verifyapi.Services.IEmailService;

namespace verifyapi
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

            services.AddControllers();

            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            
            services.AddSingleton<IEmailService, EmailService>();

            services.Configure<MailKitOptions>(Configuration.GetSection("EmailConfiguration"));

            // Add the email service implementation (EmailService) to the DI container

            //services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IValidation, ValidationRepo>();

            // Add the MailKit provider to the DI container
            services.AddMailKit(config => config.UseMailKit(Configuration.GetSection("EmailConfiguration").Get<MailKitOptions>()));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "verifyapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "verifyapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
