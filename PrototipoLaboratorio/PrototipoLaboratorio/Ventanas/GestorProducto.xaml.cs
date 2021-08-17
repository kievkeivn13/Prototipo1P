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
    /// Lógica de interacción para GestorProducto.xaml
    /// </summary>
    public partial class GestorProducto : UserControl
    {
        Conexion cn = new Conexion();
        public GestorProducto()
        {
            InitializeComponent();
            CargarCbo();
            Cargartabla();
            txtIdProducto.Focus();
        }

        //RadioButton
        void rbnestado()
        {
            if (txtEstadousuario.Text == "1")
            {
                rbnActivo.IsChecked = true;
                rbnSuspendido.IsChecked = false;
            }
            else
            {
                if (txtEstadousuario.Text == "0")
                {
                    rbnActivo.IsChecked = false;
                    rbnSuspendido.IsChecked = true;
                }
            }
        }

        //Cargar Combobox
        private void CargarCbo()
        {

            try
            {
                string cadena = "SELECT nombre_producto FROM BODEGAS_AGRICOLAS.PRODUCTO";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataReader busqueda;
                busqueda = consulta.ExecuteReader();

                cboTipoBodega.Items.Clear();
                cboTipoBodega.Items.Add("Selecione una opción");
                while (busqueda.Read())
                {
                    cboTipoBodega.Items.Add(busqueda["nombre_producto"].ToString());
                }
                cboTipoBodega.SelectedIndex = 0;
                busqueda.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //Cargar Tabla
        void Cargartabla()
        {
            try
            {
                string cadena = "SELECT * FROM BODEGAS_AGRICOLAS.PRODUCTO";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataAdapter dataAdp = new OdbcDataAdapter(consulta);
                DataTable dt = new DataTable("BODEGAS_AGRICOLAS.PRODUCTO");

                dataAdp.Fill(dt);
                dgUsuarios.ItemsSource = dt.DefaultView;

                dataAdp.Update(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Funcion de Botones
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdBodega.Text != "" || txtEstadousuario.Text != "" || txtIdProducto.Text != "" || txtNombreProducto.Text != "" || txtPrecio.Text != "" || txtStock.Text !="")
            {
                try
                {
                    string cadena = "INSERT INTO " +
                        " BODEGAS_AGRICOLAS.PRODUCTO (id_producto, id_bodega, nombre_producto," +
                        " precio, stock, status_producto) VALUES (" +
                        "'" + txtIdProducto.Text + "', '"
                         + txtIdBodega.Text + "', '"
                         + txtNombreProducto.Text + "', '"
                         + txtPrecio.Text + "', '"
                         + txtStock.Text + "', '"
                         + txtEstadousuario.Text + "' ); ";

                    OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());

                    consulta.ExecuteNonQuery();
                    MessageBox.Show("Inserción realizada");

                    txtIdProducto.Text = "";
                    txtIdBodega.Text = "";
                    txtNombreProducto.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    txtEstadousuario.Text = "";
                    txtBuscar.Text = "";
                    Cargartabla();
                    CargarCbo();
                    btnModificar.IsEnabled = false;
                    btnEliminar.IsEnabled = false;
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
                txtIdProducto.Focus();
            }
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdBodega.Text != "" || txtEstadousuario.Text != "" || txtIdProducto.Text != "" || txtNombreProducto.Text != "" || txtPrecio.Text != "" || txtStock.Text != "")
            {
                try
                {
                    string cadena = "update BODEGAS_AGRICOLAS.PRODUCTO set id_producto ='" + this.txtIdProducto.Text
                        + "',id_bodega ='" + this.txtIdBodega.Text
                        + "',nombre_precio   ='" + this.txtNombreProducto.Text
                        + "',precio='" + this.txtPrecio.Text
                        + "stock ='" + this.txtStock
                        + "',status_producto ='" + this.txtEstadousuario.Text

                        + "'where id_producto='" + this.txtIdProducto.Text + "';";

                    OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                    consulta.ExecuteNonQuery();

                    MessageBox.Show("Modificacion realizada");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                txtIdProducto.Text = "";
                txtIdBodega.Text = "";
                txtNombreProducto.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
                txtEstadousuario.Text = "";
                txtBuscar.Text = "";
                Cargartabla();
                CargarCbo();
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                rbnSuspendido.IsChecked = false;
                rbnActivo.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Faltan datos.");
                cboTipoBodega.Focus();
            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

            txtIdProducto.Text = "";
            txtIdBodega.Text = "";
            txtNombreProducto.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtEstadousuario.Text = "";
            txtBuscar.Text = "";
            CargarCbo();
            txtIdProducto.IsEnabled = true;
            btnInsertar.IsEnabled = true;
            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            Cargartabla();
            rbnSuspendido.IsChecked = false;
            rbnActivo.IsChecked = false;

        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cadena = "delete from BODEGAS_AGRICOLAS.PRODUCTO where id_producto='" + this.txtIdProducto.Text + "';";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataReader busqueda;
                busqueda = consulta.ExecuteReader();

                MessageBox.Show("Datos Eliminados");
                while (busqueda.Read())
                {
                }
                txtIdProducto.Text = "";
                txtIdBodega.Text = "";
                txtNombreProducto.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
                txtEstadousuario.Text = "";
                txtBuscar.Text = "";
                CargarCbo();
                Cargartabla();
                txtIdProducto.IsEnabled = true;
                btnInsertar.IsEnabled = true;
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
                    CargarCbo();
                    string Query = "select * from BODEGAS_AGRICOLAS.PRODUCTO where id_producto='" + this.txtBuscar.Text + "';";

                    OdbcCommand consulta = new OdbcCommand(Query, cn.conexion());
                    consulta.ExecuteNonQuery();

                    OdbcDataReader busqueda;
                    busqueda = consulta.ExecuteReader();

                    if (busqueda.Read())
                    {
                        txtIdProducto.Text = busqueda["id_producto"].ToString();
                        txtIdBodega.Text = busqueda["id_bodega"].ToString();
                        txtNombreProducto.Text = busqueda["nombre_Producto"].ToString();
                        txtPrecio.Text = busqueda["precio"].ToString();
                        txtStock.Text = busqueda["strock"].ToString();
                        txtEstadousuario.Text = busqueda["status_producto"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Registro no encontrado");
                    }
                    try
                    {
                        string Query2 = "select * from BODEGAS_AGRICOLAS.BODEGA where id_bodega='" + this.txtIdBodega.Text.Trim() + "';";

                        OdbcCommand consulta2 = new OdbcCommand(Query2, cn.conexion());
                        consulta2.ExecuteNonQuery();

                        OdbcDataReader busqueda2;
                        busqueda2 = consulta2.ExecuteReader();

                        if (busqueda2.Read())
                        {
                            cboTipoBodega.Items.Add(busqueda2["nombre_tipo_usuario"].ToString());
                        }
                        int ultimo = cboTipoBodega.Items.Count - 1;
                        cboTipoBodega.SelectedIndex = ultimo;  //<-- con esto lo dejas seleccionado
                        rbnestado();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    txtBuscar.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                txtIdProducto.IsEnabled = false;
                btnInsertar.IsEnabled = false;
                cboTipoBodega.Focus();
            }
            else
            {
                MessageBox.Show("Ingrese dato a buscar");
            }

        }

        //Seleccion en Combobox
        private void cboTipousuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string cadena = "SELECT id_bodega FROM BODEGAS_AGRICOLAS.BODEGA where nombre_bodega='" + this.cboTipoBodega.SelectedItem.ToString() + "';";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataReader busqueda;
                busqueda = consulta.ExecuteReader();

                while (busqueda.Read())
                {
                    txtIdBodega.Text = busqueda["id_bodega"].ToString();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        //Funcion de Radiobutton
        private void rbnActivo_Checked(object sender, RoutedEventArgs e)
        {
            rbnActivo.IsChecked = true;
            rbnSuspendido.IsChecked = false;
            txtEstadousuario.Text = "1";
        }
        private void rbnSuspensido_Checked(object sender, RoutedEventArgs e)
        {
            rbnSuspendido.IsChecked = true;
            rbnActivo.IsChecked = false;
            txtEstadousuario.Text = "0";
        }

        //Funcion tecla enter
       
       
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

        private void txtStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                rbnActivo.Focus();
            }
            
        }

        private void txtIdProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                cboTipoBodega.Focus();
            }
                        
        }

        private void cboTipoBodega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                txtNombreProducto.Focus();
            }
                        
        }

        private void txtNombreProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                 txtPrecio.Focus();
            }
                     
        }

       

        private void txtStock_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                txtStock.Focus();
            }
                       
        }

        private void txtBuscar_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                btnBuscar_Click(sender, e);//llama al evento click del boton
            }
        }
    }
}
