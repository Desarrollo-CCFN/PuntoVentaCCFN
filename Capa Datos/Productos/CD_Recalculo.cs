using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Productos;

namespace Capa_Datos.Productos
{
    public class CD_Recalculo
    {
        private readonly CD_Conexion conn = new CD_Conexion();
        private CE_Recalculo ce = new CE_Recalculo();

        #region recalculoProductos
        public void Recalcular(List<String> productos, List<int> cantidades)
        {

        }
        #endregion
    }
}
