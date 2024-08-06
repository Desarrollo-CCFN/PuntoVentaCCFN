namespace Capa_Entidad.Clientes
{
    public class CE_ConsultaModal
    {
        private string _CardCode;
        private string _CardFName;
        private string _ListName;
        private int _ListNum;

        public string CardCode { get => _CardCode; set => _CardCode = value; }
        public string CardName { get => _CardFName; set => _CardFName = value; }
        public string ListName { get => _ListName; set => _ListName = value; }
        public int ListNum { get => _ListNum; set => _ListNum = value; }
    }
}
