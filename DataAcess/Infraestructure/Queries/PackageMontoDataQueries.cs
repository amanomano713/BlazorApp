using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataAcess.Infraestructure.Queries
{
    public class PackageMontoDataQueries: IPackageMontoDataQueries
    {
        private string _connectionString = string.Empty;

        public PackageMontoDataQueries(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //_connectionString = configuration["DefaultConnection"];
        }
        public async Task<PackagesTotalDTO> TotalPackageDataAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryFirstOrDefaultAsync<PackagesTotalDTO>(
                   @"Select SUM(monto) as PackMonto  
                         From Packages Where idafiliado=@id"
                   , new { id = id }
                );

                return result;
            }
        }
    }
}
