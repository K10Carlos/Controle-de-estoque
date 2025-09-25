using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Controle_de_estoque
{
    public partial class Form1 : Form
    {
        List<Produto> estoque = new List<Produto>();

        public Form1()
        {
            InitializeComponent();
        }

        private void AtualizarLista()
        {
            lstProdutos.Items.Clear();
            foreach (Produto p in estoque)
            {
                lstProdutos.Items.Add(p);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            double preco = double.Parse(txtPreco.Text);
            int qtd = int.Parse(txtQuantidade.Text);

            Produto p = new Produto(nome, preco, qtd);
            estoque.Add(p);

            AtualizarLista();
        }

        private void btnIncrementar_Click(object sender, EventArgs e)
        {
            if (lstProdutos.SelectedItem is Produto p)
            {
                p.Quantidade++;
                AtualizarLista();
            }
            else
            {
                MessageBox.Show("Selecione um produto!");
            }
        }

        private void btnDecrementar_Click(object sender, EventArgs e)
        {
            if (lstProdutos.SelectedItem is Produto p)
            {
                if (p.Quantidade > 0)
                {
                    p.Quantidade--;
                }
                else
                {
                    MessageBox.Show("Estoque insuficiente!");
                }
                AtualizarLista();
            }
            else
            {
                MessageBox.Show("Selecione um produto!");
            }
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e, MySqlConnection conn)
        {

            string nome = txtNome.Text;
            double preco = double.Parse(txtPreco.Text);
            int qtd = int.Parse(txtQuantidade.Text);

            using (var conn = new Conexao().GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO produto (nome, preco, quantidade) VALUES (@nome, @preco, @qtd)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@preco", preco);
                cmd.Parameters.AddWithValue("@qtd", qtd);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Produto cadastrado com sucesso!");
            CarregarProdutos();
        }



        private void btnIncrementar_Click_1(object sender, EventArgs e)
        {
            if (lstProdutos.SelectedItem is Produto p)
            {
                using (var conn = new Conexao().GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE produto SET quantidade = quantidade + 1 WHERE nome = @nome";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", p.Nome);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                CarregarProdutos();
            }
        }

        private void lstProdutos_SelectedIndexChanged(object sender, EventArgs e)
        { }
          public void CarregarProdutos()
          {
            lstProdutos.Items.Clear();
            using (var conn = new Conexao().GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM produto";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Produto p = new Produto(
                        reader["nome"].ToString(),
                        Convert.ToDouble(reader["preco"]),
                        Convert.ToInt32(reader["quantidade"])
                    );
                    lstProdutos.Items.Add(p);
                }
                conn.Close();
            }
          }
        
           
        

        private void btnDecrementar_Click_1(object sender, EventArgs e)
        {
            if (lstProdutos.SelectedItem is Produto p)
            {
                using (var conn = new Conexao().GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE produto SET quantidade = quantidade - 1 WHERE nome = @nome AND quantidade > 0";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nome", p.Nome);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                CarregarProdutos();
            }
        }
    } 
    
}
    