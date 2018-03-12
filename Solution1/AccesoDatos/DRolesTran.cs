using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseSistema;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AccesoDatos
{
    public class DRolesTran
    {
        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString);
        SqlCommand cmd;

        public List<ERolesTran> SelectRow()
        {
            List<ERolesTran> listaRolesTran = new List<ERolesTran>();
            cmd = new SqlCommand("Sistema..SP_ROLES_TRANS", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id_tran", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@id_rol", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@i_operacion", SqlDbType.Char, 1).Value = "S";
            cmd.Parameters.Add("@o_msg", SqlDbType.VarChar, 254);
            cmd.Parameters["@o_msg"].Direction = ParameterDirection.Output;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();

            try {
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    ERolesTran rolestran = new ERolesTran();
                    rolestran.Id_Trans = Convert.ToInt32(dr["Id_tran"]);
                    rolestran.Id_Rol = Convert.ToInt32(dr["Id_rol"]);
                    listaRolesTran.Add(rolestran);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return listaRolesTran;
        }

        public string InsertarRow(ERolesTran IRT) {
            string msj = "";
            cmd = new SqlCommand("Sistema..SP_ROLES_TRANS", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@id_rol", SqlDbType.Int).Value = IRT.Id_Rol;
            cmd.Parameters.Add("@i_operacion", SqlDbType.Char, 1).Value = "I";
            cmd.Parameters.Add("@o_msg", SqlDbType.VarChar, 254);
            cmd.Parameters["@o_msg"].Direction = ParameterDirection.Output;
            try {
                conex.Open();
                cmd.ExecuteNonQuery();
                conex.Close();
                msj = cmd.Parameters["@o_msg"].Value.ToString();
            }
            catch (Exception ex) {
                msj = ex.Message;
            }
            return msj;
        }
        public string ActualizarRow(ERolesTran URT) {
            string msj = "";
            cmd = new SqlCommand("Sistema..SP_ROLES_TRANS", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@id_rol", SqlDbType.Int).Value = URT.Id_Rol;
            cmd.Parameters.Add("@i_operacion", SqlDbType.Char, 1).Value = "U";
            cmd.Parameters.Add("@o_msg", SqlDbType.VarChar, 254);
            cmd.Parameters["@o_msg"].Direction = ParameterDirection.Output;
            try
            {
                conex.Open();
                cmd.ExecuteNonQuery();
                conex.Close();
                msj = cmd.Parameters["@o_msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                msj = ex.Message;
            }
            return msj;

        }
    }
}
