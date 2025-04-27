namespace WasteIntoCity.Api.Options
{
    public class PendingFinalizationWorksHandlerBackgroundServiceOptions
    {
        public TimeSpan StartServiceWaitingTime { get; set; }

        public TimeSpan IntervalTime { get; set; }

        public int WorksAtTimeAmount { get; set; }

        public TimeSpan MinWorkIntervalAfterFinished { get; set; }
    }
}
