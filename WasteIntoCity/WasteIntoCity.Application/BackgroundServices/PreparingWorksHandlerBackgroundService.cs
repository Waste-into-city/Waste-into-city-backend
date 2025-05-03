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
        private readonly IOptions<PreparingWorksHandlerBackgroundServiceOptions> _optionsBackgroundService;
        private readonly IOptions<PreparingWorksStatusOptions> _optionsPreparingWorks;

        public PreparingWorksHandlerBackgroundService(IServiceScopeFactory scopeFactory,
            IOptions<PreparingWorksHandlerBackgroundServiceOptions> optionsBackgroundService,
            IOptions<PreparingWorksStatusOptions> optionsPreparingWorks)
        {
            _scopeFactory = scopeFactory;
            _optionsBackgroundService = optionsBackgroundService;
            _optionsPreparingWorks = optionsPreparingWorks;
        }

        public async Task ReturnPreparingWorksToAvaliableAsync(IWorksRepository worksRepository, CancellationToken stoppingToken)
        {
            List<Guid> workIds = await worksRepository.FindFirstPreparingWorksIdsByBeforeStartedTime(
                _optionsBackgroundService.Value.WorksAtTimeAmount,
                _optionsPreparingWorks.Value.MinIntervalStartAfterNow
            );

            await worksRepository.UpdateToAvailableWorksByIds(workIds);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(_optionsBackgroundService.Value.StartServiceWaitingTime, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();

                IWorksRepository worksRepository = scope.ServiceProvider.GetRequiredService<IWorksRepository>();

                await ReturnPreparingWorksToAvaliableAsync(worksRepository, stoppingToken);

                await Task.Delay(_optionsBackgroundService.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
