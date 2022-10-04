using System;
using Controller;
using Model;

namespace OnTheFly_versao3
{
    internal class Program
    {
        static AeronaveController aeronave = new();
        static CompanhiaAerea cia = new();
        static ConexaoBD conn = new();
        static Passageiro passageiro = new();
        static Passagem passagem = new();
        static Venda venda = new();
        static Voo voo = new();

        static void Main(string[] args)
        {
            Menu();
        }
        static public void Menu()
        {
            int op = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(">>>>> BEM VINDO AO AEROPORTO ON THE FLY! <<<<<\n\n");
                Console.WriteLine("Escolha a opção desejada:\n\n[1] Venda de Passagem\n[2] Passageiro\n[3] Companhias Aéreas\n[4] Vôos\n[5] Aeronaves\n[0] Sair");
                try
                {
                    op = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                switch (op)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                       // venda.MenuVenda();
                        break;
                    case 2:
                       // p.MenuPassageiro();
                        break;
                    case 3:
                       // cia.MenuCiaAerea();
                        break;
                    case 4:
                       // v.MenuVoo();
                        break;
                    case 5:
                        aeronave.MenuAeronave();
                        break;
                    default:
                        break;
                }
            } while (op < 0 || op > 5);
        }
    }
}
