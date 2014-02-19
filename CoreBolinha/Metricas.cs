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
        public List<int> CalculaQuantidadeLinhasDosArquivos(List<String> nomesArquivos)
        {
            return nomesArquivos.Select((nome) => File.ReadAllLines(nome).Length).ToList();
        }

    }
}
