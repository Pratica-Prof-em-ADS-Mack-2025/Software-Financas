using core.Domain.Entities.Models;
using core.Domain.Interfaces;
using core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParametroController : ControllerBase
    {
        private readonly IParametroService _parametroService;

        public ParametroController(IParametroService parametroService)
        {
            _parametroService = parametroService;
        }

        [HttpPost("salvar")]
        public IActionResult SalvarParametro([FromBody] ParametroDto parametroDto)
        {
            _parametroService.SalvarParametro(GetUsuarioLogadoId(), parametroDto);
            return Ok(new { success = true, message = "Parâmetro salvo com sucesso" });
        }

        [HttpGet("obter")]
        public IActionResult ObterParametro()
        {
            var parametros = _parametroService.ObterParametroPorUsuario(GetUsuarioLogadoId());
            if (parametros != null && parametros.Any())
            {
                return Ok(parametros);
            }
            return NotFound(new { success = false, message = "Parâmetro não encontrado" });
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult ExcluirParametro(int id)
        {
            _parametroService.ExcluirParametro(id, GetUsuarioLogadoId());
            return Ok(new { success = true, message = "Parâmetro excluído com sucesso" });
        }


        private int GetUsuarioLogadoId()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("Usuário não está logado");

            var usuario = _parametroService.GetUsuarioPorEmail(email);
            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            return usuario.Id;
        }
    }
}
