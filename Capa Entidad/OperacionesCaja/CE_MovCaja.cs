

namespace Capa_Entidad.OperacionesCaja
{
    public class CE_MovCaja
    {
        private int idCash;
        private string type;
        private string currency;
        private decimal amount;
        private string comments;
        private string user;
        private string supervisor;
        private string sucursal;
        private int idtrans;



        public int IdCash { get => idCash; set => idCash = value; }
        public string Type { get => type; set => type = value; }
        public string Currency { get => currency; set => currency = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public string Comments { get => comments; set => comments = value; }
        public string User { get => user; set => user = value; }
        public string Supervisor { get => supervisor; set => supervisor = value; }
        public string Sucursal { get => sucursal; set => sucursal = value; }

        public int Idtrans { get => idtrans; set => idtrans = value; }

    }
}
