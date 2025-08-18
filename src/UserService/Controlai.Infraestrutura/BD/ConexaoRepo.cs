using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.Extensions.Configuration;


namespace Infraestrutura.Data
{
    public class ConexaoRepo
    {
        private readonly string? _connectionString;

        public ConexaoRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("String de conexão não encontrada");
        }

        public MySqlConnection ObterConexao()
        {
            return new MySqlConnection(_connectionString);
        }

    }
}