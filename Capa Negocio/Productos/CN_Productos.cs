using System.Data;
using Capa_Datos.Productos;
using Capa_Entidad;
using Capa_Entidad.Productos;

namespace Capa_Negocio.Productos
{
    public class CN_Productos
    {
        private readonly CD_Productos objDatos = new CD_Productos();

        #region CargarProductos
        public DataTable CargarProductos()
        {
            return objDatos.CargarProductos();
        }
        #endregion

        #region busqueda producto x nombre
        public DataTable BusquedaProducto(string busqueda)
        {
            return objDatos.ConsultaProducto(busqueda);
        }
        #endregion

        #region busqueda um de producto
        public List<CE_ProductUm> BusquedaUm(string busqueda)
        {
            return objDatos.ConsultaUM(busqueda);
        }
        #endregion
    }
}
