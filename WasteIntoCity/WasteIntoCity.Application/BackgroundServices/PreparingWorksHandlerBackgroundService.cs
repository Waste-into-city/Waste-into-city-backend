using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;

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

        public async Task ReturnPreparingWorksToAvaliableAsync(IWorksRepository worksRepository,
            IWorkComplexityTypesRepository workComplexityTypesRepository, Dictionary<WorkComplexityEnum, WorkComplexityType> workComplexityValues,
            CancellationToken stoppingToken)
        {
            List<Guid> workIds = await worksRepository.FindFirstPreparingWorksIdsByBeforeStartedTimeAndNotEnoughParticipants(
                _optionsBackgroundService.Value.WorksAtTimeAmount,
                _optionsPreparingWorks.Value.MinIntervalStartAfterNow,
                workComplexityValues
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
                IWorkComplexityTypesRepository workComplexityTypesRepository = scope.ServiceProvider.
                    GetRequiredService<IWorkComplexityTypesRepository>();

                Dictionary<WorkComplexityEnum, WorkComplexityType> workComplexityValues =
                    new Dictionary<WorkComplexityEnum, WorkComplexityType>();

                List<WorkComplexityType> workComplexityTypes = await workComplexityTypesRepository.FindAll();

                foreach (WorkComplexityType workComplexityType in workComplexityTypes)
                {
                    workComplexityValues.Add(workComplexityType.Id, workComplexityType);
                }

                await ReturnPreparingWorksToAvaliableAsync(worksRepository, workComplexityTypesRepository,
                    workComplexityValues, stoppingToken);

                await Task.Delay(_optionsBackgroundService.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
