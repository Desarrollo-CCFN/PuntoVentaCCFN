using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Venta;
using System.Windows;
using Mysqlx.Cursor;
using System.ServiceModel.Channels;

namespace Capa_Datos.Venta
{
    public class CD_Venta
    {
        private readonly CD_Conexion conn = new CD_Conexion();

        private readonly CE_VentaHeader ce = new CE_VentaHeader();
        private readonly CE_VentaPagos ceP = new CE_VentaPagos();

        #region ventaHeader
        public void ventaHeader(string numTck, DateTime fecha, decimal total, string usuario)
        {
            MySqlCommand conm = new MySqlCommand("SP_V_Venta", conn.AbrirConexion());
            conm.CommandType = CommandType.StoredProcedure;
            conm.Parameters.Add("NumTck", MySqlDbType.VarChar).Value = numTck;
            conm.Parameters.Add("Fecha", MySqlDbType.DateTime).Value = fecha;
            conm.Parameters.Add("Total", MySqlDbType.Decimal).Value = total;
            conm.Parameters.Add("Usuario", MySqlDbType.VarChar).Value = usuario;
            conm.ExecuteNonQuery();
            conm.Parameters.Clear();
            conn.CerrarConexion();
        }
        #endregion

        #region ventaDetalle(no es live)
        public void ventaDetalle(string numTck, string itemCode, decimal cantidad, decimal monto)
        {
            MySqlCommand conm = new MySqlCommand("SP_V_VentaDetalle", conn.AbrirConexion());
            conm.CommandType = CommandType.StoredProcedure;
            conm.Parameters.Add("NumTck", MySqlDbType.VarChar).Value = numTck;
            conm.Parameters.Add("ItemCode", MySqlDbType.VarChar).Value = itemCode;
            conm.Parameters.Add("Cantidad", MySqlDbType.Decimal).Value = cantidad;
            conm.Parameters.Add("Monto", MySqlDbType.Decimal).Value = monto;
            conm.ExecuteNonQuery();
            conm.Parameters.Clear();
            conn.CerrarConexion();
        }
        #endregion

        public CE_VentaHeader Venta(string sucursal, string caja, string cardCode, int idCash, int idStation)
        {
            string sItemCode = "";

            try
            {
                sItemCode = "SP_V_Venta";

                MySqlDataAdapter da = new MySqlDataAdapter("SP_V_Venta", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("Sucursal", MySqlDbType.VarChar).Value = sucursal;
                da.SelectCommand.Parameters.Add("CodCaja", MySqlDbType.VarChar).Value = caja;
                da.SelectCommand.Parameters.Add("CardCode_", MySqlDbType.VarChar).Value = cardCode;
                da.SelectCommand.Parameters.Add("IdCash_", MySqlDbType.Int32).Value = idCash;
                da.SelectCommand.Parameters.Add("stationId_", MySqlDbType.Int32).Value = idStation;

                MySqlParameter outErrorCode = new MySqlParameter("ErrorCode_", MySqlDbType.Int32);
                outErrorCode.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorCode);

                MySqlParameter outErrorMessage = new MySqlParameter("ErrorMessage_", MySqlDbType.Text);
                outErrorMessage.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorMessage);

                MySqlParameter outIdHeader = new MySqlParameter("IdHeader_", MySqlDbType.Int32);
                outIdHeader.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outIdHeader);

                MySqlParameter outNumTck = new MySqlParameter("NumTck_", MySqlDbType.VarChar);
                outNumTck.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outNumTck);

                da.SelectCommand.ExecuteNonQuery();

                if (outErrorCode.Value.ToString() != "0")
                {
                    ce.Id = -1;
                    ce.Comments = outErrorMessage.Value.ToString();
                    return ce;
                }

                da.SelectCommand.Parameters.Clear();

                ce.Id = Convert.ToInt32(outIdHeader.Value);
                ce.NumTck = outNumTck.Value.ToString();

