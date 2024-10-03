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
        public CE_VentaHeader insertarVenta(string sucursal, string caja, string cardCode, int idCash, int idStation)
        {
            return objDatos.Venta(sucursal, caja, cardCode, idCash, idStation);

        }
        #endregion

        #region insertar venta HeaderFinal
        public bool insertarHeaderFinal(CE_VentaHeader ce, int _userId, ref string sMensaje)
        {
            return objDatos.ventaFinal(ce, _userId, ref sMensaje);
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

        public bool anularVenta(int idHeader)
        {
            return objDatos.AnularVenta(idHeader);
        }

        public bool AnularFpago(int idHeader)
        {
            return objDatos.AnularFPagos(idHeader);
        }

        public CE_VentaHeader VentaActiva(string WhsCode, int idStation)
        {
            return objDatos.VentaActiva(WhsCode, idStation);
        }

        public List<CE_VentaDetalle> GetVentaDetalle(int idHeader)
        {
            return objDatos.GetVentaDetalles(idHeader);
        }

        public decimal GetVentaActivaPagado(int idHeader)
        {
            return objDatos.VentaActivaPagado(idHeader);
        }

        public bool cerradoCaja(int station, string whsCode)
        {
            return objDatos.CerrarCaja(station, whsCode);
        }

    }
}
