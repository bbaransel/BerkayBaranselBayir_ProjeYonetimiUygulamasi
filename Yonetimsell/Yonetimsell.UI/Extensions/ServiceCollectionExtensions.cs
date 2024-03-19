using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Entity.Concrete.Identity;

namespace Yonetimsell.UI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyDbContextServices(this IServiceCollection services)
        {
            services.AddDbContext<YonetimsellDbContext>(options => 
            options.UseSqlite(services.BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetConnectionString("SqliteConnection")));
            return services;
        }
        public static IServiceCollection LoadMyIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<YonetimsellDbContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                #region Password
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                #endregion
                #region Account Lock Options
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                #endregion
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    Name = "MiniShop.Security",
                    //Güvenlik önlemleri(default olarak none)
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                };
            });
            return services;
        }
        public static IServiceCollection LoadMyRepositoryServices(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection LoadMyOtherServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
