using Capa_Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Capa_Datos
{

    public class CD_Company
    {
        private readonly CD_Conexion? conn = new CD_Conexion();
        private CE_Company ce = new CE_Company();

        public CE_Company Consulta()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_CO_ConsultarCompany", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
 

            /*
              ce.CompanyName = Convert.ToString(row[0]);
              ce.Filler = Convert.ToString(row[1]);
              ce.Bd = Convert.ToString(row[2]);
              ce.DefCardCode = Convert.ToString(row[3]);
              ce.DefRateCash = Convert.ToDecimal(row[4]);
              ce.DefRateCredit = Convert.ToDecimal(row[5]);
              ce.DefCurrency = Convert.ToString(row[6]);
              ce.DefListNum = Convert.ToInt32(row[7]);
              ce.DefSlpCode = Convert.ToInt32(row[8]);
              ce.DefSerieInv = Convert.ToString(row[9]);
            */
            ce.CompanyName = Convert.ToString(row[0]);
            ce.Filler = Convert.ToString(row[1]);
            ce.Bd = Convert.ToString(row[2]);
            ce.DefCardCode = Convert.ToString(row[3]);
            ce.DefRateCash = Convert.ToDecimal(row[4]);
            ce.DefRateCredit = Convert.ToDecimal(row[5]);
            ce.DefCurrency = Convert.ToString(row[6]);
            ce.DefListNum = Convert.ToInt32(row[7]);
            ce.DefSlpCode = Convert.ToInt32(row[8]);
            ce.DefSerieInv = Convert.ToString(row[9]);
            
           

            return ce;
        }
    }
}
