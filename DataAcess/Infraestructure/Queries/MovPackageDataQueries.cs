
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataAcess.Infraestructure.Queries
{
    public class MovPackageDataQueries : IMovPackageDataQueries
    {
        private string _connectionString = string.Empty;

        public MovPackageDataQueries(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //_connectionString = configuration["DefaultConnection"]; 
        }
        public async Task<IEnumerable<MovPackage>> GetAfiliadoDataAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<MovPackage>(
                   @"Select dbo.Packages.IdAfiliado 
                           ,dbo.Packages.CodPackage
                           ,dbo.MovPackage.DateCreated
                           ,dbo.Packages.Monto
                           ,dbo.MovPackage.Interes
                           FROM dbo.Packages INNER JOIN  dbo.MovPackage ON dbo.Packages.Id = dbo.MovPackage.IdPackage 
                               Where dbo.Packages.IdAfiliado = @id order by 3 DESC"
                   , new { id = id }
                );

                return result;
            }
        }
    }
}