                return ce;
            }
            catch (Exception ex1)
            {
                ce.Id = -1;
                ce.Comments = "Excepcion tipo " + ex1.GetType() + " " + ex1.Message +
                               " ERROR mientras se ejecutaba la transacción [" + sItemCode + "].";

                return ce;
            }
           

        }

        public bool ventaFinal(CE_VentaHeader ventaFinal, int _userId, ref string sMensaje)
        {
            string sItemCode = "";
            try
            {
                sItemCode = "SP_V_VentaCierre";
                MySqlCommand conm = new MySqlCommand("SP_V_VentaCierre", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("_IdHeader", MySqlDbType.Int32).Value = ventaFinal.Id;
                conm.Parameters.Add("UserId_", MySqlDbType.VarChar).Value = _userId;

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
                sMensaje = "Excepcion tipo " + ex1.GetType() + " " + ex1.Message +
                               " ERROR mientras se ejecutaba la transacción [" + sItemCode + "].";
                return false;
            }

            return true;

        }

        public bool ventaDetalleLive(CE_VentaDetalle detalle, ref string sMensaje)
        {

            string sItemCode = "";
            try
            {

                sItemCode = "SP_V_VentaDetalle";

                MySqlCommand conm = new MySqlCommand("SP_V_VentaDetalle", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("_ItemCode", MySqlDbType.VarChar).Value = detalle.ItemCode;
                conm.Parameters.Add("_Cantidad", MySqlDbType.Decimal).Value = detalle.Cantidad;
                conm.Parameters.Add("_Monto", MySqlDbType.Decimal).Value = detalle.Monto;
                conm.Parameters.Add("_WhsCode", MySqlDbType.VarChar).Value = detalle.WhsCode;
                conm.Parameters.Add("_Moneda", MySqlDbType.VarChar).Value = detalle.Currency;
                conm.Parameters.Add("_CodeBars", MySqlDbType.VarChar).Value = detalle.CodeBars;
                conm.Parameters.Add("_PriceNum", MySqlDbType.Decimal).Value = detalle.PriceList;
                conm.Parameters.Add("_IdHeader", MySqlDbType.Int32).Value = detalle.IdHeader;
                conm.Parameters.Add("_UomEntry", MySqlDbType.Int32).Value = detalle.UomEntry;
                conm.Parameters.Add("_LineNum", MySqlDbType.Int32).Value = detalle.LineNum;

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
                sMensaje = "Excepcion tipo " + ex1.GetType() + " " + ex1.Message +
                               " ERROR mientras se ejecutaba la transacción [" + sItemCode + "].";
                return false;
            }

            return true;


        }

        public bool AnularVenta(int idHeader)
        {
            string pName = "";
            try
            {
                pName = "SP_V_Anular";
                MySqlCommand conm = new MySqlCommand("SP_V_Anular", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("_IdHeader", MySqlDbType.Int32).Value = idHeader;
                conm.ExecuteNonQuery();
                conm.Parameters.Clear();
                conn.CerrarConexion();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public void ventaPagos(CE_VentaPagos ventaPago, ref string sMensaje)
        {
            try
            {
                MySqlCommand conm = new MySqlCommand("SP_V_VentaPagos", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("Payform", MySqlDbType.VarChar).Value = ventaPago.Payform;
                conm.Parameters.Add("Currency", MySqlDbType.VarChar).Value = ventaPago.Currency;
                conm.Parameters.Add("Rate", MySqlDbType.Decimal).Value = ventaPago.Rate;
                conm.Parameters.Add("AmountPay", MySqlDbType.Decimal).Value = ventaPago.AmountPay;
                conm.Parameters.Add("BalAmount", MySqlDbType.Decimal).Value = ventaPago.BalAmout;
                conm.Parameters.Add("IdHeader", MySqlDbType.Int32).Value = ventaPago.IdHeader;
                conm.Parameters.Add("Voucher", MySqlDbType.VarChar).Value = ventaPago.VoucherNum;

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
                }

                conm.Parameters.Clear();
                conn.CerrarConexion();


            }
            catch (Exception ex) 
            { 
               sMensaje = ex.Message;
            }
        }
            
            
        

        public bool AnularFPagos(int idHeader)
        {
            try
            {
                MySqlCommand conm = new MySqlCommand("SP_V_VentaPagosCancel", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("_IdHeader", MySqlDbType.Int32).Value = idHeader;
                conm.ExecuteNonQuery();
                conm.Parameters.Clear();
                conn.CerrarConexion();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool CerrarCaja(int station, string whsCode)
        {
            try
            {
                MySqlCommand conm = new MySqlCommand("SP_V_CerrarCaja", conn.AbrirConexion());
                conm.CommandType = CommandType.StoredProcedure;
                conm.Parameters.Add("_StationId", MySqlDbType.Int32).Value = station;
                conm.Parameters.Add("_WhsCode", MySqlDbType.VarChar).Value = whsCode;
                conm.ExecuteNonQuery();
                conm.Parameters.Clear();
                conn.CerrarConexion();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public CE_VentaHeader VentaActiva(string WhsCode, int idStation)
        {
            string sItemCode = "";

            try
            {
                sItemCode = "SP_V_VentaActiva";

                MySqlDataAdapter da = new MySqlDataAdapter("SP_V_VentaActiva", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_WhsCode", MySqlDbType.VarChar).Value = WhsCode;
                da.SelectCommand.Parameters.Add("_IdStation", MySqlDbType.Int32).Value = idStation;
                da.SelectCommand.ExecuteNonQuery();

                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt;
                dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                ce.Id = Convert.ToInt32(row[0]);
                ce.IdCash = Convert.ToInt32(row[1]);

                da.SelectCommand.Parameters.Clear();


                return ce;
            }
            catch (Exception ex)
            {
                ce.Id = -1;
                return ce;
            }
        }

        public List<CE_VentaDetalle> GetVentaDetalles(int idHeader)
        {
            string sItemCode = "SP_V_ObtenerVentaDetalle";
            List<CE_VentaDetalle> ceLista = new List<CE_VentaDetalle> ();

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_V_ObtenerVentaDetalle", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_IdHeader", MySqlDbType.Int32).Value = idHeader;
                da.SelectCommand.ExecuteNonQuery();
                DataSet ds = new DataSet();

                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                foreach (DataRow row in dt.Rows)
                {
                    CE_VentaDetalle ce = new CE_VentaDetalle
                    {
                        ItemCode = Convert.ToString(row[0]),
                        Dscription = Convert.ToString(row[1]),
                        CodeBars = Convert.ToString(row[2]),
                        Price2 = Convert.ToDecimal(row[3]),
                        Unidad = Convert.ToString(row[4]),
                        UomEntry = Convert.ToInt32(row[5]),
                        LineTotal = Convert.ToDecimal(row[6]),
                        VatSumFrgn = Convert.ToDecimal(row[7]),
                        VatSum = Convert.ToDecimal(row[8]),
                        PriceList = Convert.ToDecimal(row[9]),
                        LineNum = Convert.ToInt32(row[10]),
                        Cantidad = Convert.ToDecimal(row[11])
                    };

                    ceLista.Add(ce);
                }
                return ceLista;

            } catch (Exception ex) {
                return ceLista;
            }
        }

        public decimal VentaActivaPagado(int idHeader)
        {
            string sItemCode = "SP_C_Pagados";
            decimal value = 0;
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_C_Pagados", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("IdHeaderIn", MySqlDbType.Int32).Value = idHeader;
                da.SelectCommand.ExecuteNonQuery();
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt;
                dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                value = Convert.ToInt32(row[0]);
                da.SelectCommand.Parameters.Clear();

                return value;
            } catch (Exception ex) {
                return value;
            }
        }
    }
}
