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

        private List<String> PegaNomeArquivosAlteradosPorCommit()
        {
            var listaDiff = Enumerable.Range(0, Repo.Commits.Count() - 1).
                Select((posicaoA) => Repo.Diff.Compare<TreeChanges>(Repo.Commits.ElementAt(posicaoA + 1).Tree, Repo.Commits.ElementAt(posicaoA).Tree));

            var elementosAtuais = PegaTodosElementosAtuais();

            return listaDiff.
                SelectMany((treeChanges) =>
                    treeChanges.Added.
                    Concat(treeChanges.Modified).
                    Concat(treeChanges.Copied).
                    Concat(treeChanges.Deleted).
                    Concat(treeChanges.Renamed).
                    Concat(treeChanges.TypeChanged).
                    Select((e) => e.Path)).
                    Concat(Repo.Commits.Last().Tree.Select((e) => e.Path)).
                    Where((elemento) => elementosAtuais.Contains(elemento)).ToList();
        }

        public List<String> PegaTodosElementosAtuais()
        {
            return PegaDiretoriosInfinitamente(Repo.Info.Path + "..\\").
                Select((e) => e.Replace(Repo.Info.Path + "..\\", "")).ToList();
        }

        private List<String> PegaDiretoriosInfinitamente(String path)
        {
            return Directory.GetDirectories(path).
                SelectMany((e) => PegaDiretoriosInfinitamente(e)).
                Concat(Directory.GetFiles(path)).
                Where((nome) => !Path.GetFullPath(nome).Contains(".git")).ToList();
        }

        private int ContaOcorrenciasNomeArquivo(IEnumerable<string> nomes, string nome)
        {
            return nomes.Where((arquivo) => nome == arquivo).Count();
        }

        public List<Arquivo> GeraMetricas()
        {
            var nomes = PegaNomeArquivosAlteradosPorCommit();

            return nomes.
                OrderBy((elemento) => elemento).
                Distinct().
                Select((nome) =>
                    new Arquivo(nome, ContaOcorrenciasNomeArquivo(nomes, nome), File.ReadAllLines(Repo.Info.Path + "..\\" + nome).Length)).ToList();
        }
    }
}
