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
        #region Insertar
        public void Insertar(CE_Usuarios Usuaros)
        {
            objDatos.CD_INSERTAR(Usuaros);
        }
        #endregion
        #region CargarUsuarios
        public DataTable CargarUsuarios()
        {
            return objDatos.CargarUsuarios();
        }
        #endregion  
    }
}
