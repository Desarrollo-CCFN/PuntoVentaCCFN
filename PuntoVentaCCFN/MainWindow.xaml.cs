﻿using PuntoVentaCCFN.Views;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using Capa_Presentacion;
using Capa_Presentacion.SCS.Boxes;

namespace PuntoVentaCCFN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public MainWindow()
        {
            InitializeComponent();

            if (AppConfig.Sections["App_Preferences"] is null)
            {
                AppConfig.Sections.Add("App_Preferences", new App_Preferences());
                AppConfig.Save();
            }
        }

        private void TBShow(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 0.5;
        }

        private void TBHide(object sender, RoutedEventArgs e)
        {
            GridContent.Opacity = 1;
        }

        private void PreviewMouseLeftButtonDownBG(object sender, MouseButtonEventArgs e)
        {
            //BtnShowHide.IsChecked = false;
        }

        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Usuarios();
        }

        private void Pos_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new POS();
        }

       private void Productos_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Productos();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnCuenta_Click(object sender, RoutedEventArgs e)
        {
            var configuracion = new configuracionApp();
            configuracion.Show();
        }
    }
}