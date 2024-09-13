using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrosAPI.Controllers;
using LibrosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace LibrosAPI.Tests
{
    
    public class LibrosControllerTest
    {
        [Fact]
        public async Task PostLibro_AgregarLibro_CuandoLibroEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new LibrosController(context);
            var nuevolibro = new Libro { Titulo = "El Principito", Autor = "Antoines de Saint-Exupéry",
            FechaPublicacion = new DateTime(1943, 4, 6)};
            
            var result = await controller.PostLibro(nuevolibro);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var libro = Assert.IsType<Libro>(createdResult.Value);
            Assert.Equal("El Principito", libro.Titulo);

        }
        [Fact]
        public async Task GetLibro_RetornaLibro_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new LibrosController(context);
            var libro = new Libro { Titulo = "1984", Autor = "George Orwell", FechaPublicacion = new DateTime(1949, 6, 8) };
            context.Libros.Add(libro);
            await context.SaveChangesAsync();

            var result = await controller.GetLibro(libro.Id);

            var actionResult = Assert.IsType<ActionResult<Libro>>(result);
            var returnValue = Assert.IsType<Libro>(actionResult.Value);
            Assert.Equal("1984", returnValue.Titulo);
        }
        [Fact]
        public async Task GetLibro_RetornaNotFound_CuandoIdNoExiste()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new LibrosController(context);
            var result = await controller.GetLibro(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async Task PostLibro_NoAgregaLibro_CuandoTituloEsNulo()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new LibrosController(context);
            var libroInicial = new Libro { Titulo = "Libro 1", Autor = "Autor 1", FechaPublicacion = DateTime.Now };
            await controller.PostLibro(libroInicial);
            var nuevoLibro = new Libro { Titulo = "Libro 2", Autor = "Autor 2", FechaPublicacion = DateTime.Now };

            await controller.PostLibro(nuevoLibro);
            var libros = await context.Libros.ToListAsync();

            Assert.Equal(2, libros.Count);
        }
    }
}
