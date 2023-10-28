using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness.Interfaces
{
    public interface IUsuarioService
    {
        void AgregarUsuario(Usuario usuario);
        Usuario ObtenerUsuarioPorId(int usuarioId);
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(int usuarioId);
        Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario);
        List<Usuario> GetUsuarios();
        bool AutenticarUsuario(string nombreUsuario, string passwordPlainText);
    }
}
