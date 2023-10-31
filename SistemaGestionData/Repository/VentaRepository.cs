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

        // Método para agregar una nueva venta
        public void CargarVenta(Venta venta)
        {
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            context.Ventas.Add(venta);
            context.SaveChanges();
        }

        // Método para obtener una venta por ID
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

        // Método para editar una venta
        public void EditarVenta(Venta venta)
        {
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            context.Entry(venta).State = EntityState.Modified;
            context.SaveChanges();
        }

        // Método para eliminar una venta por ID
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
