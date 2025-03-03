using System;
using System.Net;

namespace Aplicacion.ManejadorError
{
    public class ManejadorExepcion : Exception
    {
        public HttpStatusCode Codigo { get; }
        public Object Errores { get; }

        public ManejadorExepcion(HttpStatusCode codigo, object errores = null)
        {
            Codigo = codigo;
            Errores = errores;
        }
    }
}
