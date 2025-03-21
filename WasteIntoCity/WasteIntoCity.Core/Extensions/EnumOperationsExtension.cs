namespace WasteIntoCity.Core.Extensions
{
    public static class EnumOperationsExtension
    {
        public static string ToStringAllValidValuesThroughComma<TEnum>() where TEnum : struct, Enum
        {
            TEnum[] roleTypes = Enum.GetValues<TEnum>();

            return string.Join(", ", roleTypes.Select(v => $"{Convert.ToInt32(v)} = {v}"));
        }
    }
}
