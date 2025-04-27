using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Application.BackgroundServices
{
    public class RefreshTokensCleanupBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<RefreshTokensCleanupBackgroundServiceOptions> _options;

        public RefreshTokensCleanupBackgroundService(IServiceScopeFactory scopeFactory, IOptions<RefreshTokensCleanupBackgroundServiceOptions> options)
        {
            _scopeFactory = scopeFactory;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(_options.Value.StartServiceWaitingTime, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();
                RefreshTokensRepository refreshTokensRepository = scope.ServiceProvider.GetRequiredService<RefreshTokensRepository>();

                await refreshTokensRepository.DeleteUsedAndInvalid(_options.Value.RecordsAtTimeAmount);

                await Task.Delay(_options.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
