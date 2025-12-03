using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using What2Play_Logic.Entities;

namespace What2Play_Logic.Interfaces
{
    public interface IGameRepo
    {
        Task<List<Game>> GetGames();
        Task<string> AddGame(Game game);
    }
}
