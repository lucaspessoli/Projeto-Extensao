using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        ConexaoDAO con = new ConexaoDAO();
        public Form2()
        {
            InitializeComponent();
            List<Funcionario> funcionarios = con.GetFuncionarios();
            foreach(Funcionario funcionario in funcionarios)
            {
                comboBox1.Items.Add(funcionario.Nome);
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.UploadAvaliacao(comboBox1.Text, textBox1.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar avaliação \n " + ex);

            }
        }
    }
}
