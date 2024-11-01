using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_BusquedaReporte
    {

        private readonly CD_BusquedaReporte objDatos = new CD_BusquedaReporte();

        #region consulta de BusquedaReporte
        public CE_BusquedaReporte ConsultaDatos(int IdTra, int Param)
        {
            return objDatos.Consulta(IdTra,Param);
        }
        #endregion

        #region Busqueda Tipo Cambio

        public CE_BusquedaReporte ConsultaTipoCambio(decimal Tp)
        {
            return objDatos.ConsultaTipoCambio(Tp);
        }

        #endregion

    }
}
