using System.Data;
using Capa_Datos.Clientes;
using Capa_Entidad.Clientes;

namespace Capa_Negocio
{
    public class CN_Clientes
    {

        private readonly CD_Clientes objDatos = new CD_Clientes();

        #region Consultar
        public CE_Clientes Consulta(string CardCode)
        {
            return objDatos.Consulta(CardCode);
        }
        #endregion
    }
}
