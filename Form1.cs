using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teste_conecsao_banco
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Conectar())
            {
                MessageBox.Show("conecxão bem sucedida"); 
            }
        }
        private bool Conectar()
        {
            var result = false;
            try
            {
                using (var cn = new MySqlConnection(conn.strConn))
                {
                    cn.Open();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show("falha: " + ex.Message);
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string n1 = textBox1.Text;
            string n2 = textBox2.Text;
            string n3 = textBox3.Text;
            using (var cn = new MySqlConnection(conn.strConn))
            {
                cn.Open();

                var cmd = new MySqlCommand("INSERT INTO usuarios(nome,email,senha) VALUES(@nome, @email, @senha)", cn);
                cmd.Parameters.AddWithValue("@nome", n1);
                cmd.Parameters.AddWithValue("@email", n2);
                cmd.Parameters.AddWithValue("@senha", n3);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Dados adicionados com sucesso");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = textBox4.Text;
            using (var cn = new MySqlConnection(conn.strConn))
            {
                cn.Open();

                var cmd = new MySqlCommand("SELECT * FROM usuarios WHERE id = @id", cn);
                cmd.Parameters.AddWithValue("@id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var dado = reader["nome"].ToString();

                    MessageBox.Show("Dado obtido: " + dado);
                }
                else
                {
                    MessageBox.Show("Nenhum dado encontrado");
                }
            }

        }
    }
}