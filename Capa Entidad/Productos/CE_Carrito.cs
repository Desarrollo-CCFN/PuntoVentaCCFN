namespace Capa_Entidad.Productos
{
    public class CE_Carrito
    {
        private string _ItemCode;
        private string _ItemName;
        private decimal _Precio_Base;
        private string _Unidad;
        private decimal _Total;
        private decimal _Impuesto;

        public string ItemCode { get => _ItemCode; set => _ItemCode = value; }
        public string ItemName { get => _ItemName; set => _ItemName = value; }
        public decimal Precio_Base { get => _Precio_Base; set => _Precio_Base = value; }
        public string Unidad { get => _Unidad; set => _Unidad = value; }
        public decimal Total { get => _Total; set => _Total = value; }
        public decimal Impuesto { get => _Impuesto; set => _Impuesto = value; }
    }
}
