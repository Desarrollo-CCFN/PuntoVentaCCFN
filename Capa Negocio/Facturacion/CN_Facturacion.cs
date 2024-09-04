using System.Data;
using Capa_Datos.Facturacion;
using Capa_Entidad;
using Capa_Entidad.Facturacion;

namespace Capa_Negocio.Facturacion
{
    public class CN_Facturacion
    {
        private readonly CD_Facturacion _facturacion = new CD_Facturacion();

        #region obtener usos sat
        public List<CE_SatUso> BusquedaUsos()
        {
            return _facturacion.obtenerListaSatUso();
        }
        #endregion
    }
}
