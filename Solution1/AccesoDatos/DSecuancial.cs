using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseSistema;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AccesoDatos
{
    public class DSecuancial
    {
        SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString);
        SqlCommand cmd;

        public List<ESecuencial> SelectRows()
        {
            List<ESecuencial> listaSecuancial = new List<ESecuencial>();
            cmd = new SqlCommand("Sistema..sp_secuencial", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@i_tabla", SqlDbType.VarChar, 50).Value = "";
            cmd.Parameters.Add("@o_secuencial", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@i_operacoin", SqlDbType.VarChar, 1).Value = "S";
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
                    ESecuencial secuencial = new ESecuencial();
                    secuencial.tabla = dr["tabla"].ToString();
                    secuencial.Secuancial = Convert.ToInt32(dr["Secuancial"]);
                    listaSecuancial.Add(secuencial);
                }
            }
            catch (Exception ex) {

                throw ex;
            }

            return listaSecuancial;
        }
        public string InsertRow(ESecuencial Isecuencial)
        {
            string msj = "";
            cmd = new SqlCommand("Sistema..sp_secuencial", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@i_tabla", SqlDbType.VarChar, 50).Value = Isecuencial.tabla;
            cmd.Parameters.Add("@i_operacoin", SqlDbType.VarChar, 1).Value = "I";
            cmd.Parameters.Add("@o_msg", SqlDbType.VarChar, 254);
            cmd.Parameters["@o_msg"].Direction = ParameterDirection.Output;
            try {
                conex.Open();
                cmd.ExecuteNonQuery();
                conex.Close();
                msj = cmd.Parameters["@o_msg"].Value.ToString();

            }
            catch ( Exception ex) {
                msj = ex.Message;
            }
            return msj;
        }


    }
}
