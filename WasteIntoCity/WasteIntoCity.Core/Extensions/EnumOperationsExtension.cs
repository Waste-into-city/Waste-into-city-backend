using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Extensions
{
    public static class EnumOperationsExtension
    {
        private static string ToStringAllValidValuesThroughComma<TEnum>() where TEnum : struct, Enum
        {
            TEnum[] roleTypes = Enum.GetValues<TEnum>();

            return string.Join(", ", roleTypes.Select(v => $"{Convert.ToInt32(v)} = {v}"));
        }

        public static void CheckEnumIntValue<TEnum>(int valueInt, string valueIntParamName) where TEnum : struct, Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), valueInt))
            {
                throw new InvalidValueFormatException(nameof(WorkMarkType),
                    $"The {valueIntParamName} should be an enum ({EnumOperationsExtension.ToStringAllValidValuesThroughComma<TEnum>()})");
            }
        }
    }
}
