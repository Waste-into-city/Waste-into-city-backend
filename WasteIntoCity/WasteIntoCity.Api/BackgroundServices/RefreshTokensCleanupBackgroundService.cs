namespace WasteIntoCity.Api.BackgroundServices
{
    public class RefreshTokensCleanupBackgroundService
    {
        //private Timer? _timer;
        //private readonly IServiceScopeFactory _scopeFactory;

        //public RefreshTokensCleanupBackgroundService(IServiceScopeFactory scopeFactory, RefreshTokensRepository refreshTokensRepository)
        //{
        //    _scopeFactory = scopeFactory;
        //}

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _timer = new Timer(DeleteExpiredTokens, null, TimeSpan.Zero, TimeSpan.FromHours(3));
        //    return Task.CompletedTask;
        //}

        //private void DeleteExpiredTokens(object? state)
        //{
        //    using IServiceScope scope = _scopeFactory.CreateScope();
        //    MainDbContext dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();

        //    dbContext.RefreshTokens.RemoveRange(dbContext.RefreshTokens.Where(t => t. < DateTime.UtcNow));
        //    dbContext.SaveChanges();
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    _timer?.Change(Timeout.Infinite, 0);

        //    return Task.CompletedTask;
        //}

        //public void Dispose()
        //{
        //    _timer?.Dispose();
        //}
    }
}
