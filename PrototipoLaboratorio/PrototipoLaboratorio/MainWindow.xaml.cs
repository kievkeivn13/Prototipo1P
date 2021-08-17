//using PrototipoLaboratorio.Ventanas;
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

namespace PrototipoLaboratorio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnGestionusuario_Click(object sender, RoutedEventArgs e)
        {
            funGestorventas(new Ventanas.wpfGestorusuario());
        }
        private void btnTipousuario_Click(object sender, RoutedEventArgs e)
        {
            funGestorventas(new Ventanas.wpfTipousuario());
        }
        private void btnGestorProducto_Click(object sender, RoutedEventArgs e)
        {
            funGestorventas(new Ventanas.GestorProducto()); ;
        }
        private void btnGestorProveedor_Click(object sender, RoutedEventArgs e)
        {
            funGestorventas(new Ventanas.wpfGestorProveedor()); ;
        }
        private void btnTipoInventario_Click(object sender, RoutedEventArgs e)
        {
            funGestorventas(new Ventanas.wpfTipoInventario()); ;
        }
        private void btnGestorBodega_Click(object sender, RoutedEventArgs e)
        {
            funGestorventas(new Ventanas.wpfGestorBodega()); ;
        }
        private void funGestorventas(UserControl control)
        {
            this.pnlVentanas.Children.Clear();
            this.pnlVentanas.Children.Add(control);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.pnlVentanas.Children.Clear();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            loginscreen dashboard = new loginscreen();
            dashboard.Show();
            this.Close();
        }
    }

}
