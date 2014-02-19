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

        private List<String> PegaNomeArquivosAlteradosPorCommit(Commit commit)
        {
            commit.Message.Select((e) => e);
            return commit.Tree.Select((a) => a.Name).ToList();
        }

        public List<Arquivo> CalculaQuantidadeVezesQueArquivoFoiAlterado(IQueryableCommitLog commits)
        {

            var nomes = commits.SelectMany((commit) => PegaNomeArquivosAlteradosPorCommit(commit));

            return nomes.
                OrderBy((elemento) => elemento).
                Distinct().
                Select((nome) => new Arquivo(nome, ContaOcorrenciasNomeArquivo(nomes, nome))).ToList();
        }

        private int ContaOcorrenciasNomeArquivo(IEnumerable<string> nomes, string nome)
        {
            return nomes.Where((arquivo) => nome == arquivo).Count();
        }
    }
}
