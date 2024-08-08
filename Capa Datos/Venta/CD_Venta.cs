using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Venta;
using Mysqlx.Cursor;

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

        #region ventaDetalle
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

        public CE_VentaHeader Venta(string sucursal, string caja)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_V_Venta", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("Sucursal", MySqlDbType.VarChar).Value=sucursal;
            da.SelectCommand.Parameters.Add("CodCaja", MySqlDbType.VarChar).Value = caja;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Id = Convert.ToInt32(row[0]);
            ce.NumTck = Convert.ToString(row[1]);

            return ce;

        }

        public void ventaDetalleLive(CE_VentaDetalle detalle)
        {
            MySqlCommand conm = new MySqlCommand("SP_V_VentaDetalle", conn.AbrirConexion());
            conm.CommandType = CommandType.StoredProcedure;
            conm.Parameters.Add("NumTck", MySqlDbType.VarChar).Value = detalle.NumTck;
            conm.Parameters.Add("ItemCode", MySqlDbType.VarChar).Value = detalle.ItemCode;
            conm.Parameters.Add("Cantidad", MySqlDbType.Decimal).Value = detalle.Cantidad;
            conm.Parameters.Add("Monto", MySqlDbType.Decimal).Value = detalle.Monto;
            conm.Parameters.Add("WhsCode", MySqlDbType.VarChar).Value = detalle.WhsCode;
            conm.Parameters.Add("Moneda", MySqlDbType.VarChar).Value = detalle.Currency;
            conm.Parameters.Add("CodeBars", MySqlDbType.VarChar).Value = detalle.CodeBars;
            conm.Parameters.Add("PriceList", MySqlDbType.Int32).Value = detalle.PriceList;
            conm.Parameters.Add("IdHeader", MySqlDbType.Int32).Value = detalle.IdHeader;
            conm.ExecuteNonQuery();
            conm.Parameters.Clear();
            conn.CerrarConexion();

        }

        public void ventaPagos(CE_VentaPagos ventaPago)
        {
            MySqlCommand conm = new MySqlCommand("SP_V_VentaPagos", conn.AbrirConexion());
            conm.CommandType = CommandType.StoredProcedure;
            conm.Parameters.Add("Payform", MySqlDbType.VarChar).Value = ventaPago.Payform;
            conm.Parameters.Add("Currency", MySqlDbType.VarChar).Value = ventaPago.Currency;
            conm.Parameters.Add("Rate", MySqlDbType.Decimal).Value = ventaPago.Rate;
            conm.Parameters.Add("AmountPay", MySqlDbType.Decimal).Value = ventaPago.AmountPay;
            conm.Parameters.Add("BalAmount", MySqlDbType.Decimal).Value = ventaPago.BalAmout;
            conm.Parameters.Add("IdHeader", MySqlDbType.Int32).Value = ventaPago.IdHeader;
            conm.ExecuteNonQuery();
            conm.Parameters.Clear();
            conn.CerrarConexion();
        }
    }
}
