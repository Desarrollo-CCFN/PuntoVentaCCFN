using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Devoluciones
{
    public class CE_DevolucionDetalle
    {
        private int _CodigoError;
        private string _MensajeExito;
        private string _ItemCode;
        private string _Unidad;
        private int _LineNum;
        private string _LineStatus;
        private string _Dscription;
        private decimal _Quantity;
        private string _Currency;
        private decimal _Rate;
        private decimal _LineTotal;
        private decimal _TotalFrgn;

        public int CodigoError { get => _CodigoError; set => _CodigoError = value; }
        public string MensajeExito { get => _MensajeExito; set => _MensajeExito = value; }
        public string ItemCode { get => _ItemCode; set => _ItemCode = value; }
        public string Unidad { get => _Unidad; set => _Unidad = value; }
        public int LineNum { get => _LineNum; set => _LineNum = value; }
        public string LineStatus { get => _LineStatus; set => _LineStatus = value; }
        public string Dscription { get => _Dscription; set => _Dscription = value; }
        public decimal Quantity { get => _Quantity; set => _Quantity = value; }
        public string Currency { get => _Currency; set => _Currency = value; }
        public decimal Rate { get => _Rate; set => _Rate = value; }
        public decimal LineTotal { get => _LineTotal; set => _LineTotal = value; }
        public decimal TotalFrgn { get => _TotalFrgn; set => _TotalFrgn = value; }
    }
}
