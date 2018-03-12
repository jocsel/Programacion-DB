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
    public class DTransaccion
    {
        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString);
        SqlCommand cmd;

        public List<ETransacciones> SelectRow() {
            List<ETransacciones> listaTransacciones = new List<ETransacciones>();
            cmd = new SqlCommand("Sistema..SP_TRANSACCIONES",conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID_TRANSACCIONES",SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@TRANSACCIONES",SqlDbType.VarChar,10).Value = "";
            cmd.Parameters.Add("@i_operacion",SqlDbType.Char,1).Value = "S";
            cmd.Parameters.Add("@o_msg",SqlDbType.VarChar,254);
            cmd.Parameters["@o_msg"].Direction = ParameterDirection.Output;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    ETransacciones tran = new ETransacciones();
                    tran.Transaccion = dr["Transaccion"].ToString();
                    listaTransacciones.Add(tran);
                }
            }
            catch(Exception ex) {
                throw ex;
            }
            return listaTransacciones;
        }

        public string InsertRow(ETransacciones ITransac)
        {
            string msj = "";
            cmd = new SqlCommand("Sistema..SP_TRANSACCIONES", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TRANSACCIONES", SqlDbType.VarChar, 10).Value = ITransac.Transaccion;
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

        public string ActualizarRow(ETransacciones Utran) {
            string msj = "";
            cmd = new SqlCommand("Sistema..SP_TRANSACCIONES", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TRANSACCIONES", SqlDbType.VarChar, 10).Value = Utran.Transaccion;
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
