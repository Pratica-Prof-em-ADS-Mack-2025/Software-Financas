using core.Domain.Interfaces;
using core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrcamentoRendaController : ControllerBase
{
    private readonly IOrcamentoRendaService _orcamentoService;
    private readonly IParametroService _parametroService;

    public OrcamentoRendaController(IOrcamentoRendaService orcamentoService, IParametroService parametroService)
    {
        _orcamentoService = orcamentoService;
        _parametroService = parametroService;
    }   

    [HttpGet("{ano}")]
    public async Task<IActionResult> ListarOrcamentos(int ano)
    {
        var lista = await _orcamentoService.ListarRendasAno(GetUsuarioLogadoId(), ano);
        return Ok(lista);
    }

    [HttpPost("SalvarOuAtualizar")]
    public async Task<IActionResult> SalvarOuAtualizar([FromBody] RequestDTO request)
    {
        var result = await _orcamentoService.SalvarOuAtualizar(request.dados, GetUsuarioLogadoId());
        return Ok(result);
    }

    [HttpGet("AnosDisponiveis")]
    public async Task<IActionResult> GetAnosDisponiveis()
    {
        var anos = await _orcamentoService.ListarAnosDisponiveis(GetUsuarioLogadoId());
        return Ok(anos);
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

    public class RequestDTO
    {
        public List<OrcamentoRendaDTO> dados { get; set; }
    }
}
