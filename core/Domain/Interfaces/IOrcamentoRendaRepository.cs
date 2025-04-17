using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrcamentoRendaRepository
{
    Task<IEnumerable<int>> ListarAnosDisponiveis(int idUsuario);
    List<dynamic> ListarRendasAno(int idUsuario, int ano);
    Task<bool> SalvarOuAtualizar(List<OrcamentoRendaDTO> dados, int idUsuario);
}
