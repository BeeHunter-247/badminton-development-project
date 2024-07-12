using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Badminton.Web.Services.OTP
{
    public class ExpiredOtpCleanerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ExpiredOtpCleanerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CleanExpiredOtpsAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Run every 1 minute
            }
        }

        private async Task CleanExpiredOtpsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CourtSyncContext>();

                var expiredUsers = await context.Users
                    .Where(u => u.OtpExpiration <= DateTime.UtcNow && u.Verify != 4)
                    .ToListAsync();

                if (expiredUsers.Any())
                {
                    context.Users.RemoveRange(expiredUsers);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}