using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClaseSistema;
using System.Data.SqlClient;


namespace AccesoDatos
{
   public class RolesDAL
    {
        SqlConnection conex = new SqlConnection(Properties.Settings.Default.cnnString);
        SqlCommand cmd;
        public string insertarRow(ClaseRoles rol)
        {

            string mensaje = "";
            cmd = new SqlCommand("Sistema..sp_rol", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id_rol", SqlDbType.Int).Value = rol.id_rol;
            cmd.Parameters.Add("@rol", SqlDbType.VarChar, 50).Value = rol.rol;
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

        public string updateRow(ClaseRoles rol)
        {

            string mensaje = "";
            cmd = new SqlCommand("Sistema..sp_rol", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id_rol", SqlDbType.Int).Value = rol.id_rol;
            cmd.Parameters.Add("@rol", SqlDbType.VarChar, 50).Value = rol.rol;
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

        public List<ClaseRoles> selectrow()
        {
            List<ClaseRoles> Listarol = new List<ClaseRoles>();

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
                    ClaseRoles rol = new ClaseRoles();
                    rol.id_rol = Convert.ToInt32(dr["Id_rol"]);
                    rol.rol = (dr["rol"]).ToString();

                    Listarol.Add(rol);

                }

            }
            catch (Exception ex)
            {

                string mensaje = ex.Message;
            }
            return Listarol;
        }


    }
}