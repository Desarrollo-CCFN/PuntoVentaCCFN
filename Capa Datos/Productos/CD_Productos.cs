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

        #region busqueda producto x nombre
        public DataTable ConsultaProducto(string busqueda)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_P_BusquedaProductoManual", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("busqueda", MySqlDbType.VarChar).Value = busqueda;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            conn.CerrarConexion();

            return dt;
        }
        #endregion

        #region busqueda de um de producto
        public List<CE_ProductUm> ConsultaUM(string itemCode)
        {
            List<CE_ProductUm> listUm = new List<CE_ProductUm> ();
            MySqlCommand conm = new MySqlCommand("SP_P_BusquedaUOM", conn.AbrirConexion());
            conm.CommandType = CommandType.StoredProcedure;
            conm.Parameters.Add("itemCode", MySqlDbType.VarChar).Value = itemCode;
            MySqlDataReader dr = conm.ExecuteReader();

            while(dr.Read())
            {
                listUm.Add(new CE_ProductUm { UomEntry = dr["UomEntry"].ToString(), UomCode = dr["UomCode"].ToString()});
            }
            conn.CerrarConexion();

            return listUm;
        }
        #endregion
    }
}
