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
    public class CD_FacturaCFDI
    {
        private readonly CD_Conexion conn = new CD_Conexion();

        public DataTable ConsultaDatosFactura(int DocEntry)
        {
            string sql = $@"SP_V_DatosXmlFactura";
            MySqlCommand cmd = new MySqlCommand(sql, conn.AbrirConexion());
            cmd.Parameters.Add("@_DocEntry", MySqlDbType.Int32);

            cmd.Parameters["@_DocEntry"].Value = DocEntry; 

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            conn.CerrarConexion();

            return dt;
        }
    }
}
