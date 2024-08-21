using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad;
using System.Windows;
using System.Windows.Input; 

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
            ce.DfltsGroup = Convert.ToString(row[3]);

            ce.Locked = Convert.ToString(row[4]);
            ce.SUPERUSER = Convert.ToString(row[5]);
            ce.PASSWORD4 = Convert.ToString(row[6]);


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

        #region Actualizar

        public void Actualizar(CE_Usuarios Usuarios)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn.AbrirConexion();
                    cmd.CommandText = "SP_U_ActualizarUsuarios";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_USERID", Usuarios.USERID);
                    cmd.Parameters.AddWithValue("_U_NAME", Usuarios.U_NAME);
                    cmd.Parameters.AddWithValue("_DfltsGroup", Usuarios.DfltsGroup);
                    cmd.Parameters.AddWithValue("_Locked", Usuarios.Locked);
                    cmd.Parameters.AddWithValue("_SUPERUSER", Usuarios.SUPERUSER);
                    cmd.Parameters.AddWithValue("_PASSWORD4", Usuarios.PASSWORD4);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                // Errores específicos de MySQL
                Console.WriteLine($"Error en la base de datos: {ex.Message}", "Error");



            }
            catch (Exception ex)
            {
                // otro tipo de error
                Console.WriteLine($"Ocurrió un error: {ex.Message}", "Error");
            }
            finally
            {
                conn.CerrarConexion();
            }

            /*   SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET U_NAME = @U_NAME, USER_CODE = @USER_CODE, DfltsGroup = @DfltsGroup, Locked = @Locked, SUPERUSER = @SUPERUSER WHERE USERID = @USERID", connection);
               cmd.Parameters.AddWithValue("@USERID", usuario.USERID); no
               cmd.Parameters.AddWithValue("@U_NAME", usuario.U_NAME);
               cmd.Parameters.AddWithValue("@USER_CODE", usuario.USER_CODE); NO
               cmd.Parameters.AddWithValue("@DfltsGroup", usuario.DfltsGroup);
               cmd.Parameters.AddWithValue("@Locked", usuario.Locked);
               cmd.Parameters.AddWithValue("@SUPERUSER", usuario.SUPERUSER);

               connection.Open();
               cmd.ExecuteNonQuery(); */

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
