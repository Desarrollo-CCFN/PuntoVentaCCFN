using System.Data;
using Capa_Datos;
using Capa_Entidad.Devoluciones;


namespace Capa_Negocio
{
    public class CN_Devoluciones
    {
        private readonly CD_Devoluciones obDatos = new CD_Devoluciones();

        public CE_DevolucionHeader CargarHeader(string NumTicket) { 
            return obDatos.CargarHeader(NumTicket);
        }

        public DataTable CargarDetalle(string NumTicket)
        {
            return obDatos.CargarDetalle(NumTicket);
        }

        public bool DevoluacionHeader(int _idHeader, int __UserId, ref string sMensaje)
        {
            return obDatos.DevolucionHeader(_idHeader, __UserId, ref sMensaje);
        }
    }
}
