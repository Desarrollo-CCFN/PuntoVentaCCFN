using System.Data;
using Capa_Datos;
using Capa_Datos.Venta;
using Capa_Entidad.Venta;

namespace Capa_Negocio.Venta
{
    public class CN_Venta
    {
        private readonly CD_Venta objDatos = new CD_Venta();


        #region insert venta headerInical
        public CE_VentaHeader insertarVenta(string sucursal, string caja)
        {
            return objDatos.Venta(sucursal, caja);
        }
        #endregion

        #region insertar venta headerFinal
        public void insertarHeader(string numTck, DateTime fecha, decimal total, string usuario)
        {
            objDatos.ventaHeader(numTck, fecha, total, usuario);
        }
        #endregion

        public void insertarDetalle(string numTck, string itemCode, decimal cantidad, decimal monto)
        {
            objDatos.ventaDetalle(numTck, itemCode, cantidad, monto);
        }

        public void insertarDetalleLive(CE_VentaDetalle detalle)
        {
            objDatos.ventaDetalleLive(detalle);
        }

        public void insertarVentaPago(CE_VentaPagos ventaPago)
        {
            objDatos.ventaPagos(ventaPago);
        }
    }
}
