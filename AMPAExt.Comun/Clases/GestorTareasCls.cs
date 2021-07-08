
namespace RAEE.Comun.Clases
{
    /// <summary>
    /// Clase para indicar el número de solicitudes pendientes de tramitar
    /// </summary>
    public class GestorTareasCls
    {
        public int Total { get; set; }
        public int Productor { get; set; }
        public int Scrap { get; set; }
        public int GestionProductos { get; set; }
        public int Comunicaciones { get; set; }
    }
}
