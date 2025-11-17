using What2Play_Logic.Entities;

namespace What2Play_Logic.Interfaces
{
    public interface IGetTypesRepo
    {
        Task<List<GameType>> GetTypes();
    }
}
