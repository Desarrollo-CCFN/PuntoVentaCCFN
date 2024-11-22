using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Devoluciones;
using System.Windows;
using System.Windows.Input;

namespace Capa_Datos
{
    public class CD_Devoluciones
    {
        private readonly CD_Conexion conn = new CD_Conexion();
        private readonly CE_DevolucionHeader ce = new CE_DevolucionHeader();

        public CE_DevolucionHeader CargarHeader(string NumTicket)
        {
            string _sStoredName = "SP_V_DevoHeader";

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("NumTck_", MySqlDbType.VarChar).Value = NumTicket;
                da.SelectCommand.ExecuteNonQuery();
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                if (Convert.ToInt32(row[0]) == 0)
                { 
                    return null;
                }

                ce.Id = Convert.ToInt32(row[2]);
                ce.NumTck = Convert.ToString(row[3]);
                ce.Fecha = Convert.ToString(row[4]);
                ce.DocCur = Convert.ToString(row[5]);
                ce.DocRate = Convert.ToDecimal(row[6]);
                ce.DocTotal = Convert.ToDecimal(row[7]);
                ce.DocTotalFC = Convert.ToDecimal(row[8]);
                ce.Status = Convert.ToString(row[9]);

                return ce;



            } catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable CargarDetalle(string NumTicket)
        {
            string _sStoredName = "SP_V_DevoDetalle";

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("NumTck_", MySqlDbType.VarChar).Value = NumTicket;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                conn.CerrarConexion();

                return dt;
            } catch(Exception ex)
            {
                return null;
            }
        }

        public bool DevolucionHeader(int _idHeader, int _UserId, ref string sMensaje)
        {
            string _sStoredName = "";

            try
            {
                _sStoredName = "SP_V_DevolCierre";

                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_IdHeader", MySqlDbType.Int32).Value = _idHeader;
                da.SelectCommand.Parameters.Add("UserId_", MySqlDbType.Int32).Value = _UserId;

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
                    return false;
                }

                da.SelectCommand.Parameters.Clear();

                return true;
            } catch (Exception ex) { 
                sMensaje = "Excepcion tipo " + ex.GetType() + " " + ex.Message +
                               " ERROR mientras se ejecutaba la transacción [" + _sStoredName + "].";
                return false;
            }
        }
    }
}
