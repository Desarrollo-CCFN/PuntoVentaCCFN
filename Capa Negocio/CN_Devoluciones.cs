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

        public bool DevoluacionHeader(string _NumTck,ref string sMensaje)
        {
            return obDatos.DevolucionHeader(_NumTck, ref sMensaje);
        }

        public bool DevolucionDetalle(string _NumTck, int LineNum, double cantidad, ref string sMensaje)
        {
            return obDatos.DevolucionDetalle(_NumTck, LineNum, cantidad, ref sMensaje);
        }

        public bool DevolucionCierre(int idHeader, int UserId, ref string sMensaje)
        {
            return obDatos.DevolucionCierre(idHeader, UserId, ref sMensaje);
        }
    }
}
