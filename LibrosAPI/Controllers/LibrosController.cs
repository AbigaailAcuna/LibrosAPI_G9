using Microsoft.AspNetCore.Mvc;
using LibrosAPI.Models;

namespace LibrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly LibrosDBContext _context;

        public LibrosController(LibrosDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            if (string.IsNullOrEmpty(libro.Titulo))
            {
                return BadRequest("El título no puede estar vacío");
            }
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id}, libro);
          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }
    }
}

