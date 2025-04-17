using core.Domain.Entities;
using System.Collections.Generic;

namespace core.Domain.Interfaces
{
    public interface ILoginRepository
    {
        void CadastrarUsuario(Usuario usuario);
        bool ExisteEmail(string email);
        Usuario GetUsuarioPorId(int id);
        Usuario GetUsuarioPorEmail(string email);
        void Update(Usuario usuario);
    }
}
