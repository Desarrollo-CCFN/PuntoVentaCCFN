using Capa_Entidad.Productos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.ReciboProducto
{
    public class CD_SolTraslado
    {

        private readonly CD_Conexion? conn = new CD_Conexion();

        #region CargarSolicitudTraslado
        public DataTable CargarSolTraslado(int NoTrs)
        {
            string sql = $@"SP_RP_CargarSolTraslado";
            MySqlCommand cmd = new MySqlCommand(sql, conn.AbrirConexion());
            cmd.Parameters.Add("@pTsr", MySqlDbType.Int32);

            cmd.Parameters["@pTsr"].Value = NoTrs; // 171390;

            // MySqlDataAdapter da = new MySqlDataAdapter(sql_select, conn.AbrirConexion());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            conn.CerrarConexion();

            return dt;
        }
        #endregion
    }
}
