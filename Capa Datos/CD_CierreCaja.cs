﻿using Capa_Entidad;
using MySql.Data.MySqlClient;
using System.Data;

namespace Capa_Datos
{
    public class CD_CierreCaja
    {
        private readonly CD_Conexion? conn = new CD_Conexion();
        private CE_CierreCaja ce = new CE_CierreCaja();

        #region ConsultaHeader
        public DataTable Consulta(int idCash)
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_V_CierreCajaInfoHeader", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_IdCash", MySqlDbType.Int32).Value = idCash;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt;
                dt = ds.Tables[0];
                //DataRow row = dt.Rows[0];

                //ce.IdCash = Convert.ToInt32(row[0]);
                //ce.WhsCode = Convert.ToString(row[1]);
                //ce.BegAmount = Convert.ToDecimal(row[3]);
                //ce.EndAmount = Convert.ToDecimal(row[4]);
                //ce.WdalAmount = Convert.ToDecimal(row[5]);
                //ce.BegAmountFC = Convert.ToDecimal(row[6]);
                //ce.EndAmountFC = Convert.ToDecimal(row[7]);
                //ce.WdalAmountFC = Convert.ToDecimal(row[8]);
                //ce.BegAmountT = Convert.ToDecimal(row[9]);
                //ce.EndAmountT = Convert.ToDecimal(row[10]);
                //ce.WdalAmountT = Convert.ToDecimal(row[11]);
                //ce.PayAmount = Convert.ToDecimal(row[12]);
                //ce.PayAmountFC = Convert.ToDecimal(row[13]);

                return dt;
            } catch {
                return null;
            }

        }
        #endregion

        #region venta caja detalle
        public DataTable ConsultaDetalle(int idCash)
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_V_QryCierreCaja01", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_IdCash", MySqlDbType.Int32).Value = idCash;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt;
                dt = ds.Tables[0];
                //DataRow row = dt.Rows[0];

                //ce.IdCash = Convert.ToInt32(row[0]);
                //ce.WhsCode = Convert.ToString(row[1]);
                //ce.BegAmount = Convert.ToDecimal(row[3]);
                //ce.EndAmount = Convert.ToDecimal(row[4]);
                //ce.WdalAmount = Convert.ToDecimal(row[5]);
                //ce.BegAmountFC = Convert.ToDecimal(row[6]);
                //ce.EndAmountFC = Convert.ToDecimal(row[7]);
                //ce.WdalAmountFC = Convert.ToDecimal(row[8]);
                //ce.BegAmountT = Convert.ToDecimal(row[9]);
                //ce.EndAmountT = Convert.ToDecimal(row[10]);
                //ce.WdalAmountT = Convert.ToDecimal(row[11]);
                //ce.PayAmount = Convert.ToDecimal(row[12]);
                //ce.PayAmountFC = Convert.ToDecimal(row[13]);

                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}