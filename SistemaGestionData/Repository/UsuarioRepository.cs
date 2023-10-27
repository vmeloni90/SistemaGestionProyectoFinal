using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Interfaces;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionData.Repository
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly Context context;



        public UsuarioRepository(Context dbContext)
        {
            context = dbContext;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        public Usuario ObtenerUsuarioPorId(int usuarioId)
        {
            return context.Usuarios.Find(usuarioId);
        }

        public List<Usuario> GetUsuarios()
        {
            return context.Usuarios.ToList();
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            context.Entry(usuario).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void EliminarUsuario(int usuarioId)
        {
            var usuario = context.Usuarios.Find(usuarioId);
            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
            }
        }
        public Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            return context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }
    }
}
