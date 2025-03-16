using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkGetAllResponse
    {
        required public List<Work> Works { get; init; } = [];
    }
}
