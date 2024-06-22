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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            ConexaoDAO conexaoDAO = new ConexaoDAO();
            List<Avaliacao> listaAvaliacoes = conexaoDAO.GetAvaliacoes();

            foreach (Avaliacao avaliacao in listaAvaliacoes)
            {
                label1.Text = label1.Text + ("Nome funcionário: " + avaliacao.NomeFuncionario + " |  Avaliação: " + avaliacao.Nome) + "\n";
            }
        }
    }
}
