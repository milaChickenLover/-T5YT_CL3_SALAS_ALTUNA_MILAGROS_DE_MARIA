using Ecommerce.Persons.gRPC.Milagros_Salas;
using Ecommerce.Persons.gRPC.Milagros_Salas.Datos;
using Grpc.Core;
using Microsoft.Data.SqlClient;

namespace Ecommerce.Persons.gRPC.Milagros_Salas.Services
{
    public class ClienteService : ClienteP.ClientePBase
    {
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(ILogger<ClienteService> logger)
        {
            _logger = logger;
        }

        public override Task<Clientes> Listar(Empty request, ServerCallContext context)
        {
            ClientesDAO modelDao = new ClientesDAO();
            Clientes lista = new Clientes();
            lista.Items.AddRange(modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Cliente> Obtener(ClienteId request, ServerCallContext context)
        {
            ClientesDAO modelDao = new ClientesDAO();
            Cliente model = new Cliente();
            model = modelDao.Devolver(request.Id);
            return Task.FromResult(model);
        }

        public override Task<Respuesta> Crear(Cliente request, ServerCallContext context)
        {
            ClientesDAO modelDao = new ClientesDAO();
            Respuesta respuesta = new Respuesta();
            Cliente model = new Cliente();
            model.Nombres = request.Nombres;
            model.Apellidos = request.Apellidos;
            model.Usuario = request.Usuario;
            model.Password = request.Password;
            model.FechaRegistro = request.FechaRegistro;
            model.Activo = request.Activo;
            model.Ciudad = request.Ciudad;
            respuesta.Mensaje = modelDao.Insertar(model);
            return Task.FromResult(respuesta);
        }

    }
}
