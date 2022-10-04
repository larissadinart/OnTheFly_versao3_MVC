using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ConexaoBD
    {
        public string Conexao = "Data Source=localhost;Initial Catalog=OnTheFly_versao3;User Id=sa;Password=Lari123*;";
        SqlConnection conn;
    }
}
