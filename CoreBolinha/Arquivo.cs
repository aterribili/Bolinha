using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBolinha
{
    public class Arquivo
    {
        public readonly String Nome;
        public readonly int Alterado;
        public readonly int Linhas;
        public readonly Double Bolinha;

        public Arquivo(String nome, int vezesAlterado, int linhas)
        {
            this.Nome = nome;
            this.Alterado = vezesAlterado;
            this.Linhas = linhas;
            this.Bolinha = Double.Parse(linhas.ToString())  / vezesAlterado;
        }

    }
}
