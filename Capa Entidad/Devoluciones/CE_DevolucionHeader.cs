using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Devoluciones
{
    public class CE_DevolucionHeader
    {
        private int _CodigoError;
        private string _MensajeError;
        private int _Id;
        private string _NumTck;
        private string _Fecha;
        private string _DocCur;
        private decimal _DocRate;
        private decimal _DocTotal;
        private decimal _DocTotalFC;
        private string _Status;

        public int CodigoError { get => _CodigoError; set => _CodigoError = value; }
        public string MensajeError { get => _MensajeError; set => _MensajeError = value; }
        public int Id { get => _Id; set => _Id = value; }
        public string NumTck { get => _NumTck; set => _NumTck = value; }
        public string Fecha { get => _Fecha; set => _Fecha = value; }
        public string DocCur { get => _DocCur; set => _DocCur = value; }
        public decimal DocRate { get => _DocRate; set => _DocRate = value; }
        public decimal DocTotal { get => _DocTotal; set => _DocTotal = value; }
        public decimal DocTotalFC { get => _DocTotalFC; set => _DocTotalFC = value; }
        public string Status { get => _Status; set => _Status = value; }
    }
}
