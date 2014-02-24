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

                var maiorNome = commits.Max((elemento) => elemento.Nome.Length) + 1;

                StreamWriter writer = new StreamWriter("C:\\temp\\relatorio.txt");

                string header = String.Format("{0,-" + maiorNome + "} {1,-6} {2,-7} {3,-7}",
                                          "Nome", "Linhas", "Commits", "Bolinha");
                
                writer.WriteLine(header);

                commits.ForEach((arquivo) => writer.
                    WriteLine("{0,-" + maiorNome + "} {1,6:N0} {2,7:N0} {3,7:N0}", arquivo.Nome, arquivo.Linhas, arquivo.Alterado, arquivo.Bolinha));

                writer.Close();
            }
        }
    }
}
