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
using MySqlConnector;
using PrototipoLaboratorio;


namespace PrototipoLaboratorio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para wpfGestorusuario.xaml
    /// </summary>
    public partial class wpfGestorusuario : UserControl
    {
        Conexion cn = new Conexion();
        public wpfGestorusuario()
        {
            InitializeComponent();
            CargarCbo();
            Cargartabla();
            txtIdusuario.Focus();
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
        private void CargarCbo() {

            try
            {
                string cadena = "SELECT nombre_tipo_usuario FROM BODEGAS_AGRICOLAS.TIPO_USUARIO";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataReader busqueda;
                busqueda = consulta.ExecuteReader();

                cboTipousuario.Items.Clear();
                cboTipousuario.Items.Add("Selecione una opción");
                while (busqueda.Read())
                {
                    cboTipousuario.Items.Add(busqueda["nombre_tipo_usuario"].ToString());
                }
                cboTipousuario.SelectedIndex = 0;
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
                string cadena = "SELECT * FROM BODEGAS_AGRICOLAS.USUARIOS";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataAdapter dataAdp = new OdbcDataAdapter(consulta);
                DataTable dt = new DataTable("BODEGAS_AGRICOLAS.USUARIOS");

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
            if (txtIdtipousuario.Text != "" || txtEstadousuario.Text != "" || txtIdusuario.Text != "" || txtNombreusuario.Text != "" || txtContraseñausuario.Password != "")
            {
                try
                {
                    string cadena = "INSERT INTO " +
                        " BODEGAS_AGRICOLAS.USUARIOS (id_usuario, id_tipo_usuario, nombre_usuario," +
                        " passwd_usuario, estado_usuario) VALUES (" +
                        "'" + txtIdusuario.Text + "', '"
                         + txtIdtipousuario.Text + "', '"
                         + txtNombreusuario.Text + "', '"
                         + txtContraseñausuario.Password + "', '"
                         + txtEstadousuario.Text + "' ); ";

                    OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());

                    consulta.ExecuteNonQuery();
                    MessageBox.Show("Inserción realizada");

                    txtIdusuario.Text = "";
                    txtIdtipousuario.Text = "";
                    txtNombreusuario.Text = "";
                    txtContraseñausuario.Password = "";
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
                txtIdusuario.Focus();
            }
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdtipousuario.Text != "" || txtEstadousuario.Text != "" || txtIdusuario.Text != "" || txtNombreusuario.Text != "" || txtContraseñausuario.Password != "")
            {
                try
                {
                    string cadena = "update BODEGAS_AGRICOLAS.USUARIOS set id_usuario ='" + this.txtIdusuario.Text
                        + "',id_tipo_usuario ='" + this.txtIdtipousuario.Text
                        + "',nombre_usuario ='" + this.txtNombreusuario.Text
                        + "',passwd_usuario ='" + this.txtContraseñausuario.Password
                        + "',estado_usuario ='" + this.txtEstadousuario.Text

                        + "'where id_usuario='" + this.txtIdusuario.Text + "';";

                    OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                    consulta.ExecuteNonQuery();

                    MessageBox.Show("Modificacion realizada");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                txtIdusuario.Text = "";
                txtIdtipousuario.Text = "";
                txtNombreusuario.Text = "";
                txtContraseñausuario.Password = "";
                txtEstadousuario.Text = "";
                Cargartabla();
                CargarCbo();
                txtIdtipousuario.IsEnabled = true;
                btnInsertar.IsEnabled = true;
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                rbnSuspendido.IsChecked = false;
                rbnActivo.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Faltan datos.");
                txtIdusuario.Focus();
            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

            txtBuscar.Text = "";
            txtIdusuario.Text = "";
            txtIdtipousuario.Text = "";
            txtNombreusuario.Text = "";
            txtContraseñausuario.Password = "";
            txtEstadousuario.Text = "";
            CargarCbo();
            txtIdtipousuario.IsEnabled = true;
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
                string cadena = "delete from BODEGAS_AGRICOLAS.USUARIOS where id_usuario='" + this.txtIdusuario.Text + "';";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataReader busqueda;
                busqueda = consulta.ExecuteReader();

                MessageBox.Show("Datos Eliminados");
                while (busqueda.Read())
                {
                }
                txtIdusuario.Text = "";
                txtIdtipousuario.Text = "";
                txtNombreusuario.Text = "";
                txtContraseñausuario.Password = "";
                txtEstadousuario.Text = "";
                CargarCbo();
                Cargartabla();
                txtIdtipousuario.IsEnabled = true;
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
                    string Query = "select * from BODEGAS_AGRICOLAS.USUARIOS where id_usuario='" + this.txtBuscar.Text + "';";

                    OdbcCommand consulta = new OdbcCommand(Query, cn.conexion());
                    consulta.ExecuteNonQuery();

                    OdbcDataReader busqueda;
                    busqueda = consulta.ExecuteReader();

                    if (busqueda.Read())
                    {
                        txtIdusuario.Text = busqueda["id_usuario"].ToString();
                        txtIdtipousuario.Text= busqueda["id_tipo_usuario"].ToString();
                        txtNombreusuario.Text = busqueda["nombre_usuario"].ToString();
                        txtContraseñausuario.Password = busqueda["passwd_usuario"].ToString();
                        txtEstadousuario.Text = busqueda["estado_usuario"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Registro no encontrado");
                    }
                    try
                    {
                        string Query2 = "select * from BODEGAS_AGRICOLAS.TIPO_USUARIO where id_tipo_usuario='" + this.txtIdtipousuario.Text.Trim() + "';";

                        OdbcCommand consulta2 = new OdbcCommand(Query2, cn.conexion());
                        consulta2.ExecuteNonQuery();

                        OdbcDataReader busqueda2;
                        busqueda2 = consulta2.ExecuteReader();

                        if (busqueda2.Read())
                        {
                            cboTipousuario.Items.Add(busqueda2["nombre_tipo_usuario"].ToString());
                        }
                        int ultimo = cboTipousuario.Items.Count - 1;
                        cboTipousuario.SelectedIndex = ultimo;  //<-- con esto lo dejas seleccionado
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
                txtIdtipousuario.IsEnabled = false;
                btnInsertar.IsEnabled = false;
                cboTipousuario.Focus();
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
                string cadena = "SELECT id_tipo_usuario FROM BODEGAS_AGRICOLAS.TIPO_USUARIO where nombre_tipo_usuario='" + this.cboTipousuario.SelectedItem.ToString() + "';";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataReader busqueda;
                busqueda = consulta.ExecuteReader();

                while (busqueda.Read())
                {
                    txtIdtipousuario.Text = busqueda["id_tipo_usuario"].ToString();
                }
            }
            catch(Exception ex)
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
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                btnBuscar_Click(sender, e);//llama al evento click del boton
            }
        }
        private void txtIdusuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                cboTipousuario.Focus();
            }
        }
        private void cboTipousuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                txtNombreusuario.Focus();
            }
        }
        private void txtNombreusuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                txtContraseñausuario.Focus();
            }
        }
        private void txtContraseñausuario_KeyDown(object sender, KeyEventArgs e)
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

                if(btnInsertar.IsEnabled == true || btnModificar.IsEnabled == false)
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
