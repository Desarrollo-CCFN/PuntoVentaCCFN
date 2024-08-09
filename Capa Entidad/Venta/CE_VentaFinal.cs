
namespace Capa_Entidad.Venta
{
    public class CE_VentaFinal
    {
        private int _Id;
        private string _Usuario;
        private string _DocCur;
        private int _PriceList;
        private string _Filler;
        private string _Comments;

        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string DocCur { get => _DocCur; set => _DocCur = value; }
        public int PriceList { get => _PriceList; set => _PriceList = value; }
        public string Filler { get => _Filler; set => _Filler = value; }
        public string Comments { get => _Comments; set => _Comments = value; }
        public int Id { get => _Id; set => _Id = value; }
    }
}
