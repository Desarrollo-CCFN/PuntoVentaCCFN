namespace Capa_Entidad.Productos
{
    public class CE_Recalculo
    {
        private List<String> _p_Productos;
        private List<int> _p_Cantidad;

        public List<string> P_Productos { get => _p_Productos; set => _p_Productos = value; }
        public List<int> P_Cantidad { get => _p_Cantidad; set => _p_Cantidad = value; }
    }
}
