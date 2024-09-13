using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace LibrosAPI.Models
{
    public class LibrosDBContext : DbContext
    {
        public LibrosDBContext(DbContextOptions<LibrosDBContext> options) : base(options) 
        {
           
        }

        public DbSet<Libro> Libros { get; set; }
    }
}
