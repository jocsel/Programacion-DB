using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ClaseSistema;
using System.Data;

namespace AccesoDatos
{
    public class UsuarioDAL
    {
        SqlConnection conex = new SqlConnection(Properties.Settings.Default.cnnString);
        SqlCommand cmd;

        public string insertarRow(ClaseUsuario usuario)
        {

            string mensaje = "";
            cmd = new SqlCommand("Sistema..sp_usuario", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id_usuario", SqlDbType.Int).Value = usuario.id_usuario;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = usuario.usuario;
            cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar, 8000).Value = usuario.contrasena;
            cmd.Parameters.Add("@Id_rol", SqlDbType.Int).Value = usuario.id_rol;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.apellido;
            cmd.Parameters.Add("@i_operacion", SqlDbType.VarChar, 1).Value = "I";
            cmd.Parameters.Add("@o_msg", SqlDbType.VarChar, 254);
            cmd.Parameters["@o_msg"].Direction = ParameterDirection.Output;

            try
            {
                conex.Open();
                cmd.ExecuteNonQuery();
                conex.Close();
                mensaje = cmd.Parameters["@o_msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return mensaje;

        }

        public string UpdateRow(ClaseUsuario usuario)
        {
            string mensaje = "";
            cmd = new SqlCommand("Sistema..sp_usuario", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id_usuario", SqlDbType.Int).Value = usuario.id_usuario;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = usuario.usuario;
            cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar, 8000).Value = usuario.contrasena;
            cmd.Parameters.Add("@Id_rol", SqlDbType.Int).Value = usuario.id_rol;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.apellido;
            cmd.Parameters.Add("@i_operacion", SqlDbType.VarChar, 1).Value = "U";
            cmd.Parameters.Add("@o_msg", SqlDbType.VarChar, 254);
            cmd.Parameters["@o_msg"].Direction = ParameterDirection.Output;

            try
            {
                conex.Open();
                cmd.ExecuteNonQuery();
                conex.Close();
                mensaje = cmd.Parameters["@o_msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return mensaje;


        }
        public List<ClaseUsuario> selectRow()
        {
            List<ClaseUsuario> ListaUsua = new List<ClaseUsuario>();


            cmd = new SqlCommand("Sistema..sp_usuario", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@i_operacion", SqlDbType.VarChar, 1).Value = "S";
            cmd.Parameters.Add("@o_msg", SqlDbType.VarChar, 254);
            cmd.Parameters["@o_msg"].Direction = ParameterDirection.Output;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    ClaseUsuario usua = new ClaseUsuario();
                    usua.id_usuario = Convert.ToInt32(dr["Id_usuario"]);
                    usua.usuario = (dr["Usuario"]).ToString();
                    usua.contrasena = dr["Contraseña"].ToString();
                    usua.id_rol = Convert.ToInt32(dr["Id_rol"]);
                    usua.nombre = dr["nombre"].ToString();
                    usua.apellido = dr["apellido"].ToString();
                    usua.rol = dr["rol"].ToString();
                    ListaUsua.Add(usua);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;

            }
            return ListaUsua;

        }

    }
}