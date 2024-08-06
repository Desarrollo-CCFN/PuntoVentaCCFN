using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Usuarios
    {
        private readonly CD_Conexion? conn = new CD_Conexion();
        private CE_Usuarios? ce = new CE_Usuarios();

        //CRUD USUARIOS
        #region Consultar
        public CE_Usuarios Consulta(int idUsuario)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_U_Consultar", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("IdUsuario", MySqlDbType.Int32).Value = idUsuario;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.USERID = Convert.ToInt32(row[0]);
            ce.USER_CODE = Convert.ToString(row[2]);
            ce.U_NAME = Convert.ToString(row[1]);

            return ce;
        }
        #endregion
        #region Insertar
        public void CD_INSERTAR(CE_Usuarios Usuarios)
        {
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = conn.AbrirConexion(),
                CommandText = "SP_U_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            cmd.Parameters.AddWithValue("_USERID", Usuarios.USERID);
            cmd.Parameters.AddWithValue("_USER_CODE", Usuarios.USER_CODE);
            cmd.Parameters.AddWithValue("_U_NAME", Usuarios.U_NAME);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            conn.CerrarConexion();
        }
        #endregion
        #region CargarUsuarios
        public DataTable CargarUsuarios()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SP_U_CargarUsuarios", conn.AbrirConexion());
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
