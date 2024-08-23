using System.Data;
using Capa_Datos.Productos;
using Capa_Entidad;

namespace Capa_Negocio.Productos
{
    public class CN_Carrito
    {

        private readonly CD_Carrito objDatos = new CD_Carrito();

        #region buscarproductoVenta
        public DataTable buscarProducto(string bcdCode, int listNum, string currency, string whsCode, int idHeader, ref string sMensaje)
        {
            return objDatos.Buscar(bcdCode, listNum, currency, whsCode, idHeader, ref sMensaje);
        }
        #endregion
    }
}
