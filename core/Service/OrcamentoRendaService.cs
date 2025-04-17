using System.Collections.Generic;
using System.Threading.Tasks;

public class OrcamentoRendaService : IOrcamentoRendaService
{
    private readonly IOrcamentoRendaRepository _orcamentoRepository;

    public OrcamentoRendaService(IOrcamentoRendaRepository orcamentoRepository)
    {
        _orcamentoRepository = orcamentoRepository;
    }

    public async Task<IEnumerable<int>> ListarAnosDisponiveis(int idUsuario)
    {
        return await _orcamentoRepository.ListarAnosDisponiveis(idUsuario);
    }

    public async Task<List<dynamic>> ListarRendasAno(int idUsuario, int ano)
    {
        return _orcamentoRepository.ListarRendasAno(idUsuario, ano);
    }

    public Task<bool> SalvarOuAtualizar(List<OrcamentoRendaDTO> dados, int idUsuario)
    {
        return _orcamentoRepository.SalvarOuAtualizar(dados, idUsuario);
    }
}
