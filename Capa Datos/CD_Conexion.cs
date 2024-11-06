using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Capa_Datos
{
    public static class Configuracion
    {
        // Cadena de conexión a la base de datos
        //  public static readonly string CadenaConexion = "server=192.168.101.7;uid=desarrollo2; pwd=Chivas.2024;database=ccfn_desarrollo;";
          public static readonly string CadenaConexion = "server=10.101.1.130;uid=root; pwd=root.2024;database=db_s12;";
        // public static readonly string CadenaConexion = "server=localhost;uid=root; pwd=root.2024;database=db_s12;";


    }


    public class CD_Conexion
    {
      //   private readonly MySqlConnection conn = new MySqlConnection("server=10.101.1.130;uid=root; pwd=root.2024;database=db_s12;");
       // private readonly MySqlConnection conn = new MySqlConnection("server=localhost;uid=root; pwd=root.2024;database=db_s12;");
        //public readonly MySqlConnection conn = new MySqlConnection("server=192.168.101.7;uid=desarrollo2; pwd=Chivas.2024;database=ccfn_desarrollo;");
        public readonly MySqlConnection conn = new MySqlConnection(Configuracion.CadenaConexion);

        public MySqlConnection AbrirConexion()
        {
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn;
        }
 


        public MySqlConnection CerrarConexion()
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            return conn;
        }
 
         

    }
}
