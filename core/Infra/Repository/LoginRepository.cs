﻿using core.Domain.Entities;
using core.Domain.Interfaces;
using Dapper;
using System.Collections.Generic;

namespace core.Infra.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IRepositoryBase _repositoryBase;

        public LoginRepository(IRepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                var sql = @"INSERT INTO login
                                (nome,
                                email,
                                senha,
                                celular)
                                VALUES
                                (@Nome,
                                @Email,
                                @Senha,
                                @Celular);";
                connection.Execute(sql, usuario);
            }
        }

        public bool ExisteEmail(string email)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                var sql = "SELECT COUNT(1) FROM login WHERE Email = @Email";
                return connection.ExecuteScalar<int>(sql, new { Email = email }) > 0;
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

        public Usuario GetUsuarioPorId(int id)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                var sql = "SELECT * FROM Login WHERE Id = @Id";
                return connection.QuerySingleOrDefault<Usuario>(sql, new { Id = id });
            }
        }

        public void Update(Usuario usuario)
        {
            using (var connection = _repositoryBase.connMysql())
            {
                var sql = @"UPDATE Login 
                            SET Nome = @Nome, Email = @Email, Senha = @Senha, Celular = @Celular 
                            WHERE Id = @Id";
                connection.Execute(sql, usuario);
            }
        }
    }
}
