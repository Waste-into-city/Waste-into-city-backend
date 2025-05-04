using WasteIntoCity.Core.Enums;

namespace WasteIntoCity.Application.Structs
{
    public struct MarkColleaguePairStruct
    {
        required public Guid AboutColleagueId { get; init; }

        required public WorkMarkEnum WorkMarkTypesId { get; init; }
    }
}
