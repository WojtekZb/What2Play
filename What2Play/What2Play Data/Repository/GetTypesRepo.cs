using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using What2Play_Logic.DTOs;
using What2Play_Logic.Interfaces;

namespace What2Play_Data.Repository
{
    public class GetTypesRepo : IGetTypesRepo
    {
        private readonly string _connectionstring;

        public GetTypesRepo(IConfiguration conn)
        {
            _connectionstring = conn.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string not found.");
        }
        public List<GameTypeDTO> TypeList { get; set; }


        public async Task<List<GameTypeDTO>> GetTypes()
        {
            string sql = @"SELECT TypeId, Type FROM GameType";
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            var typeList = new List<GameTypeDTO>();

            while (await reader.ReadAsync())
            {
                var gameType = new GameTypeDTO
                {
                    TypeId = Convert.ToInt32(reader["TypeId"]),
                    TypeName = reader["Type"].ToString()
                };

                typeList.Add(gameType);
            }

            return typeList;
        }

    }
}
