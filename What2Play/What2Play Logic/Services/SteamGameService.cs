using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using What2Play_Logic.Interfaces;

namespace What2Play_Logic.Services
{
    public class SteamGameService
    {
        private readonly ISteamGamesRepo _repo;

        public SteamGameService(ISteamGamesRepo repo)
        {
            _repo = repo;
        }
        public Task<string> AddSteamGames(string steamId)
        {
            return _repo.AddSteamGames(steamId);
        }
    }
}
