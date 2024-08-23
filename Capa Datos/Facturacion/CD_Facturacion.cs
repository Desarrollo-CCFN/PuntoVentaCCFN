using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad.Facturacion;
using Mysqlx.Cursor;

namespace Capa_Datos.Facturacion
{
    public class CD_Facturacion
    {
        private readonly CD_Conexion conn = new CD_Conexion();
        //private readonly CE_SatUso cE_SatUso = new CE_SatUso();

        public List<CE_SatUso> obtenerListaSatUso()
        {
            List<CE_SatUso> listUso = new List<CE_SatUso>();
            MySqlCommand conm = new MySqlCommand("SP_F_ObtenerUso", conn.AbrirConexion());
            conm.CommandType = CommandType.StoredProcedure;
            MySqlDataReader dr = conm.ExecuteReader();
            while (dr.Read())
            {
                listUso.Add(new CE_SatUso() { CveUso = dr["CveUso"].ToString(), DescripUso = dr["DescripUso"].ToString() });
            }
            conn.CerrarConexion();
            return listUso;
        }
    }
}
