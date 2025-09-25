using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Controle_de_estoque
{
    public class Produto
    {
        public int Id { get; set; }
        public String Nome  { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }

        public Produto (string nome, double preco, int quantidade)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }
        public override string ToString()
        {
            return $"{Nome} - R$ {Preco:F2} - Qtd: {Quantidade}";

        }
    }
}
