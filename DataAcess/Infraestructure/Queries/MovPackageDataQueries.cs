
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
        public async Task<IEnumerable<MovPackageDTO>> GetAfiliadoDataAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<MovPackageDTO>(
                   @"Select TOP 50 dbo.MovPackage.CodPackage,dbo.MovPackage.MontoPackage,dbo.MovPackage.DateCreated,
                            dbo.MovPackage.MontoRetiro,dbo.MovPackage.MontoTransferido,dbo.MovPackage.IdAfiliadoDestino 
                            FROM dbo.MovPackage
                               Where dbo.MovPackage.IdAfiliado = @id order by 3 DESC"
                   , new { id = id }
                );

                return result;
            }
        }
    }
}


