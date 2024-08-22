using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Usuarios
    {
        private readonly CD_Usuarios objDatos =  new CD_Usuarios();

        #region Consultar
        public CE_Usuarios Consulta(int IdUsuario)
        {
            return objDatos.Consulta(IdUsuario);
        }
        #endregion

        #region Borrar
        public void Borrar(int userId)
        {
             objDatos.Borrar(userId);
        }
        #endregion




        #region Insertar
        public void Insertar(CE_Usuarios Usuaros)
        {
            objDatos.CD_INSERTAR(Usuaros);
        }
        #endregion

        #region Actualizar
        public void Actualizar(CE_Usuarios usuario)
        {
            objDatos.Actualizar(usuario);
        }
        #endregion


        #region Alta
        public void Alta(CE_Usuarios usuario)
        {
            objDatos.Alta(usuario);
        }
        #endregion



        #region CargarUsuarios
        public DataTable CargarUsuarios()
        {
            return objDatos.CargarUsuarios();
        }
        #endregion

        #region BuscarUsuario
        public DataTable BuscarUsuario(string usuario)
        {
            return objDatos.BuscarUsuario(usuario);
        }
        #endregion


        #region AutUsuario
        public DataTable AutUsuario(string usuario, string Password)
        {
            return objDatos.AutUsuario(usuario, Password);
        }
        #endregion



    }
}
