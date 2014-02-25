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

                StreamWriter writer = new StreamWriter("C:\\temp\\relatorio.html");

                string header = String.Format("<!DOCTYPE html><html><head><meta charset='utf-8'><title>Relatorio Git</title></head><body>");
                writer.WriteLine(header);

                string tabela = String.Format("<table><tbody><tr><td>Nome</td><td>Linhas</td><td>Alterado</td><td>Bolinha</td></tr>");
                writer.WriteLine(tabela);

                commits.ForEach((arquivo) => writer.
                    WriteLine("<tr><td>" + arquivo.Nome + "</td><td>" + arquivo.Linhas + "</td><td>" +
                    arquivo.Alterado + "</td><td>" + arquivo.Bolinha + "</td></tr>"));

                string footer = String.Format("</tbody></table></body></html>");
                writer.WriteLine(footer);

                writer.Close();
            }
        }
    }
}
