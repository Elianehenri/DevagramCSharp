using DevagramCSharp.Models;
using DevagramCSharp.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevagramCSharp.Services
{
    public class TokenService
    {
        //criar um token com os dados do usuário
        public static string CriarToken(Usuario usuario)
        {
                //create a token handler
                var tokenHandler = new JwtSecurityTokenHandler();
                //cria uma chave com a chave secreta
                var key = Encoding.ASCII.GetBytes(ChaveJWT.ChaveSecreta);
            //crie um descritor de token com a chave, a data de expiração, os dados do usuário e o algoritmo
            var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Name, usuario.Nome)
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
            //criar um token com o descritor de token
            var token = tokenHandler.CreateToken(tokenDescriptor);
                //returna o token
                return tokenHandler.WriteToken(token);
            }
    }
}
