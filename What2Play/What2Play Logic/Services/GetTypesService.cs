using What2Play_Logic.DTOs;
using What2Play_Logic.Interfaces;

namespace What2Play_Logic.Services
{
    public class GetTypesService
    {
        private readonly IGetTypesRepo _repo;

        public GetTypesService(IGetTypesRepo repo)
        {
            _repo = repo;
        }
        public Task<List<GameTypeDTO>> GetTypes()
        {
            return _repo.GetTypes();
        }
    }
}
