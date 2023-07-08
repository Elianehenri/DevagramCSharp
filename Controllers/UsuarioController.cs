using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        //controle de logs
        public readonly ILogger<UsuarioController> _logger;


        public UsuarioController(ILogger<UsuarioController> logger,
            IUsuarioRepository usuarioRepository) : base(usuarioRepository)//herdando da base controller
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult ObterUsuario()
        {
            try
            {
                Usuario usuario = LerToken();

                return Ok(new UsuarioRespostaDto
                {
                    Email = usuario.Email,
                    Nome = usuario.Nome
                });

            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao obter o usuário");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu o seguinte erro: " + e.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
        //
        [HttpPut]
        public IActionResult AtualizarUsuario([FromForm] UsuarioRequisicaoDto usuariodto)
        {
            try
            {
                Usuario usuario = LerToken();
                //verifcar se o usuario existe
                if (usuariodto != null)
                {
                    var erros = new List<string>();

                    if (string.IsNullOrEmpty(usuariodto.Nome) || string.IsNullOrWhiteSpace(usuariodto.Nome))
                    {
                        erros.Add("Nome inválido");
                    }

                    if (erros.Count > 0)
                    {
                        return BadRequest(new ErrorRespostaDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Erros = erros
                        });
                    }
                    else
                    {
                        //se o usuario existir editar  foto
                        CosmicService cosmicservice = new CosmicService();
                        //envia a imagem para o cosmic eele retorna uma url
                        usuario.FotoPerfil = cosmicservice.EnviarImagem(new ImagemDto { Imagem = usuariodto.FotoPerfil, Nome = usuariodto.Nome.Replace(" ", "") });
                        usuario.Nome = usuariodto.Nome;
                        //informar o banco de dados que o usuario foi alterado
                        _usuarioRepository.AtualizarUsuario(usuario);
                    }
                }

                return Ok("Usuário foi salvo com sucesso");

            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao salvar o usuário");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu o seguinte erro: " + e.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
        //salvar novo usuario
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SalvarUsuario([FromForm] UsuarioRequisicaoDto usuariodto)
        {
            try
            {//se tudo estiver tudo conforme as regras

                if (usuariodto != null)
                {
                    var erros = new List<string>();

                    if (string.IsNullOrEmpty(usuariodto.Nome) || string.IsNullOrWhiteSpace(usuariodto.Nome))
                    {
                        erros.Add("Nome inválido");
                    }
                    if (string.IsNullOrEmpty(usuariodto.Email) || string.IsNullOrWhiteSpace(usuariodto.Email) || !usuariodto.Email.Contains("@"))
                    {
                        erros.Add("E-mail inválido");
                    }
                    if (string.IsNullOrEmpty(usuariodto.Senha) || string.IsNullOrWhiteSpace(usuariodto.Senha))
                    {
                        erros.Add("Senha inválido");
                    }

                    if (erros.Count > 0)
                    {
                        return BadRequest(new ErrorRespostaDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Erros = erros
                        });
                    }
                   
                    CosmicService cosmicservice = new CosmicService();
                    //transformar o dados em model
                    Usuario usuario = new Usuario()
                    {
                        Email = usuariodto.Email,
                        Senha = usuariodto.Senha,
                        Nome = usuariodto.Nome,
                        FotoPerfil = cosmicservice.EnviarImagem(new ImagemDto { Imagem = usuariodto.FotoPerfil, Nome = usuariodto.Nome.Replace(" ", "") })
                    };


                    usuario.Senha = Utils.MD5Utils.GerarHashMD5(usuario.Senha);
                    usuario.Email = usuariodto.Email.ToLower();

                    if (!_usuarioRepository.VerificarEmail(usuario.Email))
                    {
                        _usuarioRepository.Salvar(usuario);
                    }
                    else
                    {
                        return BadRequest(new ErrorRespostaDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Descricao = "Usuário já está cadastrado!"
                        });
                    }

                }

                return Ok("Usuario Salvo com Sucesso.");
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao salvar o usuário");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu o seguinte erro: " + e.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }

}

