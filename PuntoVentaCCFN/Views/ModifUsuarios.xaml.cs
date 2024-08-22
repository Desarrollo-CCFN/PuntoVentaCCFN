using Capa_Entidad;
using Capa_Negocio;
using PuntoVentaCCFN.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Capa_Presentacion.Views
{
    /// <summary>
    /// Lógica de interacción para ModifUsuarios.xaml
    /// </summary>
    public partial class ModifUsuarios : Page
    {

        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CE_Usuarios objeto_CE_Usuarios = new CE_Usuarios();
   

        public int IdUsuario;

        public ModifUsuarios()
        {
            InitializeComponent();
            tbUsuario.Focus();
        }

        public void Update()
        {

            cbPrivilegio.Items.Add("Y");
            cbPrivilegio.Items.Add("N");

            cbSuper.Items.Add("Y");
            cbSuper.Items.Add("N");

            var a = objeto_CN_Usuarios.Consulta(IdUsuario);
            tbUsuario.Text = a.U_NAME.ToString();
            tbId.Text = a.USERID.ToString();
            tbCode.Text = a.USER_CODE.ToString();
            tbSucursal.Text = a.DfltsGroup.ToString();
            cbPrivilegio.Text = a.Locked.ToString();
            cbSuper.Text = a.SUPERUSER.ToString();

           //   a.SUPERUSER.ToString();
           // a.Locked.ToString();
            
        }


        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {

            if (tbPassword.Password != tbPassword_Copiar.Password)
            {
                System.Windows.MessageBox.Show("La Contraseña se encuentra Inconclusa.");
                // Borrar los contenidos de los PasswordBox
                tbPassword.Clear();
                tbPassword_Copiar.Clear();

                // Regresar el foco a tbPassword
                tbPassword.Focus();
            }
            else
            {


                // Asignar los valores de la interfaz a la entidad
                objeto_CE_Usuarios.USERID = Convert.ToInt32(tbId.Text);
                objeto_CE_Usuarios.U_NAME = tbUsuario.Text;
                objeto_CE_Usuarios.USER_CODE = tbCode.Text;
                objeto_CE_Usuarios.DfltsGroup = tbSucursal.Text;
                objeto_CE_Usuarios.Locked = cbPrivilegio.Text;
                objeto_CE_Usuarios.SUPERUSER = cbSuper.Text;
                objeto_CE_Usuarios.PASSWORD4 = tbPassword_Copiar.Password;

                // Llamar al método de negocio para guardar los cambios
                objeto_CN_Usuarios.Actualizar(objeto_CE_Usuarios);

                // Mostrar un mensaje de confirmación
                System.Windows.MessageBox.Show("Los cambios han sido guardados exitosamente.", "Confirmación", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

                // Redirigir o realizar otra acción si es necesario
                Content = new Usuarios();
            }



        }

        private void tbPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                
                tbPassword_Copiar.Focus(); // Mueve el foco a tbPassword_Copiar
            }

        }

        private void tbPassword_Copiar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                // Comparar las contraseñas
                if (tbPassword.Password != tbPassword_Copiar.Password)
                {
                    System.Windows.MessageBox.Show("Las contraseñas no son iguales. Intente nuevamente.");

                    // Borrar los contenidos de los PasswordBox
                    tbPassword.Clear();
                    tbPassword_Copiar.Clear();

                    // Regresar el foco a tbPassword
                    tbPassword.Focus();
                }
                else
                { 
                    System.Windows.MessageBox.Show("Modificación de Contraseña Exitosa.");
                    // Si son iguales, continuar  
                    tbSucursal.Focus();  
                }
            }


        }
    }
}
