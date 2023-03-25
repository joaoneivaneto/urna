using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CandidatosApi.Models;
using CandidatosApi.Dto;

namespace CandidatosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroCandidatosController : ControllerBase
    {
        private readonly CandidatosDBContext _context;

        public RegistroCandidatosController(CandidatosDBContext context)
        {
            _context = context;
        }

        // GET: api/RegistroCandidatos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroCandidato>>> GetRegistroCandidatos()
        {
            return await _context.RegistroCandidatos.ToListAsync();
        }

        // GET: api/RegistroCandidatos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroCandidato>> GetRegistroCandidato(Guid id )
        {
            var registroCandidato = await _context.RegistroCandidatos.FindAsync(id);

            if (registroCandidato == null)
            {
                return StatusCode(404, "Candidato Não existe");
            }

            return registroCandidato;
        }
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<List<RegistroCandidato>>> Search([FromQuery] int legenda)
        {
            var registroCandidato = await _context.RegistroCandidatos
                 .Where(x => x.Legenda == legenda)
                 .ToListAsync();
            if (registroCandidato == null)
            {
                return StatusCode(404, "Candidato Não existe");
            }

            return registroCandidato;

        }

        // PUT: api/RegistroCandidatos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistroCandidato(Guid id, CreateRegistroCandidatosDto request)
        {
            var UpdateCandidato = new RegistroCandidato
            {
                Id = id,
                NomeCompleto = request.Nome_Candidato,
                NomeVice = request.Nome_Vice,
                DataRegistro = request.Data_Registro,
                Legenda = request.Legenda
            };
            

            _context.Entry(UpdateCandidato).State = EntityState.Modified;

            if (_context.RegistroCandidatos.Any(n => n.NomeCompleto == request.Nome_Candidato))
            {
                return StatusCode(404, "candidato ja existe");
            }

            if (_context.RegistroCandidatos.Any(n => n.NomeVice == request.Nome_Vice))
            {
                return StatusCode(404, "vice ja existe");
            }

            if (_context.RegistroCandidatos.Any(n => n.Legenda == request.Legenda))
            {
                return StatusCode(404, "Legenda ja existe");
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroCandidatoExists(id))
                {
                    return StatusCode(404, "Candidato Não existe");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetRegistroCandidato", new { id = request.Id_Candidato }, request);

        }

        // POST: api/RegistroCandidatos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegistroCandidato>> PostRegistroCandidato(CreateRegistroCandidatosDto request)
        {
            var newCandidato = new RegistroCandidato
            {
                Id = request.Id_Candidato,
                NomeCompleto = request.Nome_Candidato,
                NomeVice = request.Nome_Vice,
                DataRegistro = request.Data_Registro,
                Legenda = request.Legenda
            };

            if(_context.RegistroCandidatos.Any(n => n.NomeCompleto == request.Nome_Candidato))
            {
                return StatusCode(404, "candidato ja existe");
            }

            if (_context.RegistroCandidatos.Any(n => n.NomeVice == request.Nome_Vice))
            {
                return StatusCode(404, "vice ja existe");
            }

            if (_context.RegistroCandidatos.Any(n => n.Legenda == request.Legenda))
            {
                return StatusCode(404, "Legenda ja existe");
            }

            _context.RegistroCandidatos.Add(newCandidato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistroCandidato", new { id = request.Id_Candidato }, request);
        }

        // DELETE: api/RegistroCandidatos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroCandidato(Guid id)
        {
            var registroCandidato = await _context.RegistroCandidatos.FindAsync(id);
            if (registroCandidato == null)
            {
                return StatusCode(404, "Candidato Não existe");
            }

            _context.RegistroCandidatos.Remove(registroCandidato);
            await _context.SaveChangesAsync();

            return StatusCode(205, "Candidato removido com sucesso");
        }

        private bool RegistroCandidatoExists(Guid id)
        {
            return _context.RegistroCandidatos.Any(e => e.Id == id);
        }

        
       
    }
}
