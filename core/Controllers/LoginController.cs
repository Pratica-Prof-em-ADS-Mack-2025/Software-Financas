using Microsoft.AspNetCore.Mvc;
using core.Domain.Entities;
using core.Domain.Interfaces;
using System;
using core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using core.Infra.Repository;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService usuarioService)
        {
            _loginService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] UsuarioDto usuarioDto)
        {
            var result = _loginService.CadastrarUsuario(usuarioDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok("Usuário registrado com sucesso.");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = _loginService.Authenticate(loginDto);
                if (!result.Success)
                {
                    return BadRequest(new { Success = false, Message = result.Message });
                }

                HttpContext.Session.SetString("UserEmail", loginDto.Email);                

                return Ok(new { Success = true, Message = "Login bem-sucedido." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Erro ao realizar login!" + ex });
            }
        }       

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var email = HttpContext.Session.GetString("UserEmail");          

            HttpContext.Session.Clear();
            return Ok(new { Success = true, Message = "Logout bem-sucedido." });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario usuarioDto)
        {
            var result = _loginService.UpdateUsuario(id, usuarioDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok("Usuário atualizado com sucesso.");
        }

        private int GetUsuarioLogadoId()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("Usuário não está logado");

            var usuario = _loginService.GetUsuarioPorEmail(email);
            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            return usuario.Id;
        }
    }

    public class UsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Celular { get; set; }
    }
}

