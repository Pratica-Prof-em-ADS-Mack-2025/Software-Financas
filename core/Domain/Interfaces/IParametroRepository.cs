using core.Domain.Entities;
using core.Domain.Entities.Models;
using System.Collections.Generic;

namespace core.Domain.Interfaces
{
    public interface IParametroRepository
    {
        void SalvarParametro(int idUsuario, ParametroDto dtoParametro);
        List<Parametro> ObterParametroPorUsuario(int idUsuario);
        Usuario GetUsuarioPorEmail(string email);
        void ExcluirParametro(int id, int idUsuario);
    }
}
