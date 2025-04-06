using System.ComponentModel.DataAnnotations;

namespace Clientes.Models
{
    public class Cliente
    {
        //propiedades de la tabla a interactuar
        public int cliente_id { get; set; }
        public int nit { get; set; }
        public string? nombre { get; set; }
        public string? Apellido { get; set; }
        public string? email { get; set; }
        public int telefono { get; set; }
        public string? direccion { get; set; }
    }
}