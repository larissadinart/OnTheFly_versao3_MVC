using System;

namespace Model
{
    public class Aeronave
    {
        public String Inscricao { get; set; }
        public String Capacidade { get; set; }
        public DateTime UltimaVenda = DateTime.Now;
        public DateTime DataCadastro = DateTime.Now;
        public string Situacao { get; set; }
        public ConexaoBD banco = new ConexaoBD();
        public String CNPJ { get; set; }
    }
}
