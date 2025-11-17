using What2Play_Logic.Entities;
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
        public Task<List<GameType>> GetTypes()
        {
            return _repo.GetTypes();
        }
    }
}
