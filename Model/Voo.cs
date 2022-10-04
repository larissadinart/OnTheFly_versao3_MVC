using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Voo
    {
        public static ConexaoBD conexao = new ConexaoBD();
        public String Id { get; set; }
        public string Destino { get; set; }
        public Aeronave Aeronave { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }
        public int AssentosOcupados { get; set; }
    }
}
