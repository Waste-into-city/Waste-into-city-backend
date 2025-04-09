namespace WasteIntoCity.Api.Options
{
    public class ImagesCleanupBackgroundServiceOptions
    {
        public TimeSpan IntervalTime { get; set; }

        public int ImagesAtTimeAmount { get; set; }

        public TimeSpan MinImageIntervalAfterUploaded { get; set; }
    }
}
