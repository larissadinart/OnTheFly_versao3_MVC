using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CompanhiaAerea
    {
        public String Cnpj { get; set; }
        public String RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimoVoo { get; set; }
        public string Situacao { get; set; }

        public ConexaoBD banco;
        
    }
}
