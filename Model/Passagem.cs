using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Passagem
    {
        public static ConexaoBD conexao = new ConexaoBD();
        public string IdPassagem { get; set; }
        public Voo voo { get; set; } //idVoo
        public DateTime DataUltimaOperacao { get; set; }
        public float Valor { get; set; }
        public string SituacaoPassagem { get; set; }
    }
}
