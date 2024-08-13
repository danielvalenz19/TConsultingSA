using TConsultigSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TConsultigSA.Servicios
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(Usuario usuario);
    }
}
