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
    public class TiposController : ControllerBase
    {
        private readonly Contexto _contexto;

        public TiposController(Contexto context)
        {
            _contexto = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo>>> GetTipos()
        {
            return await _contexto.Tipos.ToListAsync();
        }
    }
}
