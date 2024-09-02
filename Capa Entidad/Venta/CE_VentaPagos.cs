
namespace Capa_Entidad.Venta
{
    public class CE_VentaPagos
    {
        private string _Payform;
        private string _Currency;
        private decimal _Rate;
        private decimal _AmountPay;
        private decimal _BalAmout;
        private int _IdHeader;
        private string _VoucherNum;

        public string Payform { get => _Payform; set => _Payform = value; }
        public string Currency { get => _Currency; set => _Currency = value; }
        public decimal Rate { get => _Rate; set => _Rate = value; }
        public decimal AmountPay { get => _AmountPay; set => _AmountPay = value; }
        public decimal BalAmout { get => _BalAmout; set => _BalAmout = value; }
        public int IdHeader { get => _IdHeader; set => _IdHeader = value; }
        public string VoucherNum { get => _VoucherNum; set => _VoucherNum = value; }
    }
}
