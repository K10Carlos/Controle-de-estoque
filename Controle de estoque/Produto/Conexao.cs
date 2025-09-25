using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Controle_de_estoque
{
    public class Conexao
    {
        public static string conexao = "server=localhost;user=root;password=;database=meu_banco;";
      

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }
    }
}