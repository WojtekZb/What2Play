using What2Play_Logic.DTOs;

namespace What2Play_Logic.Interfaces
{
    public interface IGetTypesRepo
    {
        Task<List<GameTypeDTO>> GetTypes();
    }
}
