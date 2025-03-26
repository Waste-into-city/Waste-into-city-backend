namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkMarkTypesRepository
    {
        Task<Dictionary<Guid, int>> TakeDictionaryAllWithKeyIdAndValueAdditionRanking();
    }
}
