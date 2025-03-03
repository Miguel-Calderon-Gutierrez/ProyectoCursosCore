using MediatR;
using Persistencia;
using System.Threading.Tasks;
using System.Threading;
using System;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public int CursoId { get; set; }           
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
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new {mensaje = "No se encontró el curso" }); 
                }

                _dbContext.Remove(curso);
                var valor = await _dbContext.SaveChangesAsync();

                if (valor != 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudieron guardar los cambios");
            }
        }
    }
}
