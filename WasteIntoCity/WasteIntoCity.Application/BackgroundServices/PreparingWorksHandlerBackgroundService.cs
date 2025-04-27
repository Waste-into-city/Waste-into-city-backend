using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Interfaces.Repositories;

namespace WasteIntoCity.Application.BackgroundServices
{
    public class PreparingWorksHandlerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<PreparingWorksHandlerBackgroundServiceOptions> _options;

        public PreparingWorksHandlerBackgroundService(IServiceScopeFactory scopeFactory,
            IOptions<PreparingWorksHandlerBackgroundServiceOptions> options)
        {
            _scopeFactory = scopeFactory;
            _options = options;
        }

        public async Task ReturnPreparingWorksToAvaliableAsync(IWorksRepository worksRepository, CancellationToken stoppingToken)
        {
            List<Guid> workIds = await worksRepository.FindFirstPreparingWorksIdsByBeforeStartedTime(
                _options.Value.WorksAtTimeAmount,
                _options.Value.MinWorkIntervalBeforeStart
            );

            await worksRepository.UpdateToAvailableWorksByIds(workIds);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(_options.Value.StartServiceWaitingTime, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();

                IWorksRepository worksRepository = scope.ServiceProvider.GetRequiredService<IWorksRepository>();

                await ReturnPreparingWorksToAvaliableAsync(worksRepository, stoppingToken);

                await Task.Delay(_options.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
