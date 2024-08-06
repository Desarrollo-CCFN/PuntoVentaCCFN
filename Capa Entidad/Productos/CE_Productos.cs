namespace Capa_Entidad.Productos
{
    public class CE_Productos
    {
        private string _ItemCode;
        private string _ItemName;
        private decimal _OnHand;

        public string ItemCode { get => _ItemCode; set => _ItemCode = value; }
        public string ItemName { get => _ItemName; set => _ItemName = value; }
        public decimal OnHand { get => _OnHand; set => _OnHand = value; }
    }
}
