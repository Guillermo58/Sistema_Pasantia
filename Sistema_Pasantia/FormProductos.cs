using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sistema_Pasantia
{
    public partial class FormProductos : Form
    {
        SqlConnection conexion = new SqlConnection(
            @"Data Source=MSI\SQLEXPRESS;Initial Catalog=Practica_Pasante;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
        );

        int idProductoSeleccionado = 0;

        public FormProductos()
        {

            InitializeComponent();

        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        void CargarProductos()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT ID_Producto, Codigo, Nombre, Existencia, Estado, Proveedor FROM Producto",
                    conexion
                );

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.Columns["ID_Producto"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }


        private void Insertar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "INSERT INTO Producto (Codigo, Nombre, Existencia, Estado, Proveedor) " +
                             "VALUES (@Codigo, @Nombre, @Existencia, @Estado, @Proveedor)";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Existencia", txtExistencia.Text);
                cmd.Parameters.AddWithValue("@Estado", txtEstado.Text);
                cmd.Parameters.AddWithValue("@Proveedor", txtProveedor.Text);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                CargarProductos();
                LimpiarCampos();

                MessageBox.Show("Producto insertado correctamente");
            }
            catch (Exception ex)
            {
                conexion.Close();
                MessageBox.Show("Error al insertar: " + ex.Message);
            }
        }


        private void Opciones_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridView1.SelectedRows[0];

                idProductoSeleccionado = Convert.ToInt32(fila.Cells["ID_Producto"].Value);

                txtCodigo.Text = fila.Cells["Codigo"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtExistencia.Text = fila.Cells["Existencia"].Value.ToString();
                txtEstado.Text = fila.Cells["Estado"].Value.ToString();
                txtProveedor.Text = fila.Cells["Proveedor"].Value.ToString();
            }
        }


        private void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "UPDATE Producto SET Nombre=@Nombre, " +
                             "Existencia=@Existencia, Estado=@Estado, Proveedor=@Proveedor " +
                             "WHERE Codigo=@Codigo";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Existencia", txtExistencia.Text);
                cmd.Parameters.AddWithValue("@Estado", txtEstado.Text);
                cmd.Parameters.AddWithValue("@Proveedor", txtProveedor.Text);
      

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                CargarProductos();
                LimpiarCampos();

                MessageBox.Show("Producto actualizado correctamente");
            }
            catch (Exception ex)
            {
                conexion.Close();
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este producto?", "Confirmar",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM Producto WHERE Codigo=@Codigo";

                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@Codigo", txtCodigo.Text);

                    conexion.Open();
                    int fila = cmd.ExecuteNonQuery();
                    conexion.Close();

                    CargarProductos();
                    LimpiarCampos();
                    if (fila > 0)
                        MessageBox.Show("Producto eliminado correctamente");
                    else
                        MessageBox.Show("No se encontro ese producto con ese codigo");
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        void LimpiarCampos()
        {
            idProductoSeleccionado = 0;
            txtCodigo.Clear();
            txtNombre.Clear();
            txtExistencia.Clear();
            txtEstado.Clear();
            txtProveedor.Clear();
            txtFiltroNombre.Clear();
            txtFiltroProveedor.Clear();
        }


        private void FormProductos_Load_1(object sender, EventArgs e)
        {
            
            this.productoTableAdapter.Fill(this.practica_PasanteDataSet.Producto);

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=Practica_Pasante;Integrated Security=True;TrustServerCertificate=True"))
                {
                    con.Open();
                    string query = "SELECT * FROM Producto WHERE Nombre LIKE @nombre " + "AND Estado = @estado " + "AND Proveedor LIKE @proveedor";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@nombre", "%" + txtFiltroNombre.Text + "%");
                    cmd.Parameters.AddWithValue("@estado", cmbEstado.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@proveedor", "%" + txtFiltroProveedor.Text + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}