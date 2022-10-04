using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Venda
    {
        public int Id_Venda { get; set; }
        public DateTime Data_venda { get; set; }
        public double Valor_Total { get; set; }
        public String Id_Passagem { get; set; }
        public string Cpf { get; set; }
        public ConexaoBD banco = new ConexaoBD();
        public Passageiro passageiro = new Passageiro();
        public Passagem passagem { get; set; }
        public Voo voo = new Voo();
        public Venda() { }
    }
}
