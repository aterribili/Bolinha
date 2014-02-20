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

        public Arquivo(String nome, int vezesAlterado)
        {
            this.Nome = nome;
            this.Alterado = vezesAlterado;
        }
    }
}
