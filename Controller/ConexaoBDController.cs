using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    internal class ConexaoBDController
    {
        public string Caminho()
        {
            ConexaoBD conn = new();
            return conn.Conexao;
        }
        public void InserirBD(string sql, SqlConnection conexaosql)
        {
            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();
                SqlCommand cmd = new SqlCommand(sql, conexaosql);
                cmd.Connection = conexao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeletarBD(string sql, SqlConnection conexaosql)
        {
            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();
                SqlCommand cmd = new SqlCommand(sql, conexao);
                cmd.Connection = conexao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void EditarBD(string sql, SqlConnection conexaosql)
        {
            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();
                SqlCommand cmd = new SqlCommand(sql, conexao);
                cmd.Connection = conexao;
                cmd.ExecuteNonQuery();
                conexaosql.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
