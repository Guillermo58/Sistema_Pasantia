using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Pasantia
{
    public partial class FormRegistro : Form
    {
        public FormRegistro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtCorreo.Text == "" || txtTelefono.Text == "" ||
                txtUsuario.Text == "" || txtContrasena.Text == "")
            {  // Mensaje que comlete los campos
                MessageBox.Show("Complete todos los campos");
                return;
            }

            bool correoValido = Regex.IsMatch( // valida del correo que deba tener las @
                txtCorreo.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            );

            if (!correoValido)
            {
                MessageBox.Show("Correo no válido. Ejemplo: juan@gmail.com");
                return;
            }

            bool telefonoValido = Regex.IsMatch( // valida los 4 espacios que debe tener 
                txtTelefono.Text,
                @"^\d{4}-\d{4}$"
            );

            if (!telefonoValido)
            {
                MessageBox.Show("Teléfono no válido. Ejemplo: 0000-0000");
                return;
            }

            try  // Para guardar en la base de datos
            {
                string cadena = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=Practica_Pasante;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open(); 

                    string sql = @"INSERT INTO Usuarios 
                       (usuario, contrasena, nombre, apellido, correo, telefono, fecha_Creacion)
                       VALUES (@u, @c, @n, @a, @co, @t, @f)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn)) 
                    {
                        cmd.Parameters.AddWithValue("@u", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@c", txtContrasena.Text);
                        cmd.Parameters.AddWithValue("@n", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@a", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@co", txtCorreo.Text);
                        cmd.Parameters.AddWithValue("@t", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@f", dtpFecha.Value); // DateTimePicker

                        cmd.ExecuteNonQuery(); 
                    }
                }

                MessageBox.Show("Usuario registrado correctamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar usuario: " + ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();

        }
    }
}
