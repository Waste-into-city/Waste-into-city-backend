namespace WasteIntoCity.Application.Structs
{
    public struct ScoreSettingsPairStruct
    {
        public ScoreSettingsPairStruct(int id, int value)
        {
            Id = id;
            Value = value;
        }

        public int Id { get; }

        public int Value { get; }
    }
}
