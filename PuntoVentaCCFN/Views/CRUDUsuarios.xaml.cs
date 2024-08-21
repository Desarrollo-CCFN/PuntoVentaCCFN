
using System;
using System.Collections.Generic;
using System.Configuration;
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
using Capa_Entidad;
using Capa_Negocio;

namespace PuntoVentaCCFN.Views
{
    /// <summary>
    /// Lógica de interacción para CRUDUsuarios.xaml
    /// </summary>
    public partial class CRUDUsuarios : Page
    {

        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CE_Usuarios objeto_CE_Usuarios = new CE_Usuarios();

        public int IdUsuario;

        public CRUDUsuarios()
        {
            InitializeComponent();
        }

        public void Consultar()
        {
            var a = objeto_CN_Usuarios.Consulta(IdUsuario);
            tbUsuario.Text = a.U_NAME.ToString();
            tbSucursal.Text = a.DfltsGroup.ToString();
            tbPrivilegio.Text = a.Locked.ToString();
            tbPassword.Password = "12345678";  //a.PASSWORD1.ToString();

            tbSuperUser.Text = a.SUPERUSER.ToString();
            tbId.Text = a.USERID.ToString();
            tbUserCode.Text = a.USER_CODE.ToString();




        }


        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }
    }
}
