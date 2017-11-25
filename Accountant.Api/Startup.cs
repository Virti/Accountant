using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accountant.Authorization.Middleware.Jwt;
using Accountant.Authorization.Token.Jwt;
using Accountant.Authorization.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Accountant.DataAccess;
using Microsoft.EntityFrameworkCore;
using Accountant.Users.Services;
using Accountant.Api.ViewModels.Budgets;
using AutoMapper;

namespace Accountant.Api
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
            services.AddDbContext<UsersContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString(nameof(UsersContext))));
            services.AddDbContext<BudgetsContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString(nameof(BudgetsContext))));

            services.AddTransient<IUsersService, UsersService>();

            services.AddSingleton<IMapper>(m => new MapperConfiguration(cfg => {
                cfg.AddProfile<AutomapperBudgetsProfile>();
            }).CreateMapper());


            services.AddAuthentication(Configuration.GetSection("JWT"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
