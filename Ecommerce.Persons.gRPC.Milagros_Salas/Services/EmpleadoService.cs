using Ecommerce.Persons.gRPC.Milagros_Salas.Datos;
using Grpc.Core;
using Microsoft.Data.SqlClient;

namespace Ecommerce.Persons.gRPC.Milagros_Salas.Services
{
    public class EmpleadoService : EmpleadoP.EmpleadoPBase
    {
        private readonly ILogger<EmpleadoService> _logger;

        public EmpleadoService(ILogger<EmpleadoService> logger)
        {
            _logger = logger;
        }

        public override Task<Empleados> Listar(EmptyEmpl request, ServerCallContext context)
        {
            EmpleadosDAO modelDao = new EmpleadosDAO();
            Empleados lista = new Empleados();
            lista.Items.AddRange(modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Empleado> Obtener(EmpleadoId request, ServerCallContext context)
        {
            EmpleadosDAO modelDao = new EmpleadosDAO();
            Empleado model = new Empleado();
            model = modelDao.Devolver(request.Id);
            return Task.FromResult(model);
        }

        public override Task<RespuestaEmpl> Crear(Empleado request, ServerCallContext context)
        {
            EmpleadosDAO modelDao = new EmpleadosDAO();
            RespuestaEmpl respuesta = new RespuestaEmpl();
            Empleado model = new Empleado();
            model.Nombres = request.Nombres;
            model.Apellidos = request.Apellidos;
            model.Usuario = request.Usuario;
            model.Password = request.Password;
            model.AnoRegistro = request.AnoRegistro;
            model.Activo = request.Activo;
            model.Sucursal = request.Sucursal;
            respuesta.Mensaje = modelDao.Insertar(model);
            return Task.FromResult(respuesta);
        }

    }
}
