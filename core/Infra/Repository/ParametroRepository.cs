using core.Domain.Entities;
using core.Domain.Entities.Models;
using core.Domain.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace core.Infra.Repository
{
    public class ParametroRepository : IParametroRepository
    {
        private readonly IRepositoryBase _repositoryBase;

        public ParametroRepository(IRepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public void SalvarParametro(int idUsuario, ParametroDto parametro)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                if (parametro.Id.HasValue && parametro.Id.Value > 0)
                {
                    // Atualiza o parâmetro existente
                    var sqlUpdate = "UPDATE parametro SET tipo = @Categoria, sub_tipo = @Subcategoria WHERE id = @Id AND IdUsuario = @IdUsuario";
                    connection.Execute(sqlUpdate, new
                    {
                        Id = parametro.Id,
                        IdUsuario = idUsuario,
                        Categoria = parametro.Categoria,
                        Subcategoria = parametro.Subcategoria
                    });
                }
                else
                {
                    // Insere um novo parâmetro
                    var sqlInsert = "INSERT INTO parametro (IdUsuario, tipo, sub_tipo) VALUES (@IdUsuario, @Categoria, @Subcategoria)";
                    connection.Execute(sqlInsert, new
                    {
                        IdUsuario = idUsuario,
                        Categoria = parametro.Categoria,
                        Subcategoria = parametro.Subcategoria
                    });
                }
            }
        }
        public List<Parametro> ObterParametroPorUsuario(int idUsuario)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                var sql = "SELECT * FROM parametro WHERE IdUsuario = @IdUsuario";
                return connection.Query<Parametro>(sql, new { IdUsuario = idUsuario }).ToList();
            }
        }

        public Usuario GetUsuarioPorEmail(string email)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                var sql = "SELECT * FROM Login WHERE Email = @Email";
                return connection.QuerySingleOrDefault<Usuario>(sql, new { Email = email });
            }
        }

        public void ExcluirParametro(int id, int idUsuario)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                var sql = "DELETE FROM parametro WHERE Id = @Id AND IdUsuario = @IdUsuario";
                connection.Execute(sql, new { Id = id, IdUsuario = idUsuario });
            }
        }

    }
}
