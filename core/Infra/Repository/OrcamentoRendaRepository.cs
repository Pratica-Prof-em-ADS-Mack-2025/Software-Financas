using core.Domain.Entities.Models;
using core.Domain.Interfaces;
using Dapper;
using DocumentFormat.OpenXml.InkML;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.Infra.Repository
{
    public class OrcamentoRendaRepository : IOrcamentoRendaRepository
    {
        private readonly IRepositoryBase _repositoryBase;

        public OrcamentoRendaRepository(IRepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<IEnumerable<int>> ListarAnosDisponiveis(int idUsuario)
        {
            using var connection = _repositoryBase.connMysql();

            var sql = @"
            SELECT DISTINCT Ano
            FROM Orcamento
            WHERE id_usuario = @idUsuario
            ORDER BY Ano DESC";

            var result = await connection.QueryAsync<int>(sql, new { idUsuario });

            return result;
        }

        public List<dynamic> ListarRendasAno(int idUsuario, int ano)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                var sql = @"
            SELECT 
                p.id AS IdParametro, 
                p.sub_tipo AS Nome,
                o1.valor AS Jan, o2.valor AS Fev, o3.valor AS Mar, o4.valor AS Abr,
                o5.valor AS Mai, o6.valor AS Jun, o7.valor AS Jul, o8.valor AS Ago,
                o9.valor AS Sete, o10.valor AS Outu, o11.valor AS Nov, o12.valor AS Dez
            FROM parametro p
            LEFT JOIN orcamento o1 ON o1.tipo = 'RENDA' AND o1.mes = 1 AND o1.ano = @Ano AND o1.id_usuario = @IdUsuario
            LEFT JOIN orcamento o2 ON o2.tipo = 'RENDA' AND o2.mes = 2 AND o2.ano = @Ano AND o2.id_usuario = @IdUsuario
            LEFT JOIN orcamento o3 ON o3.tipo = 'RENDA' AND o3.mes = 3 AND o3.ano = @Ano AND o3.id_usuario = @IdUsuario
            LEFT JOIN orcamento o4 ON o4.tipo = 'RENDA' AND o4.mes = 4 AND o4.ano = @Ano AND o4.id_usuario = @IdUsuario
            LEFT JOIN orcamento o5 ON o5.tipo = 'RENDA' AND o5.mes = 5 AND o5.ano = @Ano AND o5.id_usuario = @IdUsuario
            LEFT JOIN orcamento o6 ON o6.tipo = 'RENDA' AND o6.mes = 6 AND o6.ano = @Ano AND o6.id_usuario = @IdUsuario
            LEFT JOIN orcamento o7 ON o7.tipo = 'RENDA' AND o7.mes = 7 AND o7.ano = @Ano AND o7.id_usuario = @IdUsuario
            LEFT JOIN orcamento o8 ON o8.tipo = 'RENDA' AND o8.mes = 8 AND o8.ano = @Ano AND o8.id_usuario = @IdUsuario
            LEFT JOIN orcamento o9 ON o9.tipo = 'RENDA' AND o9.mes = 9 AND o9.ano = @Ano AND o9.id_usuario = @IdUsuario
            LEFT JOIN orcamento o10 ON o10.tipo = 'RENDA' AND o10.mes = 10 AND o10.ano = @Ano AND o10.id_usuario = @IdUsuario
            LEFT JOIN orcamento o11 ON o11.tipo = 'RENDA' AND o11.mes = 11 AND o11.ano = @Ano AND o11.id_usuario = @IdUsuario
            LEFT JOIN orcamento o12 ON o12.tipo = 'RENDA' AND o12.mes = 12 AND o12.ano = @Ano AND o12.id_usuario = @IdUsuario
            WHERE p.tipo = 'RENDA' AND p.idUsuario = @IdUsuario";

                return connection.Query(sql, new { IdUsuario = idUsuario, Ano = ano }).ToList();
            }
        }


        public async Task<bool> SalvarOuAtualizar(List<OrcamentoRendaDTO> dados, int idUsuario)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"
                    INSERT INTO orcamento (id_usuario, mes, ano, valor, tipo)
                    VALUES (@IdUsuario, @Mes, @Ano, @Valor, @Tipo)
                    ON DUPLICATE KEY UPDATE valor = @Valor";

                        foreach (var item in dados)
                        {
                            await connection.ExecuteAsync(sql, new
                            {
                                IdUsuario = idUsuario,
                                Mes = item.Mes,
                                Ano = item.Ano,
                                Valor = item.Valor,
                                Tipo = "Renda"
                            }, transaction);
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

    }
}
