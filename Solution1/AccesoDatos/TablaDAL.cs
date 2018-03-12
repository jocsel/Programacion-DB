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
    class TablaDAL
    {

         SqlConnection conex = new SqlConnection(Properties.Settings.Default.cnnString);
        SqlCommand cmd;
        public string insertarRow(ClaseTabla tab)
        {

            string mensaje = "";
            cmd = new SqlCommand("Sistema..sp_tabla", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id_tabla", SqlDbType.Int).Value = tab.Id_tabla;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 254).Value = tab.Descripcion;
            cmd.Parameters.Add("@Tabla", SqlDbType.VarChar, 64).Value = tab.Tabla;
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

        public string updateRow(ClaseTabla tab)
        {

            string mensaje = "";
            cmd = new SqlCommand("Sistema..sp_tabla", conex);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id_tabla", SqlDbType.Int).Value = tab.Id_tabla;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 254).Value = tab.Descripcion;
            cmd.Parameters.Add("@Tabla", SqlDbType.VarChar, 64).Value = tab.Tabla;
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

        public List<ClaseTabla> selectRow()
        {
            List<ClaseTabla> listatabla = new List<ClaseTabla>();


            cmd = new SqlCommand("Sistema..sp_tabla", conex);
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
                    ClaseTabla tab = new ClaseTabla();


                    tab.Id_tabla = Convert.ToInt32(dr["Id_tabla"]);
                    tab.Descripcion = (dr["Descripcion"]).ToString();
                    tab.Tabla = dr["Tabla"].ToString();
                    
           
                  
                    listatabla.Add(tab);
                }
            }
            catch (Exception ex)
            {
              string   mensaje = ex.Message;

            }
            return listatabla;

        }

        }


    }
}
