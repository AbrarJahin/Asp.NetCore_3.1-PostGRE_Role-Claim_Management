using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using StartupProject_Asp.NetCore_PostGRE.Services.EmailService;
using WebMarkupMin.AspNetCore3;

namespace StartupProject_Asp.NetCore_PostGRE
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region IP Address Reading support
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(
                        IPAddress.Parse(Configuration.GetSection("ApplicationIP").Value)
                    );
            });
            #endregion
            #region Email Service Configuration
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            #endregion
            #region DB Service Configuration
            services.AddDbContext<ApplicationDbContext>(options => {
                if (Environment.IsDevelopment())
                {
                    options.UseNpgsql(Configuration.GetConnectionString("DevelopConnection"));
                }
                else
                {
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                }
            });
            #endregion
            #region Identity Service Configuration
            services.AddIdentity<User, Role>(options => {
                    if (Environment.IsDevelopment())
                    {
                        options.SignIn.RequireConfirmedAccount = true;
                        // Password settings
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 4;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireLowercase = false;
                    }
                    else
                    {
                        options.Password.RequiredLength = 8;
                    }
                    //If sequintial failure for 5 times in 5 minuite
                    options.Lockout = new LockoutOptions(){
                        AllowedForNewUsers = true,
                        DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                        MaxFailedAccessAttempts = 5
                    };
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
            #endregion
            #region Policies Configuration in Auth
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("MustHaveEmail", polBuilder => polBuilder.RequireClaim(System.Security.Claims.ClaimTypes.Email));
                options.AddPolicy("IsAdminClaimAccess", policy => policy.RequireClaim("DateOfJoing"));
                //options.AddPolicy("IsAdminClaimAccess", policy => policy.Re
                //options.AddPolicy("Morethan365DaysClaim", policy => policy.Requirements.Add(new MinimumTimeSpendRequirement(365)));
            });
            #endregion
            #region Update Auth every second after role updated
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(1);
            });
            #endregion
            #region View Configuration
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddWebMarkupMin(
                options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = false;
                    options.AllowCompressionInDevelopmentEnvironment = false;
                })
                .AddHtmlMinification(
                    options =>
                    {
                        options.MinificationSettings.RemoveRedundantAttributes = true;
                        options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
                        options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
                    })
                .AddHttpCompression();
            #endregion
            #region Cache Configuration
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60); //60 days
                //options.ExcludedHosts.Add("example.com");
                //options.ExcludedHosts.Add("www.example.com");
            });
            #endregion
            #region Force to use Https Config
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //IWebHostEnvironment env - Can be removed
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();  //Can be ignored
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context => context.Context.Response.GetTypedHeaders()
                  .CacheControl = new CacheControlHeaderValue
                  {
                      NoTransform = true,
                      Public = true,
                      //OnlyIfCached = false,
                      MaxAge = TimeSpan.FromDays(365) // 1 year
                  }
            });
            app.UseWebMarkupMin();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //        name: "auth",
                //        pattern: "auth/{controller=Account}/{action=Register}/{id?}"
                //    );

                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapRazorPages();
            });
        }
    }
}
