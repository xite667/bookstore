using Microsoft.AspNetCore.Mvc;
using HomeStore.Models;
using Npgsql;
using Dapper;
using System.Data;
using WebApplication1.Sql;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private static string ConnString = "Host=localhost;Username=postgres;Password=1043;Database=bookstore";

        private IDbConnection Conn => new NpgsqlConnection(ConnString);
        // Jeżeli nie zamkniesz tego conna to będziesz miał wyciek pamięci gdyż połącznie do bazy nie jest zamknięte

        [HttpGet("books")]
        public IEnumerable<Book> GetBooks()
        {
            using (var conn = Conn)
            {
                return conn.Query<Book>(BookQueries.GetBooks);          
            }
        }

        [HttpGet("books/{id}")]
        public Book GetBook(int id)
        {
            using (var conn = Conn)
            {
                return conn.QueryFirstOrDefault<Book>(
                    BookQueries.GetBookById, new { id }
                );
            }   
        }

        [HttpPost("books/new")]
        public int CreateBook([FromBody] NewBook book) 
        {
            using (var conn = Conn)
            {
                return conn.ExecuteScalar<int>(BookQueries.CreateBook, book);
            }        
        }

        [HttpPut("books/update/{id}")]
        public int UpdateBook(int id, [FromBody] UpdateBook book)
        {
            using (var conn = Conn)
            {
                return conn.ExecuteScalar<int>(
                    BookQueries.UpdateBook, 
                    new
                    {
                        Id = id,
                        book.AuthorName,
                        book.AuthorLastName,
                        book.Title,
                        book.Year
                    }
                );
            }
        }

        [HttpDelete("book/delete/{id}")]
        public bool DeleteBook(int id)
        {
            using (var conn = Conn)
            {
                return conn.ExecuteScalar<bool>(
                    BookQueries.DeleteBook, new { Id = id }
                );
            }
        }
    }
}
