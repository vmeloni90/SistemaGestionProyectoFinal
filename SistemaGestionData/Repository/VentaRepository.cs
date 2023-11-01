using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Interfaces;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestionData.Repository
{
    public class VentaRepository: IVentaRepository
    {
        private readonly Context context;

        public VentaRepository(Context dbContext)
        {
            context = dbContext;
        }

      
        public void CargarVenta(Venta venta)
        {
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            context.Ventas.Add(venta);
            context.SaveChanges();
        }

        public Venta ObtenerVentaPorId(int ventaId)
        {
            return context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.ProductosVendidos)
                .FirstOrDefault(v => v.Id == ventaId);
        }

       
        public List<Venta> MostrarVentas()
        {
            return context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.ProductosVendidos)
                .ToList();
        }


        public void EditarVenta(Venta venta)
        {
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            var ventaExistente = context.Ventas.FirstOrDefault(v => v.Id == venta.Id);
            if (ventaExistente != null)
            {
                ventaExistente.Comentarios = venta.Comentarios;
                ventaExistente.UsuarioId = venta.UsuarioId;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Venta no encontrada");
            }
        }



        public void EliminarVenta(int ventaId)
        {
            var venta = context.Ventas.Find(ventaId);
            if (venta != null)
            {
                context.Ventas.Remove(venta);
                context.SaveChanges();
            }
        }
    }
}
