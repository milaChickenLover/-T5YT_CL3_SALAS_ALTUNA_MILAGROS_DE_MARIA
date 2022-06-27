using Ecommerce.Persons.gRPC.Milagros_Salas;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using Ecommerce.Persons.gRPCClient.Milagros_Salas.Models;

namespace Ecommerce.Persons.gRPCClient.Milagros_Salas.Controllers
{
    public class ClientesController : Controller
    {
        private ClienteP.ClientePClient _client;

        public ClientesController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7006");
            _client = new ClienteP.ClientePClient(canal);
        }

        public async Task<IActionResult> Index()
        {        
            List<ClienteModel> lista = new List<ClienteModel>();
            var data = await _client.ListarAsync(new Empty { });
            foreach (var item in data.Items)
            {
                ClienteModel model = new ClienteModel();
                model.Id = Convert.ToInt32(item.Id);
                model.Nombres = item.Nombres;
                model.Apellidos = item.Apellidos;
                model.Usuario = item.Usuario;
                model.Password = item.Password;
                model.FechaRegistro = Convert.ToDateTime(item.FechaRegistro);
                model.Activo = Convert.ToBoolean(item.Activo);
                model.Ciudad = item.Ciudad;
                lista.Add(model);
            }
            
            return View(lista);
        }

        public async Task<IActionResult> Details(string Id)
        {
            ClienteModel model = new ClienteModel();
            var id = new ClienteId();
            id.Id = Id;
            var data = await _client.ObtenerAsync(id);
            model.Id = Convert.ToInt32(data.Id);
            model.Nombres = data.Nombres;
            model.Apellidos = data.Apellidos;
            model.Usuario = data.Usuario;
            model.Password = data.Password;
            model.FechaRegistro = Convert.ToDateTime(data.FechaRegistro);
            model.Activo = Convert.ToBoolean(data.Activo);
            model.Ciudad = data.Ciudad;
            return View(model);
        }

        public IActionResult Create() //metodo no necesita ser asincrono debido a que solo llama una vista y no hace uso de algun metodo asincrono
        {
            return View(new ClienteModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteModel modelRequest)
        {
            var model = new Cliente();
            model.Id = "0";
            model.Nombres = modelRequest.Nombres;
            model.Apellidos = modelRequest.Apellidos;
            model.Usuario = modelRequest.Usuario;
            model.Password = modelRequest.Password;
            model.FechaRegistro = modelRequest.FechaRegistro.ToString();
            model.Activo = "true";//Cliente siempre se registra como activo
            model.Ciudad = modelRequest.Ciudad;

            var dato = await _client.CrearAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View();
        }
    }
}
