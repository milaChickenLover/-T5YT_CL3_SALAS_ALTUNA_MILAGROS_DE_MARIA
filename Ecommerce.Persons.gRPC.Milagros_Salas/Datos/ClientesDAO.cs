using Microsoft.Data.SqlClient;
using System.Data;

namespace Ecommerce.Persons.gRPC.Milagros_Salas.Datos
{
    public class ClientesDAO
    {
        #region singleton
        private static readonly ClientesDAO _instancia = new ClientesDAO();
        public static ClientesDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<Cliente> Listar()
        {
            SqlCommand cmd = null;
            List<Cliente> list = null;

            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                list = new List<Cliente>();
                while (dr.Read())
                {
                    Cliente model = new Cliente();
                    model.Id = Convert.ToString(dr["Id"]);
                    model.Nombres = Convert.ToString(dr["Nombres"]);
                    model.Apellidos = Convert.ToString(dr["Apellidos"]);
                    model.Usuario = Convert.ToString(dr["Usuario"]);
                    model.Password = Convert.ToString(dr["Password"]);
                    model.FechaRegistro = Convert.ToString(dr["FechaRegistro"]);
                    model.Activo = Convert.ToString(dr["Activo"]);
                    model.Ciudad = Convert.ToString(dr["Ciudad"]);
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

        public Cliente Devolver(string id)
        {
            SqlCommand cmd = null;
            Cliente model = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spObtenerCliente", cn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    model = new Cliente();
                    model.Id = Convert.ToString(dr["Id"]);
                    model.Nombres = Convert.ToString(dr["Nombres"]);
                    model.Apellidos = Convert.ToString(dr["Apellidos"]);
                    model.Usuario = Convert.ToString(dr["Usuario"]);
                    model.Password = Convert.ToString(dr["Password"]);
                    model.FechaRegistro = Convert.ToString(dr["FechaRegistro"]);
                    model.Activo = Convert.ToString(dr["Activo"]);
                    model.Ciudad = Convert.ToString(dr["Ciudad"]);
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

        public string Insertar(Cliente model)
        {
            SqlCommand cmd = null;
            string creado = "NoOk";

            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spRegistrarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombres", model.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", model.Apellidos);
                cmd.Parameters.AddWithValue("@Usuario", model.Usuario);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@FechaRegistro", model.FechaRegistro);
                cmd.Parameters.AddWithValue("@Activo", model.Activo);
                cmd.Parameters.AddWithValue("@Ciudad", model.Ciudad);

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
