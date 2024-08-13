using System;
using System.ComponentModel.DataAnnotations;

namespace TConsultigSA.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Rol { get; set; } = "Usuario";  // Valor predeterminado para Rol
    }

}
