using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionData.Interfaces
{
    public interface IUsuarioRepository
    {
        void AgregarUsuario(Usuario usuario);
        Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario);
        Usuario ObtenerUsuarioPorId(int usuarioId);
        List<Usuario> GetUsuarios();
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(int usuarioId);

    }
}
