using MySql.Data.MySqlClient;
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
    /// Lógica de interacción para wpfTipousuario.xaml
    /// </summary>

    public partial class wpfTipousuario : UserControl
    {
        Conexion cn = new Conexion();
        public wpfTipousuario()
        {
            InitializeComponent();
            Cargartabla();
            txtIdtipousuario.Focus();
        }

        //Tabla 
        void Cargartabla()
        {
            try
            {
                string cadena = "SELECT * FROM CLINICA1.TIPO_USUARIO";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataAdapter dataAdp = new OdbcDataAdapter(consulta);
                DataTable dt = new DataTable("CLINICA1.TIPO_USUARIO");

                dataAdp.Fill(dt);
                dgTipoUsuarios.ItemsSource = dt.DefaultView;

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
            if (txtIdtipousuario.Text != "" || txtNombretipousuario.Text != "")
            {
                try
                {
                    string cadena = "update CLINICA1.TIPO_USUARIO set id_tipo_usuario ='" + this.txtIdtipousuario.Text
                    + "',nombre_tipo_usuario ='" + this.txtNombretipousuario.Text

                    + "'where id_tipo_usuario='" + this.txtIdtipousuario.Text + "';";

                    OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                    consulta.ExecuteNonQuery();

                    MessageBox.Show("Modificacion realizada");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Cargartabla();
                txtIdtipousuario.Text = "";
                txtNombretipousuario.Text = "";

                txtIdtipousuario.IsEnabled = true;
                btnInsertar.IsEnabled = true;
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Faltan datos.");
                txtIdtipousuario.Focus();
            }


        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                try
                {
                    string Query = "select * from CLINICA1.TIPO_USUARIO where id_tipo_usuario='" + this.txtBuscar.Text + "';";

                    OdbcCommand consulta = new OdbcCommand(Query, cn.conexion());
                    consulta.ExecuteNonQuery();

                    OdbcDataReader busqueda;
                    busqueda = consulta.ExecuteReader();

                    if (busqueda.Read())
                    {
                        txtIdtipousuario.Text = busqueda["id_tipo_usuario"].ToString();
                        txtNombretipousuario.Text = busqueda["nombre_tipo_usuario"].ToString();

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
               
                txtIdtipousuario.IsEnabled = false;
                btnInsertar.IsEnabled = false;
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Ingrese dato a buscar");
            }
        }
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdtipousuario.Text != "" || txtNombretipousuario.Text !="")
            {
                try
                {
                    string cadena = "INSERT INTO" +
                    " CLINICA1.TIPO_USUARIO (id_tipo_usuario, nombre_tipo_usuario) VALUES (" +
                    "'" + txtIdtipousuario.Text + "', '"
                    + txtNombretipousuario.Text + "' ); ";

                    OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());

                    consulta.ExecuteNonQuery();
                    MessageBox.Show("Inserción realizada");

                    Cargartabla();
                    txtIdtipousuario.Text = "";
                    txtNombretipousuario.Text = "";
                    btnModificar.IsEnabled = false;
                    btnEliminar.IsEnabled = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Faltan datos.");
                txtIdtipousuario.Focus();
            }
                        
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtBuscar.Text = "";
            txtIdtipousuario.Text = "";
            txtNombretipousuario.Text = "";
            txtIdtipousuario.IsEnabled = true;
            btnInsertar.IsEnabled = true;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cadena = "delete from CLINICA1.TIPO_USUARIO where id_tipo_usuario='" + this.txtIdtipousuario.Text + "';";

                OdbcCommand consulta = new OdbcCommand(cadena, cn.conexion());
                consulta.ExecuteNonQuery();

                OdbcDataReader busqueda;
                busqueda = consulta.ExecuteReader();

                MessageBox.Show("Datos Eliminados");
                while (busqueda.Read())
                {
                }
                txtIdtipousuario.Text = "";
                txtNombretipousuario.Text = "";
                Cargartabla();
                txtIdtipousuario.IsEnabled = true;
                btnInsertar.IsEnabled = true;
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Funcion tecla enter
        private void txtIdtipousuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                txtNombretipousuario.Focus();
            }
        }
        private void txtNombretipousuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                if (btnInsertar.IsEnabled == true || btnModificar.IsEnabled == false )
                {
                    btnInsertar_Click(sender, e);//llama al evento click del boton
                }
                else
                {
                    if(btnModificar.IsEnabled== true || btnInsertar.IsEnabled == false)
                    {
                        btnModificar_Click(sender, e);//llama al evento click del boton
                    }
                }                
            }
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;//elimina el sonido
                btnBuscar_Click(sender, e);//llama al evento click del boton
            }
        }
    }
}
