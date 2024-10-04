using Capa_Entidad;
using Capa_Negocio;
using PuntoVentaCCFN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion
{
    public class App_Preferences : ConfigurationSection
    {
        private readonly CN_Company cn_Company = new CN_Company();
        CE_Company objCompany = new CE_Company();

        private static ConfigurationPropertyCollection _Properties;
        private static ConfigurationProperty _CompanyName;
        private static ConfigurationProperty _Filler;
        private static ConfigurationProperty _Bd;
        private static ConfigurationProperty _DefCardCode;
        private static ConfigurationProperty _DefRateCash;
        private static ConfigurationProperty _DefRateCredit;
        private static ConfigurationProperty _DefCurrency;
        private static ConfigurationProperty _DefListNum;
        private static ConfigurationProperty _DefSlpCode;
        private static ConfigurationProperty _DefSerieInv;


        public App_Preferences()
        {

            objCompany = cn_Company.ConsultaDatos();
              _CompanyName = new ConfigurationProperty("companyName", typeof(string), objCompany.CompanyName);
              _Filler = new ConfigurationProperty("filler", typeof(string), objCompany.Filler);
              _Bd = new ConfigurationProperty("bd", typeof(string), objCompany.Bd);
              _DefCardCode = new ConfigurationProperty("cardcode", typeof(string), objCompany.DefCardCode);
              _DefRateCash = new ConfigurationProperty("defRateCash", typeof(decimal), objCompany.DefRateCash);
              _DefRateCredit = new ConfigurationProperty("defRateCredit", typeof(decimal), objCompany.DefRateCredit);
              _DefCurrency = new ConfigurationProperty("defCurrency", typeof(string), objCompany.DefCurrency);
              _DefListNum = new ConfigurationProperty("defListNum", typeof(int), objCompany.DefListNum);
              _DefSlpCode = new ConfigurationProperty("defSlpCode", typeof(int), objCompany.DefSlpCode);
              _DefSerieInv = new ConfigurationProperty("delSerieInv", typeof(string), objCompany.DefSerieInv);   

           

          /*  _CompanyName = new ConfigurationProperty("companyName", typeof(string), MainWindow.AppConfig1.CompanyName);
            _Filler = new ConfigurationProperty("filler", typeof(string), MainWindow.AppConfig1.Filler);
            _Bd = new ConfigurationProperty("bd", typeof(string), MainWindow.AppConfig1.bd);
            _DefCardCode = new ConfigurationProperty("cardcode", typeof(string), MainWindow.AppConfig1.DefCardCode);
            _DefRateCash = new ConfigurationProperty("defRateCash", typeof(decimal), MainWindow.AppConfig1.DefRateCash);
            _DefRateCredit = new ConfigurationProperty("defRateCredit", typeof(decimal), MainWindow.AppConfig1.DefRateCredit);
            _DefCurrency = new ConfigurationProperty("defCurrency", typeof(string), MainWindow.AppConfig1.DefCurrency);
            _DefListNum = new ConfigurationProperty("defListNum", typeof(int), MainWindow.AppConfig1.DefListNum);
            _DefSlpCode = new ConfigurationProperty("defSlpCode", typeof(int), MainWindow.AppConfig1.DefSlpCode);
            _DefSerieInv = new ConfigurationProperty("delSerieInv", typeof(string), MainWindow.AppConfig1.DefSerieInv);
        */


            _Properties = new ConfigurationPropertyCollection();
            _Properties.Add(_CompanyName);
            _Properties.Add(_Filler);
            _Properties.Add(_Bd);
            _Properties.Add(_DefCardCode);
            _Properties.Add(_DefRateCash);
            _Properties.Add(_DefRateCredit);
            _Properties.Add(_DefCurrency);
            _Properties.Add(_DefListNum);
            _Properties.Add(_DefSlpCode);
            _Properties.Add(_DefSerieInv);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }

        public string CompanyName
        {
            get { return (string)this["companyName"]; }
            set { this["companyName"] = value; }
        }

        public string Filler
        {
            get { return (string)this["filler"]; }
            set { this["filler"] = value; }
        }

        public string Bd
        {
            get { return (string)this["bd"]; }
            set { this["bd"] = value; }
        }

        public string DefCardCode
        {
            get { return (string)this["cardcode"]; }
            set { this["cardcode"] = value; }
        }

        public decimal DefRateCash
        {
            get { return (decimal)this["defRateCash"]; }
            set { this["defRateCash"] = value; }
        }

        public decimal DefRateCredit
        {
            get { return (decimal)this["defRateCredit"]; }
            set { this["defRateCredit"] = value; }
        }

        public string DefCurrency
        {
            get { return (string)this["defCurrency"]; }
            set { this["defCurrency"] = value; }
        }

        public int DefListNum
        {
            get { return (int)this["defListNum"]; }
            set { this["defListNum"] = value; }
        }

        public int DefSlpCode
        {
            get { return (int)this["defSlpCode"]; }
            set { this["defSlpCode"] = value; }
        }

        public string DefSerieInv
        {
            get { return (string)this["defSerieInv"]; }
            set { this["defSerieInv"] = value; }
        }

    }
}
