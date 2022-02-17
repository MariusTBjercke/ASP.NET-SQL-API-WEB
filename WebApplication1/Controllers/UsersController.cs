using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private string _connectionString = @"Data Source=DESKTOP-LDL14VM\SQLEXPRESS;Initial Catalog=MYTESTSQL;Integrated Security=True";

        // GET: api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            var query = $@"SELECT * FROM Users2 where id = {id}";
            var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(query);
        }

        // GET api/<UsersController>
        [HttpGet]
        public async Task<List<User>> Get()
        {
            var query = $@"SELECT * FROM Users2";
            var connection = new SqlConnection(_connectionString);
            return (List<User>) await connection.QueryAsync<User>(query);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task Post(User user)
        {
            var query = $@"INSERT INTO Users2 (firstname, lastname, address, city) VALUES ('{user.FirstName}', '{user.LastName}', '{user.Address}', '{user.City}')";
            await Db(query);
        }

        // PUT api/<UsersController>/5
        [HttpPatch]
        public async Task Put(User user)
        {
            var query = $@"UPDATE Users2 SET firstname = '{user.FirstName}', lastname = {user.LastName}, address = {user.Address}, city = {user.City} WHERE id = {user.Id}";
            await Db(query);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var query = $@"DELETE FROM Users2 WHERE id = {id}";
            await Db(query);
        }

        public async Task Db(string query)
        {
            var connection = new SqlConnection(_connectionString);
            int rowsAffected = await connection.ExecuteAsync(query);
        }
    }
}
