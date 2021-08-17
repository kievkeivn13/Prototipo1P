using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrototipoLaboratorio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para wpfGestorBodega.xaml
    /// </summary>
    public partial class wpfGestorBodega : UserControl
    {
        Conexion cn = new Conexion();
        public wpfGestorBodega()
        {
            InitializeComponent();
            Cargartabla();

        }

        //RadioButton
        void rbnestado()
        {
            if (txtEstadomoneda.Text == "1")
            {
                rbnActivo.IsChecked = true;
                rbnSuspendido.IsChecked = false;
            }
            else
            {
                if (txtEstadomoneda.Text == "0")
                {
                    rbnActivo.IsChecked = false;
                    rbnSuspendido.IsChecked = true;
                }
            }
        }

        //Funcion Tabla
        void Cargartabla()
        {
            try
            {
                string cadena = "SELECT * FROM BODEGAS_AGRICOLAS.BODEGA";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataAdapter dataAdp = new OdbcDataAdapter(consulta);
                DataTable dt = new DataTable("BODEGAS_AGRICOLAS.BODEGA");

                dataAdp.Fill(dt);
                dgTipocambio.ItemsSource = dt.DefaultView;

                dataAdp.Update(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Funcion de Botones
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdBodega.Text != "" || txtNombreBodega.Text != "" || txtEstadomoneda.Text != "")
            {
                try
                {
                    string cadena = "update BODEGAS_AGRICOLAS.BODEGA set id_bodega ='" + this.txtIdBodega.Text
                        + "',nombre_bodega ='" + this.txtNombreBodega.Text
                       
                        + "',status_bodega ='" + this.txtEstadomoneda.Text

                        + "'where id_bodega='" + this.txtIdBodega.Text + "';";

                    OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                    consulta.ExecuteNonQuery();

                    MessageBox.Show("Modificacion realizada");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                txtIdBodega.Text = "";
                txtNombreBodega.Text = "";
                Cargartabla();
                txtEstadomoneda.Text = "";
                txtBuscar.Text = "";

                txtIdBodega.IsEnabled = true;
                btnInsertar.IsEnabled = true;
                btnEliminar.IsEnabled = false;
                btnModificar.IsEnabled = false;
                rbnSuspendido.IsChecked = false;
                rbnActivo.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Faltan datos.");
                txtIdBodega.Focus();
            }
        }
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdBodega.Text != "" || txtNombreBodega.Text != "" || txtEstadomoneda.Text != "")
            {
                try
                {
                    string cadena = "INSERT INTO" +
                        " BODEGAS_AGRICOLAS.BODEGA (id_bodega, nombre_bodega, status_bodega) VALUES (" +
                        "'" + txtIdBodega.Text + "', '"
                            + txtNombreBodega.Text + "', '"
                           
                            + txtEstadomoneda.Text + "' ); ";

                    OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());

                    consulta.ExecuteNonQuery();
                    MessageBox.Show("Inserción realizada");


                    txtIdBodega.Text = "";
                    txtNombreBodega.Text = "";
                    Cargartabla();
                    txtEstadomoneda.Text = "";
                    txtBuscar.Text = "";
                    txtIdBodega.IsEnabled = true;
                    btnInsertar.IsEnabled = true;
                    btnEliminar.IsEnabled = false;
                    btnModificar.IsEnabled = false;
                    rbnSuspendido.IsChecked = false;
                    rbnActivo.IsChecked = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Faltan datos.");
                txtIdBodega.Focus();
            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdBodega.Text = "";
            txtNombreBodega.Text = "";
            
            txtEstadomoneda.Text = "";
            txtBuscar.Text = "";
            txtIdBodega.IsEnabled = true;
            btnInsertar.IsEnabled = true;
            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            rbnSuspendido.IsChecked = false;
            rbnActivo.IsChecked = false;

        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string cadena = "delete from BODEGAS_AGRICOLAS.BODEGA where id_bodega='" + this.txtIdBodega.Text + "';";


                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataReader busqueda;
                busqueda = consulta.ExecuteReader();

                MessageBox.Show("Datos Eliminados");
                while (busqueda.Read())
                {
                }
                txtIdBodega.Text = "";
                txtNombreBodega.Text = "";
                Cargartabla();
                txtEstadomoneda.Text = "";
                txtBuscar.Text = "";
                txtIdBodega.IsEnabled = true;
                btnInsertar.IsEnabled = true;
                btnEliminar.IsEnabled = false;
                btnModificar.IsEnabled = false;
                rbnSuspendido.IsChecked = false;
                rbnActivo.IsChecked = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                try
                {
                    string Query = "select * from  BODEGAS_AGRICOLAS.BODEGA where id_bodega='" + this.txtBuscar.Text + "';";

                    OdbcCommand consulta = new OdbcCommand(Query, cn.conexion());
                    consulta.ExecuteNonQuery();

                    OdbcDataReader busqueda;
                    busqueda = consulta.ExecuteReader();

                    if (busqueda.Read())
                    {
                        txtIdBodega.Text = busqueda["id_bodega"].ToString();
                        txtNombreBodega.Text = busqueda["nombre_bodega"].ToString();
                        
                        txtEstadomoneda.Text = busqueda["status_bodega"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Registro no encontrado");
                    }

                    this.txtBuscar.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                rbnestado();
                txtIdBodega.IsEnabled = false;
                btnInsertar.IsEnabled = false;
                btnEliminar.IsEnabled = true;
                btnModificar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Ingrese dato a buscar");
            }
        }

        //Funcion de Radiobutton
        private void rbnActivo_Checked(object sender, RoutedEventArgs e)
        {
            rbnActivo.IsChecked = true;
            rbnSuspendido.IsChecked = false;
            txtEstadomoneda.Text = "1";
        }
        private void rbnSuspensido_Checked(object sender, RoutedEventArgs e)
        {
            {
                rbnSuspendido.IsChecked = true;
                rbnActivo.IsChecked = false;
                txtEstadomoneda.Text = "0";
            }
        }

        //Funcion tecla enter
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                btnBuscar_Click(sender, e);//llama al evento click del boton
            }
        }
        private void txtIdmoneda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                txtNombreBodega.Focus();
            }
        }
        private void txtNombremoneda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                rbnActivo.Focus();
            }
        }
        
        private void rbnActivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                rbnActivo_Checked(sender, e);

                if (btnInsertar.IsEnabled == true || btnModificar.IsEnabled == false)
                {
                    btnInsertar_Click(sender, e);//llama al evento click del boton
                }
                else
                {
                    if (btnModificar.IsEnabled == true || btnInsertar.IsEnabled == false)
                    {
                        btnModificar_Click(sender, e);//llama al evento click del boton
                    }
                }
            }
            else
            {
                if (e.Key == Key.Right || e.Key == Key.Left)
                {
                    e.Handled = true;//elimina el sonido
                    rbnSuspendido.Focus();
                }

            }
        }
        private void rbnSuspensido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                rbnSuspensido_Checked(sender, e);

                if (btnInsertar.IsEnabled == true || btnModificar.IsEnabled == false)
                {
                    btnInsertar_Click(sender, e);//llama al evento click del boton
                }
                else
                {
                    if (btnModificar.IsEnabled == true || btnInsertar.IsEnabled == false)
                    {
                        btnModificar_Click(sender, e);//llama al evento click del boton
                    }
                }
            }
            else
            {
                if (e.Key == Key.Left || e.Key == Key.Right)
                {
                    e.Handled = true;//elimina el sonido
                    rbnActivo.Focus();
                }
            }
        }
    }
}
