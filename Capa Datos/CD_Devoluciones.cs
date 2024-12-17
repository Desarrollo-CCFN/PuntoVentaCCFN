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
        // private readonly CE_DevolucionHeader

        public CE_DevolucionHeader CargarHeader(string NumTicket, ref string smensaje)
        {
            string _sStoredName = "SP_V_DevoHeader";

            try
            {

                /* OUT ErrorCode_ int,
    OUT ErrorMessage_ TEXT*/

                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("NumTck_", MySqlDbType.VarChar).Value = NumTicket;
                
                MySqlParameter outErrorCode = new MySqlParameter("ErrorCode_", MySqlDbType.Int32);
                outErrorCode.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorCode);

                MySqlParameter outErrorMessage = new MySqlParameter("ErrorMessage_", MySqlDbType.Text);
                outErrorMessage.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorMessage);

                da.SelectCommand.ExecuteNonQuery();

                if (outErrorCode.Value.ToString() != "0")
                {
                    smensaje = outErrorMessage.Value.ToString();
                    return null;
                }

                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];

                if (Convert.ToInt32(row[0]) == -1)
                {
                    ce.Id = -1;
                    return ce;
                }

                if (Convert.ToInt32(row[0]) == -2)
                {
                    ce.Id = -2;
                    return ce;
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
                Console.WriteLine(ex.Message);
                ce.Id = -3;
                return ce;
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
                dt.Columns.Add("QtyDevolver", typeof(Int32));
                dt.Columns.Add("ImporteFinal", typeof(double));
                conn.CerrarConexion();

                return dt;
            } catch(Exception ex)
            {
                return null;
            }
        }

        public bool DevolVentaEjecuta(int UserId_, string sJson, string _NumTck, string FormaPago, string Folio, ref string sMensaje)
        {
            string _sStoredName = "";

            try
            {
                _sStoredName = "SP_V_DevolVenta";

                /*`SP_V_DevolVenta`(IN JItems JSON,
                                                                      IN NumTck_ VARCHAR(45),
                                                                      IN UserId_ int,
                                                                      IN FormaPago_  VARCHAR(3),
																 	  IN FolioElectronico_  VARCHAR(20),
                                                                      OUT IdHeader_ INT,
                                                                      out ErrorCode_ int,
                                                                      out ErrorMessage_ varchar(255) )
                
                  cmdprocesa.Parameters.Add("@JItems", MySqlDbType.JSON).Value = sJson;
                cmdprocesa.Parameters.Add("UserId_", MySqlDbType.Int32).Value = 0;
                cmdprocesa.Parameters.Add("TransId_", MySqlDbType.Int32).Value = iSequence;
                cmdprocesa.Parameters.Add("Comentarios_", MySqlDbType.VarChar).Value = sComentario;


                MySqlParameter outErrorCode = new MySqlParameter("@ErrorCode_", MySqlDbType.Int32);
                outErrorCode.Direction = ParameterDirection.Output;
                cmdprocesa.Parameters.Add(outErrorCode);

                MySqlParameter outErrorMessage = new MySqlParameter("@ErrorMessage_", MySqlDbType.VarChar);
                outErrorMessage.Direction = ParameterDirection.Output;
                cmdprocesa.Parameters.Add(outErrorMessage);

                cmdprocesa.ExecuteNonQuery();

                if (outErrorCode.Value.ToString() != "0")
                {
                    sMensaje = outErrorMessage.Value.ToString();
                    return false;
                }
                 */


                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@JItems", MySqlDbType.JSON).Value = sJson;
                da.SelectCommand.Parameters.Add("UserId_", MySqlDbType.Int32).Value = UserId_;
                da.SelectCommand.Parameters.Add("NumTck_", MySqlDbType.VarChar).Value = _NumTck;
                da.SelectCommand.Parameters.Add("FormaPago_", MySqlDbType.VarChar).Value = FormaPago;
                da.SelectCommand.Parameters.Add("FolioElectronico_", MySqlDbType.VarChar).Value = Folio;

                MySqlParameter outErrorCode = new MySqlParameter("ErrorCode_", MySqlDbType.Int32);
                outErrorCode.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorCode);

                MySqlParameter outErrorMessage = new MySqlParameter("ErrorMessage_", MySqlDbType.Text);
                outErrorMessage.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorMessage);

                MySqlParameter outIdHeader = new MySqlParameter("IdHeader_", MySqlDbType.Int32);
                outIdHeader.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outIdHeader);

                da.SelectCommand.ExecuteNonQuery();

                if (outErrorCode.Value.ToString() != "0")
                {
                    sMensaje = outErrorMessage.Value.ToString();
                    return false;
                }
                /*
                else
                {
                    sMensaje = outIdHeader.Value.ToString();
                }
                */

                da.SelectCommand.Parameters.Clear();

                return true;
            }
            catch (Exception ex)
            {
                sMensaje = "Excepcion tipo " + ex.GetType() + " " + ex.Message +
                               " ERROR mientras se ejecutaba la transacción [" + _sStoredName + "].";
                return false;
            }
        }

        public bool DevolucionHeader(string _NumTck, string FormaPago, string Folio, ref string sMensaje)
        {
            string _sStoredName = "";

            try
            {
                _sStoredName = "SP_V_DevolHeader";

                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("NumTck_", MySqlDbType.VarChar).Value = _NumTck;
                da.SelectCommand.Parameters.Add("FormaPago_", MySqlDbType.VarChar).Value = FormaPago;
                da.SelectCommand.Parameters.Add("FolioElectronico_", MySqlDbType.VarChar).Value = Folio;

                MySqlParameter outErrorCode = new MySqlParameter("ErrorCode_", MySqlDbType.Int32);
                outErrorCode.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorCode);

                MySqlParameter outErrorMessage = new MySqlParameter("ErrorMessage_", MySqlDbType.Text);
                outErrorMessage.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outErrorMessage);

                MySqlParameter outIdHeader = new MySqlParameter("IdHeader_", MySqlDbType.Int32);
                outIdHeader.Direction = ParameterDirection.Output;
                da.SelectCommand.Parameters.Add(outIdHeader);

                da.SelectCommand.ExecuteNonQuery();

                if (outErrorCode.Value.ToString() != "0")
                {
                    sMensaje = outErrorMessage.Value.ToString();
                    return false;
                } else
                {
                    sMensaje = outIdHeader.Value.ToString();
                }

                da.SelectCommand.Parameters.Clear();

                return true;
            } catch (Exception ex) { 
                sMensaje = "Excepcion tipo " + ex.GetType() + " " + ex.Message +
                               " ERROR mientras se ejecutaba la transacción [" + _sStoredName + "].";
                return false;
            }
        }

        public bool DevolucionDetalle(string _NumTck, int LineNum, double cantidad, int _idHeader, double _Monto, ref string sMensaje)
        {
            string _sStoredName = "";

            try
            {
                _sStoredName = "SP_V_DevolDetalle";

                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_NumTck", MySqlDbType.VarChar).Value = _NumTck;
                da.SelectCommand.Parameters.Add("_LineNum", MySqlDbType.Int32).Value = LineNum;
                da.SelectCommand.Parameters.Add("_Cantidad", MySqlDbType.Decimal).Value = cantidad;
                da.SelectCommand.Parameters.Add("_IdHeader", MySqlDbType.Int32).Value = _idHeader;
                da.SelectCommand.Parameters.Add("_Monto", MySqlDbType.Decimal).Value = _Monto;
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
            }
            catch (Exception ex)
            {
                sMensaje = "Excepcion tipo " + ex.GetType() + " " + ex.Message +
                               " ERROR mientras se ejecutaba la transacción [" + _sStoredName + "].";
                return false;
            }
        }

        public bool DevolucionCierre(int _IdHeader, int _UserId, ref string sMensaje)
        {
            string _sStoredName = "";

            try
            {
                _sStoredName = "SP_V_DevolCierre";

                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_IdHeader", MySqlDbType.Int32).Value = _IdHeader;
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
            }
            catch (Exception ex)
            {
                sMensaje = "Excepcion tipo " + ex.GetType() + " " + ex.Message +
                               " ERROR mientras se ejecutaba la transacción [" + _sStoredName + "].";
                return false;
            }
        }

        public List<PayForm> GetPayForms(string _NumTck, ref string sMensaje)
        {
            string _sStoredName = "SP_V_DevolPagos";
            
            List<PayForm> _listPayForm = new List<PayForm>();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(_sStoredName, conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_NumTck", MySqlDbType.VarChar).Value = _NumTck;
                da.SelectCommand.ExecuteNonQuery();
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    PayForm _obj = new PayForm();
                    DataRow dr = dt.Rows[i];
                    //_obj.Id = Convert.ToInt32(dr[0]);
                    _obj.Name = Convert.ToString(dr[0]);
                    _listPayForm.Add(_obj);
                }
                
                return _listPayForm;

            } catch (Exception ex)
            {
                return null;
            }
        }

        public class PayForm
        {
            public string Name { get; set; }
        }
    }
}
