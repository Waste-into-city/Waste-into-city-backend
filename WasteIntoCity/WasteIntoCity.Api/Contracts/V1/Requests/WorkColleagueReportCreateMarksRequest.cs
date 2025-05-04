using WasteIntoCity.Application.Structs;

namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkColleagueReportCreateMarksRequest
    {
        required public Guid WorksId { get; init; }

        required public List<MarkColleaguePairStruct> MarkColleaguePairStructs { get; init; }
    }
}
