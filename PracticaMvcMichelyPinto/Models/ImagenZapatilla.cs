using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcMichelyPinto.Models
{
    [Table("IMAGENESZAPASPRACTICA")]

    public class ImagenZapatilla
    {
        [Key]
        [Column("IDIMAGEN")]
        public int IdImage { get; set; }
        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
    }
}
