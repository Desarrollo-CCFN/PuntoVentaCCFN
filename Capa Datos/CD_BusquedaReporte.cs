using Capa_Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_BusquedaReporte
    {
        private readonly CD_Conexion? conn = new CD_Conexion();
        private CE_BusquedaReporte ce = new CE_BusquedaReporte();

        public CE_BusquedaReporte Consulta()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_C_BusquedaReporte", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];

            ce.IdTr_ = Convert.ToInt32(row[0]);
            ce.opc_ = Convert.ToInt32(row[1]);

            return ce;

        }
 

     }
}
