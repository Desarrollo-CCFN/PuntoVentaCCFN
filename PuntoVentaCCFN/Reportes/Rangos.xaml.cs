using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion.Reportes
{
    /// <summary>
    /// Lógica de interacción para Rangos.xaml
    /// </summary>
    public partial class Rangos : Window
    {
        private int valueTo_;
        public Rangos(int Value)
        {
            valueTo_ = Value;
            InitializeComponent();

            switch (valueTo_)
            {
                case 1:
                 //   Label1.Content = "Número de TCK Venta:";
                    this.Title = "Rangos de Fecha Concentrado de Ventas";
                    break;
                case 2:
                 //   Label1.Content = "Número de Retiro:";
                    this.Title = "Rangos de Fecha Detallado de Ventas";
                    break;
            }

            }
    }
}
