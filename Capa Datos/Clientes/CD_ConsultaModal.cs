using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Clientes;


namespace Capa_Datos.Clientes
{
    public class CD_ConsultaModal
    {
        private readonly CD_Conexion? conn = new CD_Conexion();

        public DataTable ConsultaClientesModal()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_C_ConsultaClientes", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            conn.CerrarConexion();

            return dt;
        }

        public DataTable ConsultaCliente(string busqueda)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_C_BuscarCliente", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("busqueda", MySqlDbType.VarChar).Value = busqueda;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            conn.CerrarConexion();

            return dt;
        }
    }
}
