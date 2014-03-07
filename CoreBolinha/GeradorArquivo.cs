using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using System.IO;
using CoreBolinha;

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
                var commits = new Metricas(repo).GeraMetricas();
                var nomesArquivos = ambiente.PegaNomeDosArquivos();

                GeraRelatorioPorArquivo(commits);

                GeraRelatorioGeral(commits);
            }
        }

        private void GeraRelatorioPorArquivo(List<Arquivo> commits)
        {
            var contador = 0;

            commits.ForEach((arquivo) =>
            {
                var relatorioArquivo = new StreamWriter("C:\\temp\\arquivos-relatorio\\arquivo-" + contador + ".html");

                relatorioArquivo.Write("<!DOCTYPE html><html><head><meta charset='utf-8'><title>arquivo-" +
                    contador++ + ".html</title></head><body><h1>Nome: " + arquivo.Nome + "</h1>" + "<ul><li>Linhas: " +
                    arquivo.Linhas + "</li><li>Alterado: " + arquivo.Alterado + "</li><li>Bolinha :" + arquivo.Bolinha + "</li></ul></body></html>");

                relatorioArquivo.Close();
            });
        }

        private void GeraRelatorioGeral(List<Arquivo> commits)
        {
            var relatorioGeral = new StreamWriter("C:\\temp\\relatorio.html");

            string header = "<!DOCTYPE html><html><head><meta charset='utf-8'><title>Relatorio Git</title></head><body>";
            relatorioGeral.WriteLine(header);

            string estruturaTable = "<table><thead><tr><td>Nome</td><td>Linhas</td><td>Alterado</td><td>Bolinha</td></tr></thead><tbody>";
            relatorioGeral.WriteLine(estruturaTable);

            var contador2 = 0;
            commits.ForEach((arquivo) => relatorioGeral.
                WriteLine("<tr><td><a href='arquivos-relatorio\\arquivo-" + contador2++ + ".html'>" +
                arquivo.Nome + "</a></td><td>" + arquivo.Linhas + "</td><td>" +
                arquivo.Alterado + "</td><td>" + arquivo.Bolinha + "</td></tr>"));

            string footer = "</tbody></table></body></html>";

            relatorioGeral.WriteLine(footer);

            relatorioGeral.Close();
        }
    }
}
