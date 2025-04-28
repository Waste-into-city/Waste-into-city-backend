using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
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
                    $"The {valueIntParamName} should be an enum ({EnumOperationsExtension.ToStringAllValidValuesThroughComma<TEnum>()})", 35);
            }
        }

        public static WorkStatusForClientEnum TakeWorkStatusForClientEnum(DateTime? startedDatetime, DateTime? finishDatetime,
           WorkStatusEnum workStatusTypesId)
        {
            WorkStatusForClientEnum workStatusForClient;
            DateTime currentDateTime = DateTime.UtcNow;
            if (startedDatetime is null)
            {
                workStatusForClient = WorkStatusForClientEnum.Avaliable;
            }
            else
            {
                if (workStatusTypesId == WorkStatusEnum.FinishedSuccessfully)
                {
                    workStatusForClient = WorkStatusForClientEnum.FinishedSuccessfully;
                }
                else if (workStatusTypesId == WorkStatusEnum.FinishedFailed)
                {
                    workStatusForClient = WorkStatusForClientEnum.FinishedFailed;
                }
                else if (workStatusTypesId == WorkStatusEnum.Closed)
                {
                    workStatusForClient = WorkStatusForClientEnum.Closed;
                }
                else if (startedDatetime > currentDateTime)
                {
                    workStatusForClient = WorkStatusForClientEnum.Preparing;
                }
                else if (startedDatetime < currentDateTime && finishDatetime > currentDateTime)
                {
                    workStatusForClient = WorkStatusForClientEnum.InProgress;
                }
                else
                {
                    workStatusForClient = WorkStatusForClientEnum.PendingFinalization;
                }
            }

            return workStatusForClient;
        }
    }
}
