
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataAcess.Infraestructure.Queries
{
    public class AfiliadoDataQueries : IAfiliadoDataQueries
    {
        private string _connectionString = string.Empty;

        public AfiliadoDataQueries(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //_connectionString = configuration["DefaultConnection"]; 
        }
        public async Task<UserData> GetAfiliadoDataAsync(string afiliado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryFirstOrDefaultAsync<UserData>(
                   @"select [Id]
                          ,[name]
                          ,[surname]
                    FROM UserData WHERE id=@afiliado"
                   , new { afiliado = afiliado }
                );

                return result;
            }
        }
    }
}
