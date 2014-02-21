using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using System.IO;

namespace CoreBolinha
{
    public class GeradorArquivo
    {
        private readonly String Url;
        public readonly Ambiente ambiente;

        public GeradorArquivo(String url, String path)
        {
            this.Url = url;
            ambiente = new Ambiente(path, url);
        }

        public void Arquivo()
        {
            using (var repo = new Repository(ambiente.Path))
            {
                var metricas = new Metricas(repo);
                var commits = metricas.GeraMetricas();
                var nomesArquivos = ambiente.PegaNomeDosArquivos();
                var linhas = metricas.CalculaQuantidadeLinhasDosArquivos(nomesArquivos);

                var maiorNome = commits.Max((elemento) => elemento.Nome.Length) + 1;

                StreamWriter writer = new StreamWriter("C:\\temp\\relatorio.txt");

                string header = String.Format("{0,-" + maiorNome + "} {1,-6} {2,-7} {3,-7}",
                                          "Nome", "Linhas", "Commits", "Bolinha");
                
                writer.WriteLine(header);

                var zipado = commits.Zip(linhas, (arquivo, numeroLinhas) => new Tuple<Arquivo, int>(arquivo, numeroLinhas));

                zipado.ToList().ForEach((tupla) => writer.
                    WriteLine("{0,-" + maiorNome + "} {1,6:N0} {2,7:N0} {3,7:N0}", tupla.Item1.Nome, tupla.Item2, tupla.Item1.Alterado, tupla.Item1.Bolinha));

                writer.Close();
            }
        }
    }
}
