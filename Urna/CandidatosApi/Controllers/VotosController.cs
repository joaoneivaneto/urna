using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CandidatosApi.Models;
using CandidatosApi.Dto;
using Microsoft.Extensions.Logging.Abstractions;

namespace CandidatosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotosController : ControllerBase
    {
        private readonly CandidatosDBContext _context;

        public VotosController(CandidatosDBContext context)
        {
            _context = context;
        }

        // GET: api/Votos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voto>>> Getvotos()
        {
            return await _context.votos.ToListAsync();
        }

        // GET: api/Votos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Voto>>> GetVoto(Guid id)
        {
            var voto = await _context.votos
                  .Where(x => x.IdCandidato == id)
                  .ToListAsync();

            if (voto == null)
            {
                return StatusCode(404, "esse voto não existe");
            }

            return voto;
        }
        [HttpGet]
        [Route("GetBrancoNulo")]
        public async Task<ActionResult<List<Voto>>> GetBrancoNulo([FromQuery] string status)
        {

            var statusVoto = await _context.votos
                  .Where(x => x.status == status)
                  .ToListAsync();

            if (status == null ^ status != "Nulo" ^ status != "Branco" ^ status != "Concedido")
            {
                return StatusCode(404, "esse status de voto não existe");
            }

            return statusVoto;
        }




        // POST: api/Votos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voto>> PostVoto(Votodto request)
        {
            var newVoto = new Voto
            {
                id = request.Id,
                IdCandidato = request.Id_Candidato,
                DataVoto = request.Data_Voto,
                status = request.status

            };

            if (VotoExists2(request.Data_Voto))
            {
                return StatusCode(404, "voto ja existe");
            }
            Guid guid = new Guid("00000000-0000-0000-0000-000000000000");
            if (!CandidatoExists(request.Id_Candidato) && request.Id_Candidato != guid)
            {
                return StatusCode(404, "O candidato Não existe");
            }
            if ((request.Id_Candidato == guid && 
                (request.status != "Nulo" && 
                request.status != "Branco")) || 
                (request.Id_Candidato != guid && 
                request.status != "Concedido"))
            {
                return StatusCode(404, "voto inválido");
            }

            _context.votos.Add(newVoto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoto", new { id = request.Id }, request);

        }



        private bool CandidatoExists(Guid id)
        {
            return _context.RegistroCandidatos.Any(e => e.Id == id);
        }

        private bool VotoExists2(DateTime data)
        {
            return _context.votos.Any(e => e.DataVoto == data);
        }

       
    }
}
