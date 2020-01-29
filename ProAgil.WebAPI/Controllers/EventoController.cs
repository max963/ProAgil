using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController: ControllerBase
    {
        private readonly DataContext _context;

        public EventoController(DataContext context)
        {
            this._context = context;
            
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no servidor");
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> Get()
        {
            return await _context.Eventos.ToListAsync();
        }
    }
}