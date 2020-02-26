using System;
using System.Diagnostics;
using System.Linq;
using Dapper;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using Npgsql;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly string _connectionString = "User ID=cgtvalpha;Password=cogtive;Server=localhost;Port=5432;Database=cogtive;";

        private ISessionFactory _sessionFactory;

        /// <summary>
        /// Constructor class.
        /// </summary>
        public ValuesController()
        {
            _sessionFactory = Fluently.Configure().Database(PostgreSQLConfiguration.Standard.ConnectionString(_connectionString)).BuildSessionFactory();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Dapper vs NHibernate";
        }

        // GET api/values/dapper
        [HttpGet("dapper")]
        public ActionResult<string> GetDapper()
        {
            string message = "";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("- Dapper: Start");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                var result = connection.Query<object>(@"SELECT * FROM sgca.""Permissoes"" p ").ToList();
                stopwatch.Stop();
                message = result.Count + " row(s) fetched - " + stopwatch.ElapsedMilliseconds + "ms";
                Console.WriteLine("- Dapper: Stop");
                Console.WriteLine("- Dapper: " + message);
            }

            return message;
        }

        // GET api/values/dapper
        [HttpGet("nhibernate")]
        public ActionResult<string> GetNHibernate()
        {
            string message = "";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("- NHibernate: Start");

            using (ISession session = _sessionFactory.OpenSession())
            {
                ISQLQuery query = session.CreateSQLQuery(@"SELECT * FROM sgca.""Permissoes"" p ");
                var result = query.List();
                stopwatch.Stop();
                message = result.Count + " row(s) fetched - " + stopwatch.ElapsedMilliseconds + "ms";
                Console.WriteLine("- NHibernate: Stop");
                Console.WriteLine("- NHibernate: " + message);
            }

            return message;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
