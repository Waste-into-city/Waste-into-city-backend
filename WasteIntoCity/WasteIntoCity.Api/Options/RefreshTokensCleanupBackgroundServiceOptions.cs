namespace WasteIntoCity.Api.Options
{
    public class RefreshTokensCleanupBackgroundServiceOptions
    {
        public int IntervalHours { get; set; }

        public int RecordsAtTimeAmount { get; set; }
    }
}
