using Dominio;
using MediatR;
using Persistencia;
using System;
using static Aplicacion.Cursos.ConsultaId;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            [Required(ErrorMessage = "Por favor ingrese el titulo")]
            public string Titulo { get; set; }
            [Required(ErrorMessage = "Por favor ingrese una descripción")]
            public string Descripcion { get; set; }        
            public DateTime FechaPublicacion { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly DbContextCursosOnline _dbContext;
            public Manejador(DbContextCursosOnline dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };

                _dbContext.Curso.Add(curso);

                var valor = await _dbContext.SaveChangesAsync();

                if (valor != 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el curso");
            }
        }

    }
}
