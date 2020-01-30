using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Interfaces;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController: ControllerBase
    {
        private readonly IRepository _repository;

        public EventoController(IRepository repository)
        {
            this._repository = repository;
            
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.GetEventoByIdAsync(id);
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
            return await _repository.GetAllEventosAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Evento model)
        {
            try
            {
                _repository.Add(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no servidor");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int eventoId, Evento model)
        {
            try
            {
                var evento = _repository.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return NotFound();
                _repository.Update(model);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/evento/{model.Id}", model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no servidor");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = _repository.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return NotFound();
                _repository.Delete(evento);
                if (await _repository.SaveChangesAsync())
                    return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no servidor");
            }

            return BadRequest();
        }
    }
}