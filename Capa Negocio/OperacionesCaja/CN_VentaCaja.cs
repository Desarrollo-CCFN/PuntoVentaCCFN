using System.Data;
using Capa_Datos;
using Capa_Datos.OperacionesCaja;
using Capa_Entidad;
using Capa_Entidad.OperacionesCaja;

namespace Capa_Negocio.OperacionesCaja
{
    public class CN_VentaCaja
    {
        private readonly CD_VentaCaja _cdVentaCaja = new CD_VentaCaja();

        public CE_VentaCaja insertVentaCaja(CE_VentaCaja obj)
        {
            return _cdVentaCaja.InsertarVentaCaja(obj);
        }

        public int insertCajaDenom(CE_Denominacion obj)
        {
           return  _cdVentaCaja.insertarCajaDenom(obj);
        }

        public int  insertMovCaja(CE_MovCaja obj) {
           return _cdVentaCaja.isertarMovCaja(obj);
        }

        public CE_VentaCaja infoCaja(string User, string whsCode, int stationId)
        {
            return _cdVentaCaja.infoApertura(User, whsCode, stationId);
        }

        public CE_MovCaja validateRetiro(int idCash)
        {
            return _cdVentaCaja.validateRetiro(idCash);
        }
    }
}
