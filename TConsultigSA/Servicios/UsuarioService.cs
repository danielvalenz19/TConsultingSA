using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TConsultigSA.Models;

namespace TConsultigSA.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly string _connectionString;
        public UsuarioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            const string sql = "SELECT UsuarioID, NombreUsuario, Contraseña, FechaCreacion FROM Usuarios";
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Usuario>(sql);
            }
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            const string sql = "SELECT UsuarioID, NombreUsuario, Contraseña, FechaCreacion FROM Usuarios WHERE UsuarioID = @Id";
            using (var connection = CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Usuario>(sql, new { Id = id });
            }
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombreUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { NombreUsuario = nombreUsuario });
            }
        }


        public async Task AddUsuarioAsync(Usuario usuario)
        {
            const string sql = @"
                INSERT INTO Usuarios (NombreUsuario, Contraseña, FechaCreacion) 
                VALUES (@NombreUsuario, @Contraseña, @FechaCreacion)";

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(sql, new
                {
                    usuario.NombreUsuario,
                    usuario.Contraseña,
                    usuario.FechaCreacion
                });
            }
        }

    }
}
