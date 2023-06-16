using System.Data;
using Dapper;
using HomeStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Npgsql;
using WebApplication1.Sql;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ClientController
    {
        private static string ConnString = "Host=localhost;Username=postgres;Password=1043;Database=bookstore";

        private IDbConnection Conn => new NpgsqlConnection(ConnString);

        [HttpGet("clients")]

        public IEnumerable<Client> GetClients()
        {
            using (var conn = Conn)
            {
                return conn.Query<Client>(ClientQueries.GetClient);
            }
        }

        [HttpPost("clients/new")]

        public int AddClient([FromBody] NewClient client) 
        {
            using(var conn = Conn)
            {
                return conn.ExecuteScalar<int>(ClientQueries.AddClient, client);
            }       
        }

        [HttpPut("clients/update/{id}")]
        public int UpdateClient(int id, [FromBody] Client client)
        {
            using (var conn = Conn)
            {
                return conn.ExecuteScalar<int>(
                    ClientQueries.UpdataClient,
                    new
                    {
                        Id = id,
                        client.ClientName,
                        client.ClientLastName,
                        client.Email,
                        client.Phone
                    }
                );
            }
        }

        [HttpDelete("clients/delete/{id}")]

        public bool DeleteClient(int id)
        {
            using (var conn = Conn)
            {
                return conn.ExecuteScalar<bool>(
                    ClientQueries.DeleteClient, new {Id = id}    
                );
            }
        }
    }
}
