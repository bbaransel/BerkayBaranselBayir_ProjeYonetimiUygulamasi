using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Business.Concrete;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Data.Abstract;
using Yonetimsell.Data.Concrete.Contexts;
using Yonetimsell.Data.Concrete.Repositories;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.Helpers.Abstract;
using Yonetimsell.Shared.Helpers.Concrete;
using Yonetimsell.UI.EmailServices.Abstract;
using Yonetimsell.UI.EmailServices.Concrete;

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
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
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
                    Name = "Yonetimsell.Security",
                    //Güvenlik önlemleri(default olarak none)
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                };
            });
            return services;
        }
        public static IServiceCollection LoadMyRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectManager>();
            services.AddScoped<IPTaskService, PTaskManager>();
            services.AddScoped<ISubscriptionService, SubscriptionManager>();
            services.AddScoped<ITeamMemberService, TeamMemberManager>();
            services.AddScoped<IFriendshipService, FriendshipManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IPTaskFileService, PTaskFileManager>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IPTaskRepository, PTaskRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            services.AddScoped<IFriendshipRepository, FriendshipRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPTaskFileRepository, PTaskFileRepository>();
            return services;
        }
        public static IServiceCollection LoadMyOtherServices(this IServiceCollection services)
        {
            services.AddScoped<ISweetAlertService, SweetAlertManager>();
            services.AddScoped<IImageService, ImageManager>(); 
            services.AddScoped<IEmailSender, SmtpEmailSender>(options => new SmtpEmailSender(
            services.BuildServiceProvider().GetRequiredService<IConfiguration>()["EmailSender:Host"],
            services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetValue<int>("EmailSender:Port"),
            services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetValue<bool>("EmailSender:EnableSSL"),
            services.BuildServiceProvider().GetRequiredService<IConfiguration>()["EmailSender:UserName"],
            services.BuildServiceProvider().GetRequiredService<IConfiguration>()["EmailSender:Password"]
            ));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<MapperlyConfiguration>();
            return services;
        }
    }
}
