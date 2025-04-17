using core.Domain.Entities;
using core.Domain.Entities.Models;
using core.Domain.Interfaces;
using core.Infra.Repository;
using System.Collections.Generic;

namespace core.Service
{
    public class ParametroService : IParametroService
    {
        private readonly IParametroRepository _parametroRepository;

        public ParametroService(IParametroRepository parametroRepository)
        {
            _parametroRepository = parametroRepository;
        }

        public void SalvarParametro(int idUsuario, ParametroDto parametroDto)
        {
            _parametroRepository.SalvarParametro(idUsuario, parametroDto);
        }

        public List<Parametro> ObterParametroPorUsuario(int idUsuario)
        {
            return _parametroRepository.ObterParametroPorUsuario(idUsuario);
        }

        public Usuario GetUsuarioPorEmail(string email)
        {
            return _parametroRepository.GetUsuarioPorEmail(email);
        }

        public void ExcluirParametro(int id, int idUsuario)
        {
            _parametroRepository.ExcluirParametro(id, idUsuario);
        }


    }
}
