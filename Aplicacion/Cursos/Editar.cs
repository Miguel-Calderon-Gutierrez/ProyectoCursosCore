using Dominio;
using MediatR;
using Persistencia;
using System.Threading.Tasks;
using System.Threading;
using System;
using FluentValidation;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
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
                var curso = await _dbContext.Curso.FindAsync(request.CursoId);

                if (curso == null) {
                    throw new Exception("El curso no existe");
                }
                
                curso.Titulo = request.Titulo ?? curso.Titulo ;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                var valor = await _dbContext.SaveChangesAsync();

                if (valor != 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo actualizar el curso");
            }
        }

    }
}
