using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Venta
{
    public class CD_GeneraFactura
    {
        private readonly CD_Conexion conn = new CD_Conexion();

        public int GenerarFactura(int _IdHeader, string _CardCode, string _UsoCfdi, ref string _Mensaje)
        {
            
            MySqlCommand cmd = new MySqlCommand("SP_V_Facturar", conn.AbrirConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IdHeader_", MySqlDbType.Int32).Value = _IdHeader; ;
            cmd.Parameters.Add("@UserId_", MySqlDbType.Int32).Value = 0;
            cmd.Parameters.Add("@CardCode_", MySqlDbType.VarChar).Value = _CardCode;
            cmd.Parameters.Add("@UsoCfdi_", MySqlDbType.VarChar).Value = _UsoCfdi;
            
            MySqlParameter outDocEntry = new MySqlParameter("@DocEntry_", MySqlDbType.Int32);
            outDocEntry.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outDocEntry);

            MySqlParameter outErrorCode = new MySqlParameter("@ErrorCode_", MySqlDbType.Int32);
            outErrorCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outErrorCode);

            MySqlParameter outErrorMessage = new MySqlParameter("@ErrorMessage_", MySqlDbType.VarChar);
            outErrorMessage.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outErrorMessage);

            cmd.ExecuteNonQuery();
            conn.CerrarConexion();

            if (outErrorCode.Value.ToString() != "0")
            {
                _Mensaje = outErrorMessage.Value.ToString();
                return -1;
            }
            else
            {
                return Convert.ToInt32(outDocEntry.Value);
            }

        }
    }
}
