using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Company
    {
        private readonly CD_Company objDatos = new CD_Company();

        #region consulta de company
        public CE_Company ConsultaDatos()
        {
            return objDatos.Consulta();
        }
        #endregion
    }
}
