using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadoDeCursosApi.Data;
using GerenciadoDeCursosApi.Models;

namespace GerenciadoDeCursosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly GerenciadorDeCursosContext _context;

        public UsuariosController(GerenciadorDeCursosContext context)
        {
            _context = context;
        }

        [HttpGet("ListaDeUsuarios")]
        public async Task<ActionResult<IEnumerable<UsuariosModel>>> GetUsuariosModels()
        {
            return await _context.UsuariosModels.ToListAsync();
        }

        [HttpGet("ListaDeUsuarios{id}")]
        public async Task<ActionResult<UsuariosModel>> ListaUsuariosModelAsync(int id)
        {
            var usuariosModel = await _context.UsuariosModels.FindAsync(id);

            if (usuariosModel == null)
            {
                return NotFound();
            }

            return usuariosModel;
        }


        [HttpPut("AtualizarUsuario{id}")]
        public async Task<IActionResult> AtualizaUsuariosModelAsync(int id, UsuariosModel usuariosModel)
        {
            if (id != usuariosModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuariosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosModelExists(id))
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


        [HttpPost("CadastrarUsuarios")]
        public async Task<ActionResult<UsuariosModel>> CadastrarUsuariosModelAsync(UsuariosModel usuariosModel)
        {
            _context.UsuariosModels.Add(usuariosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuariosModel", new { id = usuariosModel.Id }, usuariosModel);
        }

        [HttpDelete("ExcluirUsuarios{id}")]
        public async Task<IActionResult> DeleteUsuariosModelAsync(int id)
        {
            var usuariosModel = await _context.UsuariosModels.FindAsync(id);
            if (usuariosModel == null)
            {
                return NotFound();
            }

            _context.UsuariosModels.Remove(usuariosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosModelExists(int id)
        {
            return _context.UsuariosModels.Any(e => e.Id == id);
        }
    }
}
