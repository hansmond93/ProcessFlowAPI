using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProcessFlowSProj.API.Common;
using ProcessFlowSProj.API.Data;
using ProcessFlowSProj.API.Entities;
using ProcessFlowSProj.API.Helpers;
using ProcessFlowSProj.API.Interface;
using ProcessFlowSProj.API.Repository;
using ProcessFlowSProj.API.Services.Extensions;
using Services.Configuration;
using Services.EmailService;

namespace ProcessFlowSProj.API
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("SqlServerConnection")));
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            #region Add Identity

            IdentityBuilder builder = services.AddIdentityCore<StaffEntity>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<StaffEntity>>();

            #endregion

            #region Add Authentication  

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });


            #endregion

            #region Add Authorization

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Administrator"));
            });


            #endregion


            services.AddMvc(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddCors();
            services.AddSwagger();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            Mapper.Reset(); //confirm what it does
            services.AddAutoMapper();
            services.Configure<EmailConfig>(options =>
                Configuration.GetSection(nameof(EmailConfig)).Bind(options));

            
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IWorkFlow, WorkFlow>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IImagesRepository, ImagesRepository>();
            services.AddScoped<ITokenDecryptionHelper, TokenDecryptionHelper>();
            services.AddScoped<ISetupRepository, SetupRepository>();

            services.AddTransient<SetupData>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SetupData seedData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            seedData.SeedAdminUser();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();

            app.UseCustomSwaggerApi();
        }
    }
}
