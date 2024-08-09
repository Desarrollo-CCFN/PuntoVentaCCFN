namespace Capa_Entidad.Venta
{
    public class CE_VentaHeader
    {
        private int _Id;
        private string _NumTck;
        private DateTime _Fecha;
        private decimal _Total;
        private string _Usuario;
        private string _CardCode;
        private string _CardName;
        private decimal _VatPercent;
        private decimal _VatSum;
        private decimal _VatSumFC;
        private decimal _DiscPrcnt;
        private decimal _DiscSum;
        private decimal _DiscSumFC;
        private string _DocCur;
        private decimal _DocRate;
        private decimal _DocTotal;
        private decimal _DocTotalFC;
        private string _Comments;
        private DateTime _CreateDate;
        private string _Filler;
        private DateTime _CancelDate;
        private string _UserCancel;
        private char _CANCELED;
        private DateTime _ClosedDate;
        private int _IdCash;
        private int _PriceList;
        private int _SlpCode;

        public int Id { get => _Id; set => _Id = value; }
        public string NumTck { get => _NumTck; set => _NumTck = value; }
        public DateTime Fecha { get => _Fecha; set => _Fecha = value; }
        public decimal Total { get => _Total; set => _Total = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string CardCode { get => _CardCode; set => _CardCode = value; }
        public string CardName { get => _CardName; set => _CardName = value; }
        public decimal VatPercent { get => _VatPercent; set => _VatPercent = value; }
        public decimal VatSum { get => _VatSum; set => _VatSum = value; }
        public decimal VatSumFC { get => _VatSumFC; set => _VatSumFC = value; }
        public decimal DiscPrcnt { get => _DiscPrcnt; set => _DiscPrcnt = value; }
        public decimal DiscSum { get => _DiscSum; set => _DiscSum = value; }
        public decimal DiscSumFC { get => _DiscSumFC; set => _DiscSumFC = value; }
        public string DocCur { get => _DocCur; set => _DocCur = value; }
        public decimal DocRate { get => _DocRate; set => _DocRate = value; }
        public decimal DocTotal { get => _DocTotal; set => _DocTotal = value; }
        public decimal DocTotalFC { get => _DocTotalFC; set => _DocTotalFC = value; }
        public string Comments { get => _Comments; set => _Comments = value; }
        public DateTime CreateDate { get => _CreateDate; set => _CreateDate = value; }
        public string Filler { get => _Filler; set => _Filler = value; }
        public DateTime CancelDate { get => _CancelDate; set => _CancelDate = value; }
        public string UserCancel { get => _UserCancel; set => _UserCancel = value; }
        public char CANCELED { get => _CANCELED; set => _CANCELED = value; }
        public DateTime ClosedDate { get => _ClosedDate; set => _ClosedDate = value; }
        public int IdCash { get => _IdCash; set => _IdCash = value; }
        public int PriceList { get => _PriceList; set => _PriceList = value; }
        public int SlpCode { get => _SlpCode; set => _SlpCode = value; }
    }
}
