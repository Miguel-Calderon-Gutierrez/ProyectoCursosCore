﻿using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;
using System.Net;
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

                if (curso == null)
                {                
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el curso" });
                }
                return curso;
            }
        }
    }
}
