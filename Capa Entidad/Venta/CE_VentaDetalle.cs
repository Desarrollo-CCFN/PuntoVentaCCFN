namespace Capa_Entidad.Venta
{
    public class CE_VentaDetalle
    {
        private int _id;
        private string _NumTck;
        private string _ItemCode;
        private decimal _Cantidad;
        private decimal _Monto;
        private decimal _Descrip;
        private string _Unidad;
        private int _LineNum;
        private char _LineStatus;
        private string _Dscription;
        private decimal _Quantity;
        private decimal _Price;
        private decimal _PriceList;
        private decimal _Price2;
        private string _Currency;
        private decimal _Rate;
        private decimal _DiscPrcnt;
        private decimal _LineTotal;
        private decimal _TotalFrgn;
        private string _CodeBars;
        private decimal _VatPrcnt;
        private decimal _VatSum;
        private decimal _VatSumFrgn;
        private string _TaxCode;
        private int _UomEntry;
        private int _UomEntry2;
        private string _WhsCode;
        private decimal _InvQty;
        private char _PriceEdit;
        private string _UserAuthPriceEdit;
        private int _IdHeader;

        public int Id { get => _id; set => _id = value; }
        public string NumTck { get => _NumTck; set => _NumTck = value; }
        public string ItemCode { get => _ItemCode; set => _ItemCode = value; }
        public decimal Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public decimal Monto { get => _Monto; set => _Monto = value; }
        public decimal Descrip { get => _Descrip; set => _Descrip = value; }
        public string Unidad { get => _Unidad; set => _Unidad = value; }
        public int LineNum { get => _LineNum; set => _LineNum = value; }
        public char LineStatus { get => _LineStatus; set => _LineStatus = value; }
        public string Dscription { get => _Dscription; set => _Dscription = value; }
        public decimal Quantity { get => _Quantity; set => _Quantity = value; }
        public decimal Price { get => _Price; set => _Price = value; }
        public decimal PriceList { get => _PriceList; set => _PriceList = value; }
        public decimal Price2 { get => _Price2; set => _Price2 = value; }
        public string Currency { get => _Currency; set => _Currency = value; }
        public decimal Rate { get => _Rate; set => _Rate = value; }
        public decimal DiscPrcnt { get => _DiscPrcnt; set => _DiscPrcnt = value; }
        public decimal LineTotal { get => _LineTotal; set => _LineTotal = value; }
        public decimal TotalFrgn { get => _TotalFrgn; set => _TotalFrgn = value; }
        public string CodeBars { get => _CodeBars; set => _CodeBars = value; }
        public decimal VatPrcnt { get => _VatPrcnt; set => _VatPrcnt = value; }
        public decimal VatSum { get => _VatSum; set => _VatSum = value; }
        public decimal VatSumFrgn { get => _VatSumFrgn; set => _VatSumFrgn = value; }
        public string TaxCode { get => _TaxCode; set => _TaxCode = value; }
        public int UomEntry { get => _UomEntry; set => _UomEntry = value; }
        public int UomEntry2 { get => _UomEntry2; set => _UomEntry2 = value; }
        public string WhsCode { get => _WhsCode; set => _WhsCode = value; }
        public decimal InvQty { get => _InvQty; set => _InvQty = value; }
        public char PriceEdit { get => _PriceEdit; set => _PriceEdit = value; }
        public string UserAuthPriceEdit { get => _UserAuthPriceEdit; set => _UserAuthPriceEdit = value; }
        public int IdHeader { get => _IdHeader; set => _IdHeader = value; }
    }
}
