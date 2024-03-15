using Microsoft.EntityFrameworkCore;
using PracticaMvcMichelyPinto.Models;

namespace PracticaMvcMichelyPinto.Data
{
    public class ZapatillaContext:DbContext
    {
        public ZapatillaContext(DbContextOptions<ZapatillaContext> options)
            : base(options) { }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenZapatilla> ImagenZapatillas { get; set; }
    }
}
