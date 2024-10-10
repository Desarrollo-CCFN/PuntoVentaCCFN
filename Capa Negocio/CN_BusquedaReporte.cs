using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    internal class CN_BusquedaReporte
    {

        private readonly CD_BusquedaReporte objDatos = new CD_BusquedaReporte();

        #region consulta de BusquedaReporte
        public CE_BusquedaReporte ConsultaDatos()
        {
            return objDatos.Consulta();
        }
        #endregion


    }
}
