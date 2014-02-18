using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using System.IO;

namespace CoreBolinha
{
    public class Metricas
    {
        private readonly String caminhoRepositorio;

        public Metricas(String pathOrigem)
        {
            caminhoRepositorio = pathOrigem;
        }

        public int CalculaQuantidadeLinhasDoArquivo(String nomeArquivo)
        {
            return File.ReadAllLines(caminhoRepositorio + nomeArquivo).Length;
        }

    }
}
