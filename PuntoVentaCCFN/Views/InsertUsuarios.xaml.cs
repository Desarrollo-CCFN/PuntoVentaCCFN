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
    /// Lógica de interacción para InsertUsuarios.xaml
    /// </summary>
    public partial class InsertUsuarios : Page
    {
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CE_Usuarios objeto_CE_Usuarios = new CE_Usuarios();
        public InsertUsuarios()
        {
            InitializeComponent();
            cbPrivilegio.Items.Add("Y");
            cbPrivilegio.Items.Add("N");

            cbSuper.Items.Add("Y");
            cbSuper.Items.Add("N");

            cbCode.Items.Add("SISTEMAS");
            cbCode.Items.Add("SUPERVISOR");
            cbCode.Items.Add("CAJERA");



            tbUsuario.Focus();
            
        }

        public void Alta()
        {
            /*
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
            */
            //   a.SUPERUSER.ToString();
            // a.Locked.ToString();
 
        }

        private void Alta_Click(object sender, RoutedEventArgs e)
        {
            int ErrorCode;
            // Verifica que los campos obligatorios no estén vacíos
            if (string.IsNullOrWhiteSpace(tbUsuario.Text) ||
                string.IsNullOrWhiteSpace(cbCode.Text) ||
                string.IsNullOrWhiteSpace(tbSucursal.Text) ||
                string.IsNullOrWhiteSpace(cbPrivilegio.Text) ||
                string.IsNullOrWhiteSpace(cbSuper.Text) ||
                string.IsNullOrWhiteSpace(tbPassword.Password) ||
                string.IsNullOrWhiteSpace(tbPassword_Copiar.Password))
            {
                System.Windows.MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificar que las contraseñas coincidan
            if (tbPassword.Password != tbPassword_Copiar.Password)
            {
                System.Windows.MessageBox.Show("Las contraseñas no coinciden.");
                tbPassword.Clear();
                tbPassword_Copiar.Clear();
                tbPassword.Focus();
                return;
            }

            // Asignar los valores a la entidad
            objeto_CE_Usuarios.U_NAME = tbUsuario.Text;
            objeto_CE_Usuarios.USER_CODE = cbCode.Text;
            objeto_CE_Usuarios.DfltsGroup = tbSucursal.Text;
            objeto_CE_Usuarios.Locked = cbPrivilegio.Text;
            objeto_CE_Usuarios.SUPERUSER = cbSuper.Text;
            objeto_CE_Usuarios.PASSWORD4 = tbPassword_Copiar.Password;

            // Llamar al método para guardar los datos
            ErrorCode = objeto_CN_Usuarios.Alta(objeto_CE_Usuarios);


            if (ErrorCode == 0)
            {

                // Mostrar mensaje de éxito
                System.Windows.MessageBox.Show("Alta de usuario exitoso.", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

                // Redirigir o realizar otra acción
                Content = new Usuarios();
            }
            else if (ErrorCode == 1) 
            {
                // Mostrar mensaje de éxito
                System.Windows.MessageBox.Show("El usuario: "+tbUsuario.Text + " EXISTE debe cambiar el Nombre", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
            } 
            else if(ErrorCode == 2) 
            {
                // Mostrar mensaje de éxito
                System.Windows.MessageBox.Show("No se puede ingresar Intente nuevamente", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

            }

           

        }



        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
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
               //     System.Windows.MessageBox.Show("Modificación de Contraseña Exitosa.");
                    // Si son iguales, continuar  
                    tbSucursal.Focus();
                }
            }


        }

         
    }
}
