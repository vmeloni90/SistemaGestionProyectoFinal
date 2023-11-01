using SistemaGestionEntities;

namespace SistemaGestionProyectoFinal.Views.Model
{
    public class CargarVentaViewModel
    {
        public int UsuarioId { get; set; }
        public List<Producto> ProductosDisponibles { get; set; }
        public List<int> ProductosSeleccionados { get; set; } 
        public int CantidadVendida { get; set; }
        public string Comentarios { get; set; }
    }
}
