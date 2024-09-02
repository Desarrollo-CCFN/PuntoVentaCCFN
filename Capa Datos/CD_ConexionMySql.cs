using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public static class CD_ConexionMySql
    {
        public static bool Open(bool bProductiva, ref MySql.Data.MySqlClient.MySqlConnection connMySql, ref string sMensaje)
        {
            bool Respuesta = false;
            string myConnectionString;
            //--  MySql.Data.MySqlClient.MySqlConnection conn;

            if (bProductiva)
            {
                myConnectionString = "server=192.168.101.7;uid=root;" +
                   "pwd=root.2024;database=ccfn_desarrollo";
            }
            else
            {
                myConnectionString = "server=localhost;uid=root;" +
                   "pwd=690305;database=ccfn_desarrollo";
            }

            try
            {
                connMySql = new MySql.Data.MySqlClient.MySqlConnection();
                connMySql.ConnectionString = myConnectionString;
                connMySql.Open();

                Respuesta = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Respuesta = false;
                sMensaje = ex.Message;
            }

            return Respuesta;
        }
    }
}
