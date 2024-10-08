﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace Capa_Datos.ReciboProducto
{
    public class CD_ProcesarRecibo
    {

        private readonly CD_Conexion? conn = new CD_Conexion();


        #region Procesar Solicitud de Traslado
        public bool CD_ProcesarSolTraslado(ref DataTable dt, ref string sMensaje, string sComentario)
        {
            // Validar si el DataTable tiene filas
            if (dt == null || dt.Rows.Count == 0)
            {
                sMensaje = "La Fila está vacío. No hay partidas por procesar.";
                return false;
            }

            int iRows = 0;

            // Genero el valor de la secuencia para la transacción
            MySqlCommand cmdSeq = new MySqlCommand("seq_next_id", conn.AbrirConexion());
            cmdSeq.CommandType = CommandType.StoredProcedure;
            MySqlParameter outSeqNextVal = new MySqlParameter("@NetVal", MySqlDbType.Int32);
            outSeqNextVal.Direction = ParameterDirection.Output;
            cmdSeq.Parameters.Add(outSeqNextVal);
            cmdSeq.ExecuteNonQuery();

            int iSequence = Convert.ToInt32(outSeqNextVal.Value);

            string sItemCode = "";
            int iDocEntry = 0;
            string sJson = "";
          //  string sComentarios = "";

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["InQty"].ToString() != "")
                    {
                        if (Convert.ToDouble(dt.Rows[i]["InQty"]) > 0)
                        {
                            iRows++;
                            sItemCode = dt.Rows[i]["ItemCode"].ToString();

                            if (sJson == "")
                            {
                                sJson = "[[" + Convert.ToString(dt.Rows[i]["LineNum"]) + "," + Convert.ToString(dt.Rows[i]["InQty"]) + "]";
                            }
                            else
                            {
                                sJson += ",[" + Convert.ToString(dt.Rows[i]["LineNum"]) + "," + Convert.ToString(dt.Rows[i]["InQty"]) + "]";
                            }
                        }
                    }
                    iDocEntry = Convert.ToInt32(dt.Rows[i]["DocEntry"]);
                  //   sComentarios = Convert.ToString(dt.Rows[0]["Comentarios"]); // en renglo cero se graba el comentario
                }

                sJson += "]";

                if (iRows == 0)
                {
                    sMensaje = "No existe partidas por procesar !!!";
                    return false;
                }

                // Proceso el recibo de producto
                MySqlCommand cmdprocesa = new MySqlCommand("SP_RP_ProcSolTsr", conn.AbrirConexion());
                cmdprocesa.CommandType = CommandType.StoredProcedure;

                cmdprocesa.Parameters.Add("@JItems", MySqlDbType.JSON).Value = sJson;
                cmdprocesa.Parameters.Add("DocEntry_", MySqlDbType.Int32).Value = iDocEntry;
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

                cmdprocesa.Parameters.Clear();
            }
            catch (Exception ex1)
            {
                sMensaje = "Excepción tipo " + ex1.GetType() + " " + ex1.Message +
                           " ERROR mientras se ejecutaba la transacción [" + sItemCode + "].";
                return false;
            }

            return true;
        }

        #endregion
    }
}
