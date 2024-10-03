using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_ListaPrecios
    {
        private readonly CD_Conexion? conn = new CD_Conexion();
        private CE_ListaPrecios? ce = new CE_ListaPrecios();
        #region Consultar
        public CE_ListaPrecios Consulta(int listNum)
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_LP_Consultar", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("listNum", MySqlDbType.Int32).Value = listNum;
                DataSet ds = new DataSet();
                ds.Clear();
                da.Fill(ds);
                DataTable dt;
                dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                ce.ListNum = Convert.ToInt32(row[0]);
                ce.ListName = Convert.ToString(row[1]);


                return ce;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

    }
}
