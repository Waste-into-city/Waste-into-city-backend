namespace WasteIntoCity.Api.Options
{
    public class PreparingWorksHandlerBackgroundServiceOptions
    {
        public TimeSpan StartServiceWaitingTime { get; set; }

        public TimeSpan IntervalTime { get; set; }

        public int WorksAtTimeAmount { get; set; }

        public TimeSpan MinWorkIntervalBeforeStart { get; set; }
    }
}
