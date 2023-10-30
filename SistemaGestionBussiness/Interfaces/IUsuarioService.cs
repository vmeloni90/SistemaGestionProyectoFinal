using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness.Interfaces
{
    public interface IUsuarioService
    {
        void CreateUsuario(Usuario usuario);
        Usuario ObtenerUsuarioPorId(int usuarioId);
        void EditarUsuario(Usuario usuario);
        void EliminarUsuario(int usuarioId);
        Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario);
        List<Usuario> GetUsuarios();
        bool IsAuthenticated(ClaimsPrincipal user);
    }
}
