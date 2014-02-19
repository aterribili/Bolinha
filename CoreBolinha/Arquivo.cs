using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBolinha
{
    public class Arquivo
    {
        private readonly String nome;
        private readonly int alterado;

        public Arquivo(String nome, int vezesAlterado)
        {
            this.nome = nome;
            this.alterado = vezesAlterado;
        }

        public override string ToString()
        {
            return "Arquivo "+nome+" alterado "+alterado.ToString()+" vezes";
        }

    }
}
