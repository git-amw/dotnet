using AMD.Services.Account.BusinessLayer.Services;
using AMD.Services.Accounts.DataLayer.Data;
using AMD.Services.Accounts.DataLayer.Repository;
using AMD.Services.Accounts.DomainLayer.Entities;
using AMD.Services.Accounts.DomainLayer.Helper;
using AMD.Services.Accounts.DomainLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AMD.Services.AccountsAPI
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AMD.Services.AccountsAPI", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<RegistrationEntity, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.Configure<JwtOptionsEntity>(Configuration.GetSection("AuthSettings:JwtOptions"));

            services.Configure<OnboardingOptionsEntity>(Configuration.GetSection("WelcomeMessageSettings:OnboardingOptions"));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepo, AccountRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AMD.Services.AccountsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
