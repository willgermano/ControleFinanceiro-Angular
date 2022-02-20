using ControleFinanceiro.Data;
using ControleFinanceiro.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly Context _context;

        public CategoriasController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
        {
            return await _context.Categorias
                            .Include(c => c.Tipo)
                            .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.FindAsync<Categoria>(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getCategoria", new { id = categoria.CategoriaId }, categoria);

            //return Ok(new
            //{
            //    mensagem = $"Categoria {categoria.Nome} cadastrada com sucesso"
            //});

            //return BadRequest(categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }
    }
}
