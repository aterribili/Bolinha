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
        public readonly Repository Repo;

        public Metricas(Repository repo)
        {
            this.Repo = repo;
        }

        public List<int> CalculaQuantidadeLinhasDosArquivos(List<String> nomesArquivos)
        {
            return nomesArquivos.Select((nome) => File.ReadAllLines(nome).Length).ToList();
        }

        private List<String> PegaNomeArquivosAlteradosPorCommit()
        {
            var listaDiff = Enumerable.Range(0, Repo.Commits.Count() - 1).
                Select((posicaoA) => Repo.Diff.Compare<TreeChanges>(Repo.Commits.ElementAt(posicaoA + 1).Tree, Repo.Commits.ElementAt(posicaoA).Tree));

            return listaDiff.
                SelectMany((treeChanges) =>
                    treeChanges.Added.
                    Concat(treeChanges.Modified).
                    Concat(treeChanges.Copied).
                    Concat(treeChanges.Deleted).
                    Concat(treeChanges.Renamed).
                    Concat(treeChanges.TypeChanged).
                    Select((e) => e.Path)).
                    Concat(Repo.Commits.Last().Tree.Select((e) => e.Path)).ToList();
        }

        public List<Arquivo> CalculaQuantidadeVezesQueArquivoFoiAlterado()
        {
            var nomes = PegaNomeArquivosAlteradosPorCommit();

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
