using GerenciadorDeCursosApi.Data;
using GerenciadorDeCursosApi.Models;
using GerenciadorDeCursosApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorDeCursosApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly GerenciadorDeCursosContext _context;

        public AutenticacaoController (GerenciadorDeCursosContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> AutenticacaoUsuarioAsync ([FromBody] UsuarioModel usuariosModel)
        {
            // Recupera o usuário
            var user = await _context.UsuariosModels.FindAsync(usuariosModel.Id);
            

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenServices.GerarToken(usuariosModel);

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };

        }
    }
}
