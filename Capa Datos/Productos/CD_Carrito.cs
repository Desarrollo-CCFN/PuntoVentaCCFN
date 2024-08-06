using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad;

namespace Capa_Datos.Productos
{
    public class CD_Carrito
    {



        private readonly CD_Conexion? conn = new CD_Conexion();


        #region buscarArticulo
        public DataTable Buscar(string BcdCode, int listNum)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_P_ProductoVenta", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("p_Barcode", MySqlDbType.VarChar).Value = BcdCode;
            da.SelectCommand.Parameters.Add("p_ListaP", MySqlDbType.Int32).Value = listNum;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            dt.Columns.Add("Cantidad", typeof(int));

            foreach (DataRow row in dt.Rows)
            {
                row["Cantidad"] = 1;
            }

            conn.CerrarConexion();
            return dt;
        }
        #endregion
    }
}
