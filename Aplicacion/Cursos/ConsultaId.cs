using Dominio;
using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, Curso>
        {
            private readonly DbContextCursosOnline _dbContext;
            public Manejador(DbContextCursosOnline dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _dbContext.Curso.FindAsync(request.Id);
                return curso;
            }
        }
    }
}
