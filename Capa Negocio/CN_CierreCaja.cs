using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_CierreCaja
    {
        private readonly CD_CierreCaja _caja = new CD_CierreCaja();

        public DataTable CargaHeader(int idCash)
        {
            return _caja.Consulta(idCash);
        }

        public DataTable CargarDetalle(int idCash)
        {
            return _caja.ConsultaDetalle(idCash);
        }
    }
}
