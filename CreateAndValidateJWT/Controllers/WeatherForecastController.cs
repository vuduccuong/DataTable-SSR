using CreateAndValidateJWT.Data;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAndValidateJWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _config;
        private readonly JwtDbContext _db;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, JwtDbContext db)
        {
            _logger = logger;
            _config = config;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("{id:int}")]
        public IActionResult Login(int id)
        {
            var u = _db.Users.Find(id);
            var strKey = _config.GetSection("JwtKey").Value;
            var key = Encoding.ASCII.GetBytes(strKey);
            var tk = JWTHelpers.GenerationToken(u.Id,key);
            return Ok(new {
            user = u,
            token = "Bear "+ tk
            });
        }
    }
}
