namespace WasteIntoCity.Api.Options
{
    public class FinishedWorksHandlerBackgroundServiceOptions
    {
        public TimeSpan IntervalTime { get; set; }

        public int WorksAtTimeAmount { get; set; }

        public TimeSpan MinWorkIntervalAfterFinished { get; set; }
    }
}
