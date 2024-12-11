using System.ComponentModel.DataAnnotations;

namespace CRUD_Productos.Shared
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public double? Price { get; set; }
    }
}
