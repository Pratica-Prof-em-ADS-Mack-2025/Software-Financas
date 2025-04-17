using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IOrcamentoRendaService
{
    Task<IEnumerable<int>> ListarAnosDisponiveis(int idUsuario);
    Task<List<dynamic>> ListarRendasAno(int idUsuario, int ano);
    Task<bool> SalvarOuAtualizar(List<OrcamentoRendaDTO> dados, int idUsuario);
}
