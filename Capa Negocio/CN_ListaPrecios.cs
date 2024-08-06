using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_ListaPrecios
    {
        private readonly CD_ListaPrecios objDatos = new CD_ListaPrecios();

        #region Consultar
        public CE_ListaPrecios Consulta(int listNum)
        {
            return objDatos.Consulta(listNum);
        }
        #endregion
    }
}
