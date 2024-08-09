using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion
{
    internal class App_Preferences:ConfigurationSection
    {
        [ConfigurationProperty("cardcode", DefaultValue = "C00000024")]
        public string CardCode
        {
            get { return (string)this["cardcode"]; } 
            set { this["cardcode"] = value; }
        }

        [ConfigurationProperty("listaprecio", DefaultValue = 13)]
        public int PriceList
        {
            get { return (int)this["listaprecio"]; }
            set { this["listaprecio"] = value; }
        }

        [ConfigurationProperty("puertoimpresora", DefaultValue = "COM8")]
        public string Puerto
        {
            get { return (string)this["puertoimpresora"]; }
            set { this["puertoimpresora"] = value; }
        }

        [ConfigurationProperty("sucursal", DefaultValue = "S24")]
        public string Sucursal
        {
            get { return (string)this["sucursal"]; }
            set { this["sucursal"] = value; }
        }
    }
}
