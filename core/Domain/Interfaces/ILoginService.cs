using core.Controllers;
using core.Domain.Entities;
using System.Collections.Generic;

namespace core.Service
{
    public interface ILoginService
    {
        ServiceResult Authenticate(LoginDto loginDto);  
        ServiceResult CadastrarUsuario(UsuarioDto usuarioDto);  
        Usuario GetUsuarioPorId(int id); 
        ServiceResult UpdateUsuario(int id, Usuario usuario);      
        Usuario GetUsuarioPorEmail(string email);     
    }
}

