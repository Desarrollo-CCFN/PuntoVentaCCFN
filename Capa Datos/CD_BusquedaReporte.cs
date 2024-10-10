using Capa_Entidad;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
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

        public CE_BusquedaReporte Consulta(int IdTra, int Param)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_C_BusquedaReporte", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Pasar parámetros al procedimiento almacenado
            da.SelectCommand.Parameters.AddWithValue("_IdTran", IdTra);
            da.SelectCommand.Parameters.AddWithValue("_Opc", Param);


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                ce.IdTr_ = Convert.ToInt32(row[0]);
                ce.opc_ = Convert.ToInt32(row[1]);
            }
             
            return ce;

        }
 

     }
}
