using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad;
using System.Diagnostics;
using Capa_Entidad.OperacionesCaja;

namespace Capa_Datos.OperacionesCaja
{
    public class CD_VentaCaja
    {
        private readonly CD_Conexion conn = new CD_Conexion();
        private CE_VentaCaja objventaCaja = new CE_VentaCaja();

        public CE_VentaCaja InsertarVentaCaja(CE_VentaCaja obj)
        {

            string sItemCode = "";

            try
            {
                sItemCode = "SP_V_OperacionCaja";

                MySqlDataAdapter da = new MySqlDataAdapter(sItemCode, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("User", MySqlDbType.VarChar).Value = obj.User;
                da.SelectCommand.Parameters.Add("WhsCode", MySqlDbType.VarChar).Value = obj.WhsCode;
                da.SelectCommand.Parameters.Add("BegAmount", MySqlDbType.VarChar).Value = obj.BegAmount;
                da.SelectCommand.Parameters.Add("BegAmountFC", MySqlDbType.VarChar).Value = obj.BegAmountFC;
                da.SelectCommand.Parameters.Add("Cashier", MySqlDbType.VarChar).Value = obj.Cashier;
                da.SelectCommand.Parameters.Add("StationId", MySqlDbType.VarChar).Value = obj.StationId;

                MySqlParameter outIdCash = new MySqlParameter("IdCASH", MySqlDbType.Int32);
                outIdCash.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outIdCash);

                da.SelectCommand.ExecuteNonQuery();

                da.SelectCommand.Parameters.Clear();

                objventaCaja.IdCash = Convert.ToInt32(outIdCash.Value);

                return objventaCaja;

            }
            catch
            {
                objventaCaja.IdCash = -1;
                return objventaCaja;
            }
        }

        public int insertarCajaDenom(CE_Denominacion obj)
        {
            string sItemCode = "";
            int Out = 0;

            try
            {
                sItemCode = "SP_V_OperacionCajaDenom";
                MySqlDataAdapter da = new MySqlDataAdapter(sItemCode, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("IdCash", MySqlDbType.Int32).Value = obj.IdCash;
                da.SelectCommand.Parameters.Add("PayForm", MySqlDbType.VarChar).Value = obj.PayForm;
                da.SelectCommand.Parameters.Add("IdCDenom", MySqlDbType.Int32).Value = obj.IdCDenom;
                da.SelectCommand.Parameters.Add("Quantity", MySqlDbType.Int32).Value = obj.Quantity;
                da.SelectCommand.Parameters.Add("TotAmount", MySqlDbType.Decimal).Value = obj.TotAmount;
                da.SelectCommand.Parameters.Add("User", MySqlDbType.Int32).Value = obj.Usuario;
                da.SelectCommand.Parameters.Add("IdTran", MySqlDbType.Int32).Value = obj.Idtrans;

                // da.SelectCommand.ExecuteNonQuery();
                // da.SelectCommand.Parameters.Clear();

                MySqlParameter outIdCash = new MySqlParameter("_Idtrans", MySqlDbType.Int32);
                outIdCash.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outIdCash);

                da.SelectCommand.ExecuteNonQuery();

                da.SelectCommand.Parameters.Clear();

                Out = Convert.ToInt32(outIdCash.Value);

              //  return objventaCaja;
 

            }
            catch { }



            return Out;

        }

        public int isertarMovCaja(CE_MovCaja obj)
        {
            string sItemCode = "";
            int Out = 0;

            try
            {
                sItemCode = "SP_V_OperacionMovCaja";
                MySqlDataAdapter da = new MySqlDataAdapter(sItemCode, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("IdCash", MySqlDbType.Int32).Value = obj.IdCash;
                da.SelectCommand.Parameters.Add("Type", MySqlDbType.VarChar).Value = obj.Type;
                da.SelectCommand.Parameters.Add("Currency", MySqlDbType.VarChar).Value = obj.Currency;
                da.SelectCommand.Parameters.Add("Amount", MySqlDbType.Decimal).Value = obj.Amount;
                da.SelectCommand.Parameters.Add("Comments", MySqlDbType.VarChar).Value = obj.Comments;
                da.SelectCommand.Parameters.Add("User", MySqlDbType.VarChar).Value = obj.User;
                da.SelectCommand.Parameters.Add("Supervisor", MySqlDbType.VarChar).Value = obj.Supervisor;
                da.SelectCommand.Parameters.Add("Sucursal", MySqlDbType.VarChar).Value = obj.Sucursal;
                  da.SelectCommand.Parameters.Add("IdTran", MySqlDbType.Int32).Value = obj.Idtrans;

                //    da.SelectCommand.ExecuteNonQuery();
                //    da.SelectCommand.Parameters.Clear();

               MySqlParameter outIdCash = new MySqlParameter("_out", MySqlDbType.Int32);
            //    MySqlParameter outIdCash = new MySqlParameter("_out", MySqlDbType.VarChar);
                outIdCash.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outIdCash);

                da.SelectCommand.ExecuteNonQuery();

                da.SelectCommand.Parameters.Clear();

                  Out = Convert.ToInt32(outIdCash.Value);

               // Out = Convert.ToString(outIdCash.Value);

               

            }

            catch { }

            return Out;

        }

        public CE_VentaCaja infoApertura(string User, string whsCode, int stationId)
        {
            string sItemCode = "";

            try
            {
                sItemCode = "SP_V_InfoCaja";
                MySqlDataAdapter da = new MySqlDataAdapter(sItemCode, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("usuario", MySqlDbType.VarChar).Value = User;
                da.SelectCommand.Parameters.Add("_WhsCode", MySqlDbType.VarChar).Value = whsCode;
                da.SelectCommand.Parameters.Add("_StationId", MySqlDbType.Int32).Value = stationId;
                DataSet ds = new DataSet();

                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                CE_VentaCaja objv = new CE_VentaCaja
                {
                    IdCash = Convert.ToInt32(row[0]),
                    BegAmount = Convert.ToDecimal(row[1]),
                    BegAmountFC = Convert.ToDecimal(row[2]),
                };

                da.SelectCommand.ExecuteNonQuery();
                da.SelectCommand.Parameters.Clear();

                return objv;
            }
            catch
            {
                return null;
            }
        }

        public CE_MovCaja validateRetiro(int idCash)
        {
            string sItemCode = "";
            CE_MovCaja objm = new CE_MovCaja();

            try
            {
                sItemCode = "SP_V_ValidateRetiro";
                MySqlDataAdapter da = new MySqlDataAdapter(sItemCode, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("val", MySqlDbType.Int32).Value = idCash;

                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    objm.IdCash = -1;
                    return objm;
                }
                else
                {
                    DataRow row = dt.Rows[0];
                    objm.IdCash = Convert.ToInt32(row[0]);
                }

                return objm;

            }
            catch
            {
                objm.IdCash = -2;
                return objm;
            }
        }
    }
}
