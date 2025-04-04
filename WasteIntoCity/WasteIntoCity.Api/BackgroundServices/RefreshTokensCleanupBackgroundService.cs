using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Api.BackgroundServices
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
            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();
                RefreshTokensRepository refreshTokensRepository = scope.ServiceProvider.GetRequiredService<RefreshTokensRepository>();

                await refreshTokensRepository.DeleteUsedAndInvalid(_options.Value.RecordsAtTimeAmount);

                await Task.Delay(TimeSpan.FromHours(_options.Value.IntervalHours), stoppingToken);
            }
        }
    }
}
