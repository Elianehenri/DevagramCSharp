using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //controlar os logs
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto loginrequisicao)
        {
            try
            {//SE ELE NAO FOR NULO E NEM VAZIO E NEM ESPAÇO EM BRANCO ELE VAI ENTRAR NO IF
                if (!String.IsNullOrEmpty(loginrequisicao.Senha) && !String.IsNullOrEmpty(loginrequisicao.Email) &&
                    !String.IsNullOrWhiteSpace(loginrequisicao.Senha) && !String.IsNullOrWhiteSpace(loginrequisicao.Email))
                {
                    string email = "nani@teste.com.br";
                    string senha = "Senha126@";
                    //validar se a requisiçao esta com essa senha e email
                    if (loginrequisicao.Email == email && loginrequisicao.Senha == senha)
                    {
                        Usuario usuario = new Usuario()
                        {
                            Email = loginrequisicao.Email,
                            Id = 6,
                            Nome = "Eliane"
                        };

                        return Ok(new LoginRespostaDto()
                        {
                            Email = usuario.Email,
                            Nome = usuario.Nome,
                            Token = TokenService.CriarToken(usuario)
                        });
                        
                    }
                    else
                    {
                        return BadRequest(new ErrorRespostaDto()
                        {
                            Descricao = "Email ou sennha inválido!",
                            Status = StatusCodes.Status400BadRequest
                        });
                    }
                }
                else
                {
                    return BadRequest(new ErrorRespostaDto()
                    {
                        Descricao = "Usuário não preencheu os campos de login corretamente",
                        Status = StatusCodes.Status400BadRequest
                    });
                }
            
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro no login: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu um erro ao fazer o login",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }


    }
}
