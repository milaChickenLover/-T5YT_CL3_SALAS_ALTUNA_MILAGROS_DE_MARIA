using Ecommerce.Persons.gRPC.Milagros_Salas;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using Ecommerce.Persons.gRPCClient.Milagros_Salas.Models;

namespace Ecommerce.Persons.gRPCClient.Milagros_Salas.Controllers
{
    public class EmpleadosController : Controller
    {
        private EmpleadoP.EmpleadoPClient _client;

        public EmpleadosController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7006");
            _client = new EmpleadoP.EmpleadoPClient(canal);
        }

        public async Task<IActionResult> Index()
        {
            List<EmpleadoModel> lista = new List<EmpleadoModel>();
            var data = await _client.ListarAsync(new EmptyEmpl { });
            foreach (var item in data.Items)
            {
                EmpleadoModel model = new EmpleadoModel();
                model.Id = Convert.ToInt32(item.Id);
                model.Nombres = item.Nombres;
                model.Apellidos = item.Apellidos;
                model.Usuario = item.Usuario;
                model.Password = item.Password;
                model.AnoRegistro = Convert.ToDateTime(item.AnoRegistro);
                model.Activo = Convert.ToBoolean(item.Activo);
                model.Sucursal = item.Sucursal;
                lista.Add(model);
            }

            return View(lista);
        }

        public async Task<IActionResult> Details(string Id)
        {
            EmpleadoModel model = new EmpleadoModel();
            var id = new EmpleadoId();
            id.Id = Id;
            var data = await _client.ObtenerAsync(id);
            model.Id = Convert.ToInt32(data.Id);
            model.Nombres = data.Nombres;
            model.Apellidos = data.Apellidos;
            model.Usuario = data.Usuario;
            model.Password = data.Password;
            model.AnoRegistro = Convert.ToDateTime(data.AnoRegistro);
            model.Activo = Convert.ToBoolean(data.Activo);
            model.Sucursal = data.Sucursal;
            return View(model);
        }

        public IActionResult Create() //metodo no necesita ser asincrono debido a que solo llama una vista y no hace uso de algun metodo asincrono
        {
            return View(new EmpleadoModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpleadoModel modelRequest)
        {
            var model = new Empleado();
            model.Id = "0";
            model.Nombres = modelRequest.Nombres;
            model.Apellidos = modelRequest.Apellidos;
            model.Usuario = modelRequest.Usuario;
            model.Password = modelRequest.Password;
            model.AnoRegistro = modelRequest.AnoRegistro.ToString();
            model.Activo = "true";//Empleado siempre se registra como activo
            model.Sucursal = modelRequest.Sucursal;

            var dato = await _client.CrearAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View();
        }
    }
}
