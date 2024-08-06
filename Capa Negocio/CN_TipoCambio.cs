using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_TipoCambio
    {
        private readonly CD_TipoCambio objDatos = new CD_TipoCambio();

        public CE_TipoCambio Consulta()
        {
            return objDatos.Consulta();
        }
    }
}
