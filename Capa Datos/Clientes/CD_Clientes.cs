using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Clientes;

namespace Capa_Datos.Clientes
{
    public class CD_Clientes
    {
        private readonly CD_Conexion? conn = new CD_Conexion();
        private readonly CE_Clientes ce = new CE_Clientes();

        public CE_Clientes Consulta(string CardCode)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_C_Consultar", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("cardCode", MySqlDbType.VarChar).Value = CardCode;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.CardCode = Convert.ToString(row[0]);
            ce.CardName = Convert.ToString(row[1]);

            return ce;
        }
    }
}
