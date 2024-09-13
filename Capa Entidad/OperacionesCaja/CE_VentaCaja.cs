
namespace Capa_Entidad.OperacionesCaja
{
    public class CE_VentaCaja
    {
        private int _IdCash;
        private string user;
        private int serie;
        private string whsCode;
        private decimal begAmount;
        private decimal endAmount;
        private decimal begAmountFC;
        private decimal endAmountFC;
        private string cashier;
        private int stationId;

        public string User { get => user; set => user = value; }
        public int Serie { get => serie; set => serie = value; }
        public string WhsCode { get => whsCode; set => whsCode = value; }
        public decimal BegAmount { get => begAmount; set => begAmount = value; }
        public decimal EndAmount { get => endAmount; set => endAmount = value; }
        public decimal BegAmountFC { get => begAmountFC; set => begAmountFC = value; }
        public decimal EndAmountFC { get => endAmountFC; set => endAmountFC = value; }
        public string Cashier { get => cashier; set => cashier = value; }
        public int StationId { get => stationId; set => stationId = value; }
        public int IdCash { get => _IdCash; set => _IdCash = value; }
    }
}
