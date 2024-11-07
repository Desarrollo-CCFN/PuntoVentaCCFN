using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Productos;
using System.Diagnostics;
namespace Capa_Datos.Productos
{
    public class CD_Productos
    {
        private readonly CD_Conexion? conn = new CD_Conexion();
        private readonly CE_Productos ce = new CE_Productos();

        #region CargarProductos
        public DataTable CargarProductos()
        {

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_P_CargarProductos", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                conn.CerrarConexion();

                return dt;
            } catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region busqueda producto x nombre
        public DataTable ConsultaProducto(string busqueda)
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_P_BusquedaProductoManual", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("busqueda", MySqlDbType.VarChar).Value = busqueda;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                conn.CerrarConexion();

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region busqueda de um de producto
        public List<CE_ProductUm> ConsultaUM(string itemCode)
        {

            try
            {
                List<CE_ProductUm> listUm = new List<CE_ProductUm>();
                MySqlCommand conm = new MySqlCommand("SP_P_BusquedaUOM", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("itemCode", MySqlDbType.VarChar).Value = itemCode;
                MySqlDataReader dr = conm.ExecuteReader();

                while (dr.Read())
                {
                    listUm.Add(new CE_ProductUm { UomEntry = dr["UomEntry"].ToString(), UomCode = dr["UomCode"].ToString() });
                }
                conn.CerrarConexion();

                return listUm;
            } catch(Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region actualizar cantidad
        public bool AnulacionProducto(int idHeader, int LineNum, decimal Cantidad, ref string sMensaje)
        {
            string pName = "";
            try
            {
                pName = "SP_P_ActualizarCantidad";
                MySqlCommand conm = new MySqlCommand("SP_P_ActualizarCantidad", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("_Idheader", MySqlDbType.Int32).Value = idHeader;
                conm.Parameters.Add("_LineNum", MySqlDbType.Int32).Value = LineNum;
                conm.Parameters.Add("_Quantity", MySqlDbType.Decimal).Value = Cantidad;

                MySqlParameter outErrorCode = new MySqlParameter("@ErrorCode_", MySqlDbType.Int32);
                outErrorCode.Direction = ParameterDirection.Output;
                conm.Parameters.Add(outErrorCode);

                MySqlParameter outErrorMessage = new MySqlParameter("@ErrorMessage_", MySqlDbType.VarChar);
                outErrorMessage.Direction = ParameterDirection.Output;
                conm.Parameters.Add(outErrorMessage);

                conm.ExecuteNonQuery();

                if (outErrorCode.Value.ToString() != "0")
                {
                    sMensaje = outErrorMessage.Value.ToString();
                    return false;
                }

                conm.Parameters.Clear();
                conn.CerrarConexion();
            }
            catch (Exception ex1)
            {
                //sMensaje = "Excepcion tipo " + ex1.GetType() + " " + ex1.Message +
                //               " ERROR mientras se ejecutaba la transacción [" + sItemCode + "].";
                sMensaje = ex1.Message;
                return false;
            }
            return true;
        }
        #endregion

        #region anular producto
        public bool AnularProducto(int idHeader, int LineNum)
        {
            string pName = "";
            try
            {
                pName = "SP_P_Anular";
                MySqlCommand conm = new MySqlCommand("SP_P_Anular", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("_Idheader", MySqlDbType.Int32).Value = idHeader;
                conm.Parameters.Add("_LineNum", MySqlDbType.Int32).Value = LineNum;
                conm.ExecuteNonQuery();
                conm.Parameters.Clear();
                conn.CerrarConexion();
            }
            catch (Exception ex1)
            {
                //sMensaje = "Excepcion tipo " + ex1.GetType() + " " + ex1.Message +
                //               " ERROR mientras se ejecutaba la transacción [" + sItemCode + "].";
                string mensaje = ex1.Message;
                string Mensaje = mensaje.Replace(" ", "_");
                int Param = 1;

                // Crear una instancia de ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"C:\PuntoVenta\Errores\WindowsError.exe"; // Ruta completa al ejecutable de RetirosCaja
                                                                                

                // startInfo.Arguments = $"{IdTra}";
                startInfo.Arguments = $"{Mensaje} {Param}";
                Process.Start(startInfo);
              
                return false;
            }
            return true;
        }
        #endregion
    }
}
