namespace Capa_Entidad
{
    public class CE_CierreCaja
    {
        private int _IdCash;
        private String _User;
        private String _WhsCode;
        private decimal _BegAmount;
        private decimal _EndAmount;
        private decimal _WdalAmount;
        private decimal _BegAmountFC;
        private decimal _EndAmountFC;
        private decimal _WdalAmountFC;
        private decimal _BegAmountT;
        private decimal _EndAmountT;
        private decimal _WdalAmountT;
        private decimal _PayAmount;
        private decimal _PayAmountFC;

        public int IdCash { get => _IdCash; set => _IdCash = value; }
        public string User { get => _User; set => _User = value; }
        public string WhsCode { get => _WhsCode; set => _WhsCode = value; }
        public decimal BegAmount { get => _BegAmount; set => _BegAmount = value; }
        public decimal EndAmount { get => _EndAmount; set => _EndAmount = value; }
        public decimal WdalAmount { get => _WdalAmount; set => _WdalAmount = value; }
        public decimal BegAmountFC { get => _BegAmountFC; set => _BegAmountFC = value; }
        public decimal EndAmountFC { get => _EndAmountFC; set => _EndAmountFC = value; }
        public decimal WdalAmountFC { get => _WdalAmountFC; set => _WdalAmountFC = value; }
        public decimal BegAmountT { get => _BegAmountT; set => _BegAmountT = value; }
        public decimal EndAmountT { get => _EndAmountT; set => _EndAmountT = value; }
        public decimal WdalAmountT { get => _WdalAmountT; set => _WdalAmountT = value; }
        public decimal PayAmount { get => _PayAmount; set => _PayAmount = value; }
        public decimal PayAmountFC { get => _PayAmountFC; set => _PayAmountFC = value; }
    }
}
