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
        public CE_VentaHeader insertarVenta(string sucursal, string caja, string cardCode, int idCash)
        {
            return objDatos.Venta(sucursal, caja, cardCode, idCash);

        }
        #endregion

        #region insertar venta HeaderFinal
        public void insertarHeaderFinal(CE_VentaHeader ce, int _userId, ref string sMensaje)
        {
            objDatos.ventaFinal(ce, _userId, ref sMensaje);
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

        public bool insertarDetalleLive(CE_VentaDetalle detalle, ref string sMensaje)
        {
            return objDatos.ventaDetalleLive(detalle, ref sMensaje);
        }

        public void insertarVentaPago(CE_VentaPagos ventaPago)
        {
            objDatos.ventaPagos(ventaPago);
        }
    }
}
