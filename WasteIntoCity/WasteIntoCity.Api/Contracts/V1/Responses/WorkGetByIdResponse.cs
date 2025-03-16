using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkGetByIdResponse
    {
        required public Work Work { get; init; }
    }
}
