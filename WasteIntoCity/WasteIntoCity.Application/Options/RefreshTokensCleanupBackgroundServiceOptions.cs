namespace WasteIntoCity.Api.Options
{
    public class RefreshTokensCleanupBackgroundServiceOptions
    {
        public TimeSpan IntervalTime { get; set; }

        public int RecordsAtTimeAmount { get; set; }
    }
}
