using System.Data;
using Npgsql;

namespace WebApplication1
{
    public class Connection
    {
        public IDbConnection Conn { get; set; }
        public Connection(string connectionString)
        {
            Conn = new NpgsqlConnection(connectionString);
        }

    }
}
