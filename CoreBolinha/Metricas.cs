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

        public List<int> CalculaQuantidadeLinhasDoArquivo(List<String> nomesArquivos)
        {
            var lista = new List<int>();
            foreach (String nome in nomesArquivos)
                lista.Add(File.ReadAllLines(caminhoRepositorio + nome).Length);

            return lista;
        }

    }
}
