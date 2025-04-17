namespace core.Domain.Interfaces
{
    using System.Data.SqlClient;
    using MySql.Data.MySqlClient;

    public interface IRepositoryBase
    {
        SqlConnection conn();
        MySqlConnection connMysql();
    }
}