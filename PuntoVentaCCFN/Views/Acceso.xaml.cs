using Capa_Entidad;
using Capa_Negocio;
using System.Data;
using Capa_Presentacion.SCS.Boxes;
using MySqlConnector;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
 

namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para Acceso.xaml
    /// </summary>
    public partial class Acceso : Window
    {
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CE_Usuarios objeto_CE_Usuarios = new CE_Usuarios();
        public int ReturnValue { get; set; } // Propiedad para devolver el valor
        public int SupervisorId { get; set; } // Propiedad para devolver el valor del supervisor

      //  private MySqlConnectionStringBuilder objBuidel = new MySqlConnectionStringBuilder();
        private int valueToSend_;
          public Acceso(int valueToSend)
        {
            valueToSend_ = valueToSend;
            ReturnValue = 0; // Valor por defecto
            //    System.Windows.MessageBox.Show("Password para Acceso: " + valueToSend_, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

            InitializeComponent();
            txtUsuario.Focus();

        }
       
        

            private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
           
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                // Asignar los valores de la interfaz a la entidad
                objeto_CE_Usuarios.U_NAME = this.txtUsuario.Text;
                objeto_CE_Usuarios.PASSWORD4 = this.txtPassword.Password;

                // Llamar al método de negocio para autenticar el usuario
                DataTable dtResultado = objeto_CN_Usuarios.AutUsuario(this.txtUsuario.Text, this.txtPassword.Password);

                Mouse.OverrideCursor = null;

                if (dtResultado.Rows.Count > 0)
                {
                    string superUserFlag = dtResultado.Rows[0].ItemArray[5]?.ToString();
                    string UserCod       = dtResultado.Rows[0].ItemArray[2]?.ToString();
                    string UserId        = dtResultado.Rows[0].ItemArray[0]?.ToString();

                     SupervisorId = Convert.ToInt32(UserId);

                    if (superUserFlag == "Y" || superUserFlag == "N" )
                    {


                        if (superUserFlag == "Y" && valueToSend_ == 1)
                        {
                            
                            var configuracionWindow = new Capa_Presentacion.SCS.Boxes.configuracionApp();
                            this.Close();
                            configuracionWindow.Show();
                        

                    }
                    else if ((superUserFlag == "Y" || UserCod == "SUPERVISOR") && valueToSend_ == 2)     // cuando pide autorizacion para anular caja
                    {
                        ReturnValue = 1;     // regresa 1 que puede proseguir con acceso         PARA PODER ABRIR DEVOLUCION
                        this.Close();                   // EN LA PANTALLA ORIGEN

                    }
                    else if ((superUserFlag == "Y" || UserCod == "SUPERVISOR") && valueToSend_ == 3)   // cuando pide autorizacion para rendicion de caja
                    {
                        ReturnValue = int.Parse(UserId);     // regresa 1 que puede proseguir con acceso         PARA PODER ABRIR DEVOLUCION
                        this.Close();                   // EN LA PANTALLA ORIGEN

                    }
                    else if ((superUserFlag == "Y" || UserCod == "SUPERVISOR") && valueToSend_ == 4)   // cuando pide autorizacion para rendicion de caja
                    {
                        ReturnValue = 1;     // regresa 1 que puede proseguir con acceso         PARA PODER ABRIR DEVOLUCION
                        this.Close();                   // EN LA PANTALLA ORIGEN

                    }

                    else
                        {
                               System.Windows.MessageBox.Show("No esta validado esta funcion (Anexar)", "No se tiene Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                                ReturnValue = 0;

                    }



                           // this.Close();
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("No se tiene registro Favor de Verificar Usuario y/o Password.", "No se tiene Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtUsuario.Clear();
                    txtPassword.Clear();
                    txtUsuario.Focus();


                }

        }

        private void txtUsuario_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {           

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                txtPassword.Focus();
            }
             
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab) // Verificar si la tecla presionada es Enter
            {
                BtnLogin_Click(sender, e); // Llamar al método btnLogin_Click con los argumentos correctos
            }
              
        }
    }
}
