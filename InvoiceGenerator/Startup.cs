using ElectronNET.API;
using ElectronNET.API.Entities;
using InvoiceGenerator.Common.Helpers;
using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Middlewares;
using InvoiceGenerator.Repository;
using InvoiceGenerator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvoiceGenerator
{
    /// <summary>
    /// Implements the start up.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Register database.
            services.AddDbContext<InGenDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("InGenDbContext")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //Adds initializer.
            services.AddAsyncInitializer<Initializer>();

            //Registers unit of work.
            services.AddScoped<IUnitOfWork, UnitOfWork<InGenDbContext>>();

            //Registers helpers and their interfaces.
            services.AddScoped<IFileHelper, FileHelper>();

            //Registers services and their interfaces.
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IImageService, ImageService>();

            //Adds Controllers with views.
            services.AddControllersWithViews();
            services.AddRazorPages();

            //Registers Identity managers.
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddRoleManager<RoleManager<Role>>()
            .AddUserManager<UserManager<User>>()
            .AddEntityFrameworkStores<InGenDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            if (HybridSupport.IsElectronActive)
                CreateWindow();
        }
        private async void CreateWindow()
        {
            var window = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                WebPreferences = new WebPreferences { NativeWindowOpen = true },
                AutoHideMenuBar = true
                //Width = 1000,
                //Height = 800,
            });

            window.OnReadyToShow += () =>
            {
                window.Show();
            };

            window.OnClose += () =>
            {
                Electron.App.Quit();
            };
        }
    }
}