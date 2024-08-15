using System.Data;
using Capa_Datos.Productos;
using Capa_Entidad;

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
    }
}
