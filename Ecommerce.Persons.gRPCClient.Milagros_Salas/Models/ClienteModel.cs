namespace Ecommerce.Persons.gRPCClient.Milagros_Salas.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public string Ciudad { get; set; }

    }
}