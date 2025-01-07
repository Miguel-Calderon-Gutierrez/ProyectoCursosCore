using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {     

        private readonly DbContextCursosOnline dbContext;

        public WeatherForecastController(DbContextCursosOnline _dbContext)
        {
            this.dbContext = _dbContext;
        }

        [HttpGet]
        public IEnumerable<Curso> Get()
        {
           var cursos = dbContext.Curso.ToList();
            return cursos;
        }
    }
}
