using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sistema_Pasantia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=Practica_Pasante;Integrated Security=True;TrustServerCertificate=True");
            con.Open();
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario=@Usuario AND Contrasena=@Contrasena";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Usuario", textBox1.Text);
            cmd.Parameters.AddWithValue("@Contrasena", textBox2.Text);
            int count = (int)cmd.ExecuteScalar();
            con.Close();
            if (count > 0)
            {
                FormProductos pf = new FormProductos();
                pf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRegistro registro = new FormRegistro();
            registro.Show();
            this.Hide();
        }
    }
}
