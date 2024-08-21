using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad;

namespace Capa_Datos.Productos
{
    public class CD_Carrito
    {



        private readonly CD_Conexion? conn = new CD_Conexion();


        #region buscarArticulo
        public DataTable Buscar(string BcdCode, int listNum, string currency, ref string sMensaje)
        {
            string sItemCode = "";
            DataTable dt = new DataTable();

            try
            {
                sItemCode = "SP_P_ProductoVenta";

                MySqlDataAdapter da = new MySqlDataAdapter("SP_P_ProductoVenta", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("p_Barcode", MySqlDbType.VarChar).Value = BcdCode;
                da.SelectCommand.Parameters.Add("p_ListaP", MySqlDbType.Int32).Value = listNum;
                da.SelectCommand.Parameters.Add("p_MonVenta", MySqlDbType.VarChar).Value = currency;

                MySqlParameter outErrorCode = new MySqlParameter("ErrorCode_", MySqlDbType.Int32);
                outErrorCode.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorCode);

                MySqlParameter outErrorMessage = new MySqlParameter("ErrorMessage_", MySqlDbType.Text);
                outErrorMessage.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorMessage);

                da.SelectCommand.ExecuteNonQuery();

                if (outErrorCode.Value.ToString() != "0")
                {
                    sMensaje = outErrorMessage.Value.ToString();
                    return null;
                }

                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                dt = ds.Tables[0];

                dt.Columns.Add("Cantidad", typeof(int));

                foreach (DataRow row in dt.Rows)
                {
                    row["Cantidad"] = 1;
                }

                conn.CerrarConexion();
                return dt;
            }
            catch (Exception ex1)
            {
                sMensaje = "Excepcion tipo " + ex1.GetType() + " " + ex1.Message +
                               " ERROR mientras se ejecutaba la transacción [" + sItemCode + "].";
                return null;
            }
            
        }
        #endregion
    }
}
