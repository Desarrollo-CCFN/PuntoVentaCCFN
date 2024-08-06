using System.Data;
using Capa_Datos.Clientes;
using Capa_Entidad.Clientes;

namespace Capa_Negocio.Clientes
{
    
    public class CN_ConsultaModal
    {
        private readonly CD_ConsultaModal objDatos = new CD_ConsultaModal();

        public DataTable ConsultaClientesModal()
        {
            return objDatos.ConsultaClientesModal();
        }

        public DataTable ConsultarCliente(string busqueda)
        {
            return objDatos.ConsultaCliente(busqueda);
        }
    }
}
