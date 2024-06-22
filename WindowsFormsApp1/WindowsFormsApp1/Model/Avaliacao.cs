using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    internal class Avaliacao
    {
        public int AvaliacaoId { get; set; }
        public string Nome { get; set; }
        public int FuncionarioId { get; set; }
        public string NomeFuncionario { get; set; }
    }
}
