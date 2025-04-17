
using core.Domain.Interfaces;


namespace core.Infra.Repository
{
    using MySql.Data.MySqlClient;
    using System.Data.SqlClient;

    public class RepositoryBase : IRepositoryBase
    {
        private readonly string _connectionString;
        private readonly string _myConnectionString;
        public RepositoryBase(string connectionString, string myConnectionString)
        {
            _connectionString = connectionString;   // Injetando a string de conexão no construtor da classe
            _myConnectionString = myConnectionString;
        }

        public SqlConnection conn()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            return connection;
        }

        public MySqlConnection connMysql()
        {
            MySqlConnection connection = new MySqlConnection(_myConnectionString);

            return connection;
        }
    }
}
