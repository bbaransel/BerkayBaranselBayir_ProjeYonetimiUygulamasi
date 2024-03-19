using Microsoft.EntityFrameworkCore;
using Yonetimsell.Data.Concrete.Contexts;

namespace Yonetimsell.UI.Extensions
{
    public static class HostServiceExtension
    {
        public static IHost UpdateDatabase(this IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                using(var yonetimsellDbContext = scope.ServiceProvider.GetRequiredService<YonetimsellDbContext>())
                {
                    try
                    {
                        var pendingMigrationsCount = yonetimsellDbContext.Database.GetPendingMigrations().Count();
                        if(pendingMigrationsCount > 0) yonetimsellDbContext.Database.Migrate();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
