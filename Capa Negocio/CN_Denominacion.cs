﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capa_Datos;
using Capa_Entidad;
using System.Xml.Linq;

namespace Capa_Negocio
{
     public class CN_Denominacion
    {
        private readonly CD_Denominacion objDatos = new  CD_Denominacion();


        #region Consultar
        //  public CE_Denominacion Consulta(string _PayForm)
        public List<CE_Denominacion> Consulta(string _PayForm)
        {
             
            return objDatos.consulta(_PayForm);
        }
        #endregion



        #region Cajeras
        //  public CE_Denominacion Consulta(string _PayForm)
        public List<CE_Denominacion> Cajeras(string DfltsGroup)
        {

            return objDatos.Cajeras(DfltsGroup);
        }
        #endregion






        #region InsertarRetiro

        public   string  InsertarRetiro(CE_Denominacion Retiros)
        {
            return objDatos.InsertarRetiro(Retiros);
        }

        #endregion


        #region Movimiento Caja
        
      public string VerificarCaja(int stationId, string Sucursal)
        {
           return objDatos.VerificarCaja(stationId, Sucursal);
        }


        #endregion

    }
}