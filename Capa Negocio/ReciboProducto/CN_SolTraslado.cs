using Capa_Datos.ReciboProducto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.ReciboProducto
{
    public class CN_SolTraslado
    {
        private readonly CD_SolTraslado objDatos = new CD_SolTraslado();

        #region CargarSolicitudTraslado
        public DataTable CargarSolTraslado(int NoTsr)
        {
            DataTable dt = objDatos.CargarSolTraslado(NoTsr);



            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = true;
            }

            dt.Columns["InQty"].ReadOnly = false;
            
            /*
            dt.Columns["LineNum"].ColumnName = "No.";
            dt.Columns["LineStatus"].ColumnName = "Activo";
            dt.Columns["ItemCode"].ColumnName = "Codigo";
            dt.Columns["Dscription"].ColumnName = "Descripción";
            dt.Columns["Quantity"].ColumnName = "Cantidad";
            dt.Columns["UmTsr"].ColumnName = "UmTsr";
            dt.Columns["Price"].ColumnName = "Costo";
            dt.Columns["ImporteTotal"].ColumnName = "Total";
            dt.Columns["Currency"].ColumnName = "Moneda";
            dt.Columns["OpenQty"].ColumnName = "Saldo";

            dt.Columns["InQty"].ReadOnly = false;
            dt.Columns["InQty"].ColumnName = "Cant.Recibo";
            
            
            dt.Columns["BaseQty"].ColumnName = "Base";
            dt.Columns["Umi"].ColumnName = "Umi";
            */

            return dt; // objDatos.CargarSolTraslado();
        }
        #endregion
    }

}
