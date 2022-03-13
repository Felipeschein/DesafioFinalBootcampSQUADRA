using GerenciadoDeCursosApi.Data;
using GerenciadoDeCursosApi.Models;
using GerenciadoDeCursosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadoDeCursosApi.Controllers
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
        public async Task<ActionResult<dynamic>> AutenticacaoUsuario ([FromBody] UsuariosModel usuariosModel)
        {
            // Recupera o usuário
            var user = await _context.UsuariosModels.FindAsync(usuariosModel.Id);
            

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenServices.GenerateToken(usuariosModel);

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };

        }
    }
}
