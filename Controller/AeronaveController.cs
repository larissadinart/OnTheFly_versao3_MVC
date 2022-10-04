using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Model;
using Utils;

namespace Controller
{
    public class AeronaveController
    {
        static AeronaveController aero = new();
        static Aeronave aeronave = new();
        static ConexaoBD banco = new();
        static ConexaoBDController bdcontroller = new();
        static SqlConnection conexaosql = new SqlConnection();
        public void MenuAeronave()
        {
            int op;

            do
            {
                Console.Clear();
                Console.WriteLine(">>> MENU AERONAVE <<<\n\n");
                Console.WriteLine("Escolha a opção desejada:\n\n[1] Cadastrar Aeronave\n[2] Localizar Aeronave\n[3] Editar Aeronave\n[4] Voltar ao Menu Principal\n[0] Sair");
                op = int.Parse(Console.ReadLine());
            } while (op < 0 && op > 4);
            switch (op)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    aero.CadastrarAeronaves(conexaosql, aeronave, banco);
                    break;
                case 2:
                    aero.LocalizarAeronave(conexaosql);
                    break;
                case 3:
                    //aero.EditarAeronave(conexaosql);
                    break;
                case 4:
                    //Program.Menu();
                    break;
                default:
                    break;
            }
        }
        public void CadastrarAeronaves(SqlConnection conexaosql, Aeronave aeronave, ConexaoBD banco)
        {
            Validacoes val = new();

            aeronave.UltimaVenda = DateTime.Now;
            aeronave.DataCadastro = DateTime.Now;
            aeronave.Inscricao = "PR-" + GeraNumero();
            int cap = 0;

            Console.Clear();
            Console.WriteLine(">>> Cadastrar Aeronave <<<\n\n");
            Console.Write("Informe o CNPJ da Companhia Aérea: ");
            aeronave.CNPJ = Console.ReadLine();

            string sql = "Select CNPJ from CompanhiaAerea where CNPJ = '" + aeronave.CNPJ + "';";

            int verificar = VerificarExiste(sql);

            while (verificar == 0)
            {
                Console.Write("Não há nenhuma Companhia Aérea cadastrada com esse CNPJ, informe um CNPJ cadastrado: ");
                aeronave.CNPJ = Console.ReadLine();
                sql = "Select CNPJ from CompanhiaAerea where CNPJ = '" + aeronave.CNPJ + "';";
                verificar = VerificarExiste(sql);
            }
            do
            {
                Console.Write("\nInforme a Capacidade da Aeronave (entre 0 e 999): ");
                cap = int.Parse(Console.ReadLine());
                if (cap < 100 || cap > 999)
                {
                    Console.Clear();
                    Console.WriteLine("\nCapacidade inválida, tente novamente!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                aeronave.Capacidade = cap.ToString();
            } while (cap < 100 || cap > 999);
            do
            {
                Console.Write("\nInfome a Situação da Aeronave ( [A] Ativo / [I] Inativo:");
                aeronave.Situacao = Console.ReadLine().ToUpper();
            } while (!aeronave.Situacao.Equals("A") && !aeronave.Situacao.Equals("I"));
            String func = $"Insert Into Aeronave(InscricaoAnac,Cnpj,Data_Cadastro,Situacao,UltimaVenda,Capacidade) " +
                         $"Values ('{aeronave.Inscricao}','{aeronave.CNPJ}','{aeronave.DataCadastro}','{aeronave.Situacao}','{aeronave.UltimaVenda}','{aeronave.Capacidade}');";
            banco = new ConexaoBD();
            bdcontroller.InserirBD(func,conexaosql);
            Console.Clear();
            Console.WriteLine($">>> Informações Cadastradas:\n\nId:{aeronave.Inscricao}\nCNPJ: {aeronave.CNPJ}\nData de Cadastro: {aeronave.DataCadastro}\nData da última venda: {aeronave.UltimaVenda}\nCapacidade: {aeronave.Capacidade}\nSituação: {aeronave.Situacao}\n");
            Console.WriteLine("\nCadastro de Aeronave Salvo com Sucesso!\n\nAperte enter para continuar.");
            Console.ReadKey();

            //Program.Menu();CHAMAR MENU AQUI
        }
        public void LocalizarAeronave(SqlConnection conexaosql) //ERROOOO
        {
            ConexaoBD banco = new();

            Console.Clear();
            Console.WriteLine(">>> Localizar Aeronave <<<\n\n");
            Console.Write("Digite o ID da Aeronave: ");
            aeronave.Inscricao = Console.ReadLine();
            while (aeronave.Inscricao.Length < 6)
            {
                Console.WriteLine("\nID Inválido, tente novamente:");
                aeronave.Inscricao = Console.ReadLine();
            }
            String sql = $"Select InscricaoANAC,CNPJ,Data_Cadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where InscricaoANAC='{this.Inscricao}' and CNPJ='{this.CNPJ}';";
            Console.WriteLine(sql);
            banco = new ConexaoBD();
            Console.Clear();
            Console.WriteLine(sql);
            if (!string.IsNullOrEmpty(LocalizarAeronave(sql, conexaosql)))
            {
                LocalizarDado(sql, conexaosql,parametro);
                Console.WriteLine("Aperte enter para continuar.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Aeronave não encontrada!!!");
            }
            //Program.Menu();
        }
        public static string LocalizarDado(string sql, SqlConnection conexao, string parametro)
        {
            var situacao = "";
            ConexaoBD caminho = new();
            conexao = new(bdcontroller.Caminho());
            conexao.Open();
            SqlCommand cmd = new(sql, conexao);
            cmd.CommandType = CommandType.Text;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        situacao = reader[$"{parametro}"].ToString();
                    }
                }
            }
            conexao.Close();
            return situacao;
        }
        public void DeletarAeronave(SqlConnection conexaosql, Aeronave aeronave, ConexaoBD banco)
        {
            Console.WriteLine("\n>>> Deletar Aeronave <<<");
            Console.Write("\nDigite o ID da aeronave: ");
            aeronave.Inscricao = Console.ReadLine().ToUpper();
            while (aeronave.Inscricao.Length < 6)
            {
                Console.WriteLine("\nID Inválido, tente novamente: ");
                Console.Write("ID_ANAC: ");
                aeronave.Inscricao = Console.ReadLine().ToUpper();
            }
            Console.Clear();
            // String sql = $"Select ID_ANAC,DATA_CADASTRO,SITUACAO,ULTIMA_VENDA,CAPACIDADE From AERONAVE Where ID_ANAC=('{this.Inscricao}') and CNPJ=('{this.CNPJ}');";
            String sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where InscricaoANAC=('{this.Inscricao}');";
            banco = new ConexaoBD();
            if (!string.IsNullOrEmpty(banco.LocalizarAeronave(sql, conexaosql)))
            {
                Console.WriteLine("Deseja excluir cadastro de Aeronave? ");
                Console.Write("1- Sim / 2- Não ");
                int op = int.Parse(Console.ReadLine());
                if (op == 1)
                {
                    //Ver se existe Companhia Aeronave cadastrada, perguntar pro usuario o CNPJ manda pro banco via select e  localizar e se tiver deletar
                    // USei Para testa
                    aeronave.CNPJ = "15086511000145";
                    sql = $"Delete From Aeronave Where InscricaoANAC=('{aeronave.Inscricao}') and CNPJ=('{aeronave.CNPJ}');";
                    banco = new ConexaoBanco();
                    banco.DeletarBD(sql, conexaosql);
                    Console.WriteLine("\nCadastro de Aeronave excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nOpção excluir Aeronave não Foi selecionada.");
                }
            }
            else
            {
                Console.WriteLine("\nAeronave não Encontrada!!!");
            }
            Program.Menu();
        }
        public void EditarAeronave(SqlConnection conexaosql, Aeronave aeronave)
        {
            ConexaoBD banco = new ConexaoBD();
            int opc = 0;
            Console.Clear();
            Console.WriteLine(">>> Editar Aeronave <<<\n\n");
            Console.Write("Digite o ID da aeronave: ");
            aeronave.Inscricao = Console.ReadLine().ToUpper();
            while (aeronave.Inscricao.Length < 6)
            {
                Console.WriteLine("\nID Inválido, tente Novamente: ");
                Console.Write("ID_ANAC: ");
                aeronave.Inscricao = Console.ReadLine().ToUpper();
            }
            Console.Clear();
            String sql = $"Select InscricaoANAC,CNPJ,Data_Cadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where InscricaoANAC=('{this.Inscricao}');";
            banco = new ConexaoBD();
            if (!string.IsNullOrEmpty(banco.LocalizarAeronave(sql, conexaosql)))
            {
                Console.Clear();
                Console.WriteLine("Digite a Opção que Deseja Editar\n");
                Console.WriteLine("1- Data do Cadastro");
                Console.WriteLine("2- Data da última venda");
                Console.WriteLine("3- Situação");
                Console.WriteLine("4- Capacidade");
                opc = int.Parse(Console.ReadLine());
                while (opc < 1 || opc > 4)
                {
                    Console.WriteLine("\nDigite uma Opcão válida:");
                    opc = int.Parse(Console.ReadLine());
                }
                switch (opc)
                {
                    case 1:
                        Console.Write("\nAlterar a Data de Cadastro: ");
                        aeronave.DataCadastro = DateTime.Parse(Console.ReadLine());
                        sql = $"Update Aeronave Set Data_Cadastro=('{aeronave.DataCadastro}') Where InscricaoANAC=('{aeronave.Inscricao}');";
                        Console.WriteLine("\nData de cadastro alterada Com Sucesso... ");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case 2:
                        Console.Write("\nAlterar a Data da Ultima Venda: ");
                        aeronave.UltimaVenda = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("\nData da última Venda alterada Com Sucesso... ");
                        Thread.Sleep(2000);
                        Console.Clear();
                        sql = $"Update Aeronave Set UltimaVenda=('{aeronave.UltimaVenda}') Where InscricaoANAC=('{aeronave.Inscricao}');";
                        break;
                    case 3:
                        Console.Write("\nAlterar Situação: ");
                        Console.WriteLine("\nA-Ativo ou I-Inativo");
                        Console.Write("Situacao: ");
                        aeronave.Situacao = Console.ReadLine();
                        sql = $"Update Aeronave Set Situacao=('{aeronave.Situacao}') Where InscricaoANAC=('{aeronave.Inscricao}');";
                        Console.WriteLine("\nSituação Editada Com Sucesso. ");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case 4:
                        Console.Write("\nAlterar a Capacidade: ");
                        aeronave.Capacidade = Console.ReadLine();
                        sql = $"Update Aeronave Set Capacidade=('{aeronave.Capacidade}') Where InscricaoANAC=('{aeronave.Inscricao}');";
                        Console.WriteLine("\nCapacidade Editada Com Sucesso. ");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                }
                banco = new ConexaoBD();
                banco.EditarBD(sql, conexaosql);
            }
            else
            {
                Console.WriteLine("Aeronove não encontrada.");
            }
            Program.Menu();

        }
        public void ConsultarAeronave(SqlConnection conexaosql, ConexaoBD banco)
        {
            ConexaoBD banco = new ConexaoBD();
            int op = 0;
            String sql = "";
            Console.Write("\nDeseja consultar situação de aeronave" +
                             "\n1-Ativo , 2-Inativo , 3-Geral\n" +
                             "\nConsulta: ");
            op = int.Parse(Console.ReadLine());
            switch (op)
            {
                case 1:
                    Console.Clear();
                    Console.Write("\n*** Aeronaves Ativas ***\n");
                    aeronave.Situacao = "A";
                    sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where Situacao = '{this.Situacao}' ;";
                    banco = new ConexaoBD();
                    banco.LocalizarAeronave(sql, conexaosql);
                    break;
                case 2:
                    Console.Clear();
                    Console.Write("\n*** Aeronaves Ativas ***\n");
                    aeronave.Situacao = "I";
                    sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where Situacao = '{this.Situacao}');";
                    banco = new ConexaoBD();
                    banco.LocalizarAeronave(sql, conexaosql);
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("\n*** Aeronaves Cadastradas ***\n");
                    sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave;";
                    banco = new ConexaoBD();
                    banco.LocalizarAeronave(sql, conexaosql);
                    break;
            }
            Program.Menu();
        }
        public int VerificarExiste(string sql)
        {
            ConexaoBDController conn = new();
            SqlConnection conexao = new(conn.Caminho());
            conexao.Open();
            int count = 0;
            SqlCommand sqlVerify = conexao.CreateCommand();
            sqlVerify.CommandText = sql;
            sqlVerify.Connection = conexao;
            using (SqlDataReader reader = sqlVerify.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }
            if (count != 0)
            {
                conexao.Close();
                return 1;
            }
            conexao.Close();
            return 0;
        }
        public String GeraNumero()
        {
            Random rand = new Random();
            int[] numero = new int[100];
            int aux = 0;
            String convert = "";
            for (int k = 0; k < numero.Length; k++)
            {
                int rnd = 0;
                do
                {
                    rnd = rand.Next(100, 999);
                } while (numero.Contains(rnd));
                numero[k] = rnd;
                aux = numero[k];
                convert = aux.ToString();
                break;
            }
            return convert;
        }
    }
}
