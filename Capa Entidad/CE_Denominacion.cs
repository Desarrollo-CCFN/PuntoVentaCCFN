

namespace Capa_Entidad
{
    public class CE_Denominacion
    {
 
        public int IdCash { get; set; }
        public string PayForm { get; set; }
        public int IdCDenom { get; set; }
        public int Quantity { get; set; }
        public decimal TotAmount { get; set; }
        public int Usuario { get; set; }

        public string Sucursal { get; set; }

        public string status { get; set; }
        public string Name { get; set; }
        public int StationId { get; set; }

        public string retiro { get; set; }

        public string DfltsGroup { get; set; }
        public string OpenDate { get; set; }

        public int StatusCaja { get; set; }

        public int INTERNAL_K { get; set; }

        public string AperturaP { get; set; }
        public string CierreP { get; set; }
        public string AperturaD { get; set; }
        public string CierreD { get; set; }

        public int Super { get; set; }

        public int Idtrans { get; set; }
        public string Descrip { get; set; }

        public decimal AmountValue { get; set; }

         

        public override string ToString()
        {
            return Descrip;     // regresa el valor que se desea mostrar  
        }



    }
}
