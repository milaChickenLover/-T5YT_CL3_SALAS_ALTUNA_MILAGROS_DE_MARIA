using Microsoft.Data.SqlClient;
using System.Data;

namespace Ecommerce.Persons.gRPC.Milagros_Salas.Datos
{
    public class EmpleadosDAO
    {
        #region singleton
        private static readonly EmpleadosDAO _instancia = new EmpleadosDAO();
        public static EmpleadosDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<Empleado> Listar()
        {
            SqlCommand cmd = null;
            List<Empleado> list = null;

            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarEmpleados", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                list = new List<Empleado>();
                while (dr.Read())
                {
                    Empleado model = new Empleado();
                    model.Id = Convert.ToString(dr["Id"]);
                    model.Nombres = Convert.ToString(dr["Nombres"]);
                    model.Apellidos = Convert.ToString(dr["Apellidos"]);
                    model.Usuario = Convert.ToString(dr["Usuario"]);
                    model.Password = Convert.ToString(dr["Password"]);
                    model.AnoRegistro = Convert.ToString(dr["AnoRegistro"]);
                    model.Activo = Convert.ToString(dr["Activo"]);
                    model.Sucursal = Convert.ToString(dr["Sucursal"]);
                    list.Add(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return list;
        }

        public Empleado Devolver(string id)
        {
            SqlCommand cmd = null;
            Empleado model = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spObtenerEmpleado", cn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    model = new Empleado();
                    model.Id = Convert.ToString(dr["Id"]);
                    model.Nombres = Convert.ToString(dr["Nombres"]);
                    model.Apellidos = Convert.ToString(dr["Apellidos"]);
                    model.Usuario = Convert.ToString(dr["Usuario"]);
                    model.Password = Convert.ToString(dr["Password"]);
                    model.AnoRegistro = Convert.ToString(dr["AnoRegistro"]);
                    model.Activo = Convert.ToString(dr["Activo"]);
                    model.Sucursal = Convert.ToString(dr["Sucursal"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return model;
        }

        public string Insertar(Empleado model)
        {
            SqlCommand cmd = null;
            string creado = "NoOk";

            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spRegistrarEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombres", model.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", model.Apellidos);
                cmd.Parameters.AddWithValue("@Usuario", model.Usuario);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@AnoRegistro", model.AnoRegistro);
                cmd.Parameters.AddWithValue("@Activo", model.Activo);
                cmd.Parameters.AddWithValue("@Sucursal", model.Sucursal);

                cn.Open();

                int nro = cmd.ExecuteNonQuery();
                if (nro > 0) creado = "Ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return creado;
        }

    }
}
