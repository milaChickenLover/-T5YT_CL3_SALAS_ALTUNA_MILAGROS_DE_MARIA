using Ecommerce.Persons.gRPC.Milagros_Salas;
using Grpc.Core;

namespace Ecommerce.Persons.gRPC.Milagros_Salas.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + string.Concat(request.Nombre, ";", request.Apellido) //
            });
        }
    }
}