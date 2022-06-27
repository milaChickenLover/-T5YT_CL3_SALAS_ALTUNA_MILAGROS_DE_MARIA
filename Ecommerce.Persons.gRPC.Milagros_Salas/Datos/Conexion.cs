using Microsoft.Data.SqlClient;

namespace Ecommerce.Persons.gRPC.Milagros_Salas.Datos
{
    public class Conexion
    {
        #region singleton
        private static readonly Conexion _instancia = new Conexion();
        public static Conexion Instancia { get { return _instancia; } }
        #endregion singleton

        public SqlConnection conectar()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Server=tcp:databasecibertec.database.windows.net,1433;Initial Catalog=CL3_T5YT_Salas_Altuna_Milagros;Persist Security Info=False;User ID=admincl3;Password=Alsatia1516;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return cn;
        }
    }
}
