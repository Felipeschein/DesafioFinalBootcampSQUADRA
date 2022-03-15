using GerenciadorDeCursosApi.Data;
using GerenciadorDeCursosApi.DTOs;
using GerenciadorDeCursosApi.Enum;
using GerenciadorDeCursosApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public List<CursoModel> ListarCursosStatus (StatusEnum? status)
        {
            if (status == null)
            {
                return _context.CursosModels.ToList();
            }
            var cursosStatus = _context.CursosModels.Where(s => s.Status == status).ToList();
            return cursosStatus;

        }
        [Authorize(Roles = "Secretaria, Gerente")]
        [HttpPut("AtualizarCurso/{id}")]
        public ActionResult<CursoModel> AtualizarCursos (int id, AtualizarCursoDTO cursosModel)
        {

            var curso = _context.CursosModels.Find(id);
            curso.Status = cursosModel.Status;
            _context.SaveChanges();

            return Ok(curso);
        }
        [Authorize(Roles = "Gerente")]
        [HttpPost("CadastrarCursos")]
        [ActionName(nameof(CadastrarCursosAsync))]
        public async Task<ActionResult<CursoModel>> CadastrarCursosAsync ([FromBody] CursoModel cursosModel)
        {

            _context.CursosModels.Add(cursosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CadastrarCursosAsync", new { id = cursosModel.Id }, cursosModel);
        }
        [Authorize(Roles = "Gerente")]
        [HttpDelete("ExcluirCursos{id}/")]
        public async Task<IActionResult> ExcluirCursosAsync (int id)
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

        private bool CursosModelExists (int id)
        {
            return _context.CursosModels.Any(e => e.Id == id);
        }
    }
}
