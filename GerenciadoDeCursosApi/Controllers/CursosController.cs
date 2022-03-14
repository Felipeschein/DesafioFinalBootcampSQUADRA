using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeCursosApi.Data;
using GerenciadorDeCursosApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorDeCursosApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly GerenciadorDeCursosContext _context;

        public CursosController (GerenciadorDeCursosContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet("ListaDeCursos")]
        public async Task<ActionResult<IEnumerable<CursoModel>>> ListarCursosAsync ()
        {
            return await _context.CursosModels.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("ListaDeCursos{status}")]
        public async Task<List<CursoModel>> ListaCursosStatusAsync (string status)
        {

            var cursosModel = await _context.CursosModels.Where(m => m.Status == status).ToListAsync();

            return cursosModel;
        }

        [Authorize(Roles = "Secretaria, Gerente")]
        [HttpPut("AtualizarCurso{id}")]
        public async Task<IActionResult> AtualizarCursosAsync(int id, CursoModel cursosModel)
        {
            if (id != cursosModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(cursosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursosModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [Authorize(Roles = "Gerente")]
        [HttpPost("CadastrarCursos")]
        [ActionName(nameof(CadastrarCursosAsync))]
        public async Task<ActionResult<CursoModel>> CadastrarCursosAsync([FromBody] CursoModel cursosModel)
        {
            _context.CursosModels.Add(cursosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CadastrarCursosAsync", new { id = cursosModel.Id }, cursosModel);
        }
        [Authorize(Roles = "Gerente")]
        [HttpDelete("ExcluirCursos{id}")]
        public async Task<IActionResult> ExcluirCursosAsync(int id)
        {
            var cursosModel = await _context.CursosModels.FindAsync(id);
            if (cursosModel == null)
            {
                return NotFound();
            }

            _context.CursosModels.Remove(cursosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursosModelExists(int id)
        {
            return _context.CursosModels.Any(e => e.Id == id);
        }
    }
}
