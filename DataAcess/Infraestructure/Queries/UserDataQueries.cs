using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataAcess.Infraestructure.Queries
{
    public class UserDataQueries : IUserDataQueries
    {
        private string _connectionString = string.Empty;

        public UserDataQueries(IConfiguration configuration)
        {
           _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //_connectionString = configuration["DefaultConnection"]; 
        }
        public async Task<UserData> GetUserDataAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryFirstOrDefaultAsync<UserData>(
                   @"select [Id]
                          ,[name]
                          ,[surname]
                          ,[city]
                          ,[mobile]
                          ,[dateOfbirth]
                          ,[wallet]
                          ,[email]
                          ,[CreatedDate]
                          ,[UpdatedDate]
                    FROM UserData WHERE email=@email"
                   , new { email = email }
                );

                return result;
            }
        }
    }
}
