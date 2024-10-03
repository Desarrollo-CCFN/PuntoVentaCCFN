using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_TipoCambio
    {

        private readonly CD_Conexion conn = new CD_Conexion();
        private CE_TipoCambio ce = new CE_TipoCambio();

        public CE_TipoCambio Consulta()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_TC_ConsultaTC", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt;
                dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                ce.Rate = Convert.ToDecimal(row[0]);
                return ce;
            } catch (Exception ex) { return null; }


        }
    }
}
