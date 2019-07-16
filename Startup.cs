using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCasbin;
using NetCasbin.Persist;
using Casbin.NET.Adapter.EFCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace CasbinTestProj
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
            // NOTE: add this 3 lines to your own project
            // services.AddScoped<IEntityTypeConfiguration<CasbinRule>, CustomCasbinRuleTableConfiguration>();
            services.AddDbContext<CasbinDbContext>(opt => opt.UseSqlite("Data Source=casbin_test.sqlite3"));
            services.AddScoped<IAdapter>(x => new CasbinDbAdapter (x.GetRequiredService<CasbinDbContext>()));
            services.AddScoped<Enforcer>(x => new Enforcer ("./auth_model.conf",  x.GetRequiredService<IAdapter>()));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("AdminAuthScheme", x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    // use your own key
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("ab319fd28cec8280712916ba12mab2b5")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddHttpContextAccessor();
            // be careful, because `Enforcer` is injected as scoped service
            // this handler must be scoped or Transient
            services.AddTransient<IAuthorizationHandler, CasbinCheckPermHandler>();
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("CasbinCheckPerm", policy =>
                {
                    policy.AuthenticationSchemes.Add("AdminAuthScheme");
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new CasbinCheckPermRequirement());
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
