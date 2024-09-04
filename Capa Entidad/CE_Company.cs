

namespace Capa_Entidad
{
    public class CE_Company
    {
        private string companyName;
        private string filler;
        private string bd;
        private string defCardCode;
        private decimal defRateCash;
        private decimal defRateCredit;
        private string defCurrency;
        private int defListNum;
        private int defSlpCode;
        private string defSerieInv;

        public string CompanyName { get => companyName; set => companyName = value; }
        public string Filler { get => filler; set => filler = value; }
        public string Bd { get => bd; set => bd = value; }
        public string DefCardCode { get => defCardCode; set => defCardCode = value; }
        public decimal DefRateCash { get => defRateCash; set => defRateCash = value; }
        public decimal DefRateCredit { get => defRateCredit; set => defRateCredit = value; }
        public string DefCurrency { get => defCurrency; set => defCurrency = value; }
        public int DefListNum { get => defListNum; set => defListNum = value; }
        public int DefSlpCode { get => defSlpCode; set => defSlpCode = value; }
        public string DefSerieInv { get => defSerieInv; set => defSerieInv = value; }
    }
}
