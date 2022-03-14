using GerenciadorDeCursosApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace GerenciadorDeCursosApi.Services
{
    public class TokenServices
    {
        public static string GerarToken (UsuarioModel usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, usuario.Login.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, usuario.Role.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "https://localhost:5001",
                Issuer = "DesafioFinal",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
