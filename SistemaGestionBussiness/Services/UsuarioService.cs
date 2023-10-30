using SistemaGestionBussiness.Interfaces;
using SistemaGestionData.Interfaces;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void CreateUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            if (NombreUsuarioYaExiste(usuario.NombreUsuario))
            {
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");
            }

            _usuarioRepository.CreateUsuario(usuario);
        }


        public bool IsAuthenticated(ClaimsPrincipal user)
        {
            if (user == null)
                return false;

            return user.Identity.IsAuthenticated;
        }


        private bool NombreUsuarioYaExiste(string nombreUsuario)
        {
            return _usuarioRepository.ObtenerUsuarioPorNombreUsuario(nombreUsuario) != null;
        }

        public Usuario ObtenerUsuarioPorId(int usuarioId)
        {
            return _usuarioRepository.ObtenerUsuarioPorId(usuarioId);
        }

        public List<Usuario> GetUsuarios()
        {
            return _usuarioRepository.GetUsuarios();
        }

        public void EditarUsuario(Usuario usuarioActualizado)
        {
            // Verificar si usuarioActualizado es nulo
            if (usuarioActualizado == null)
            {
                throw new ArgumentNullException(nameof(usuarioActualizado));
            }

            // Obtener usuario de la base de datos
            var usuarioEnBD = _usuarioRepository.ObtenerUsuarioPorId(usuarioActualizado.Id);

            // Verificar si el usuario existe en la base de datos
            if (usuarioEnBD == null)
            {
                throw new InvalidOperationException("El usuario no existe en la base de datos.");
            }

            // Verificar si el nombre de usuario está siendo modificado
            if (usuarioActualizado.NombreUsuario != usuarioEnBD.NombreUsuario)
            {
                // Comprobar si el nuevo nombre de usuario ya existe
                var existingUser = _usuarioRepository.ObtenerUsuarioPorNombreUsuario(usuarioActualizado.NombreUsuario);

                if (existingUser != null && existingUser.Id != usuarioActualizado.Id)
                {
                    throw new InvalidOperationException("El nombre de usuario ya está en uso por otro usuario.");
                }
            }

            // Actualizar campos del usuario
            usuarioEnBD.Nombre = usuarioActualizado.Nombre;
            usuarioEnBD.Apellido = usuarioActualizado.Apellido;
            usuarioEnBD.NombreUsuario = usuarioActualizado.NombreUsuario;
            usuarioEnBD.Mail = usuarioActualizado.Mail;

            // Verificar si la contraseña ha cambiado y, en ese caso, actualizarla
            if (usuarioActualizado.Password != usuarioEnBD.Password)
            {
                usuarioEnBD.Password = usuarioActualizado.Password;
            }

            // Guardar cambios
            _usuarioRepository.EditarUsuario(usuarioEnBD);
        }


        public void EliminarUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(usuarioId), "El ID del usuario es inválido.");
            }

            _usuarioRepository.EliminarUsuario(usuarioId);
        }

        public Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                throw new ArgumentException("El nombre de usuario es requerido.", nameof(nombreUsuario));
            }

            return _usuarioRepository.ObtenerUsuarioPorNombreUsuario(nombreUsuario);
        }
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            return _usuarioRepository.GetUsuarios();
        }
    }
}
