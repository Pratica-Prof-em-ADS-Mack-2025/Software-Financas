﻿using core.Controllers;
using core.Domain.Entities;
using core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace core.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _usuarioRepository;

        public LoginService(ILoginRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public Usuario GetUsuarioPorId(int id)
        {
            return _usuarioRepository.GetUsuarioPorId(id);
        }

        public ServiceResult CadastrarUsuario(UsuarioDto usuarioDto)
        {
            if (_usuarioRepository.ExisteEmail(usuarioDto.Email))
            {
                return new ServiceResult { Success = false, Message = "Email já registrado." };
            }

            var hashedPassword = HashPassword(usuarioDto.Senha);

            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = hashedPassword,
                Celular = usuarioDto.Celular
            };

            _usuarioRepository.CadastrarUsuario(usuario);

            return new ServiceResult { Success = true };
        }

        public ServiceResult UpdateUsuario (int id, Usuario usuarioDto)
        {
            var usuario = _usuarioRepository.GetUsuarioPorId(id);
            if (usuario == null)
            {
                return new ServiceResult { Success = false, Message = "Usuário não encontrado." };
            }

            usuario.Nome = usuarioDto.Nome;
            usuario.Email = usuarioDto.Email;

            if (!string.IsNullOrEmpty(usuarioDto.Senha))
            {
                usuario.Senha = HashPassword(usuarioDto.Senha);
            }

            _usuarioRepository.Update(usuario);

            return new ServiceResult { Success = true, Message = "Usuário atualizado com sucesso." };
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public ServiceResult Authenticate(LoginDto loginDto)
        {
            var usuario = _usuarioRepository.GetUsuarioPorEmail(loginDto.Email);
            if (usuario == null || !VerifyPassword(loginDto.Senha, usuario.Senha))
            {
                return new ServiceResult { Success = false, Message = "Email ou senha inválidos." };
            }

            return new ServiceResult { Success = true, Message = "Login bem-sucedido." };
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedInput = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                var hashedInputString = BitConverter.ToString(hashedInput).Replace("-", "").ToLower();
                return hashedInputString == storedHash;
            }
        }       

        public Usuario GetUsuarioPorEmail(string email)
        {
            return _usuarioRepository.GetUsuarioPorEmail(email);
        }
    }

    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

