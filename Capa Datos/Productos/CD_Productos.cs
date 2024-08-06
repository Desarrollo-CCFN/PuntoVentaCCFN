using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Productos;
namespace Capa_Datos.Productos
{
    public class CD_Productos
    {
        private readonly CD_Conexion? conn = new CD_Conexion();
        private readonly CE_Productos ce = new CE_Productos();

        #region CargarProductos
        public DataTable CargarProductos()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_P_CargarProductos", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            conn.CerrarConexion();

            return dt;
        }
        #endregion
    }
}
