using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreBolinha;
using LibGit2Sharp;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;
using System.Linq;

namespace CoreBolinhaTest
{
    [TestClass]
    public class MetricasTest : Paths
    {
        [TestMethod]
        public void DeveCalcularQuantidadeLinhasDosArquivosEmRepositorio()
        {
            var destinoPath = PathAleatorio();
            var origemPath = Repository.Init(destinoPath, false);

            using (var repo = new Repository(origemPath))
            {
                File.WriteAllText(destinoPath + "\\arquivo.txt", "Lambda\r\nLambda\r\nLambda");
                File.WriteAllText(destinoPath + "\\arquivo-2.txt", "Teste\r\nTeste\r\nTeste");
                File.WriteAllText(destinoPath + "\\arquivo-3.txt", "Blz\r\nBlz\r\nBlz");

                repo.Index.Stage("arquivo.txt");
                repo.Index.Stage("arquivo-2.txt");
                repo.Index.Stage("arquivo-3.txt");

                repo.Commit("Initial Commit");


                Assert.IsTrue(File.Exists(destinoPath + "\\arquivo.txt"));
                Assert.IsTrue(File.Exists(destinoPath + "\\arquivo-2.txt"));
                Assert.IsTrue(File.Exists(destinoPath + "\\arquivo-3.txt"));

                var novoPath = PathAleatorio();
                var metricas = new Metricas(repo);

                var listaArquivos = new Ambiente(novoPath, origemPath).PegaNomeDosArquivos();

                Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDosArquivos(listaArquivos)[0]);
                Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDosArquivos(listaArquivos)[1]);
                Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDosArquivos(listaArquivos)[2]);
            }
        }

        [TestMethod]
        public void DeveListarCommitsPorArquivo()
        {
            var destinoPath = PathAleatorio();
            var origemPath = Repository.Init(destinoPath, false);

            using (var repo = new Repository(origemPath))
            {
                File.WriteAllText(destinoPath + "\\arquivo.txt", "Lambda\r\nLambda\r\nLambda");

                repo.Index.Stage("arquivo.txt");
                repo.Commit("Adicionando arquivo");

                File.WriteAllText(destinoPath + "\\arquivo-2.txt", "Teste\r\nTeste\r\nTeste");
                repo.Index.Stage("arquivo-2.txt");
                repo.Commit("Adicionando arquivo-2");

                File.WriteAllText(destinoPath + "\\arquivo-2.txt", "T\r\nT\r\nT");
                repo.Index.Stage("arquivo-2.txt");
                repo.Commit("Adicionando arquivo-2");

                File.WriteAllText(destinoPath + "\\arquivo-3.txt", "Blz\r\nBlz\r\nBlz");
                repo.Index.Stage("arquivo-3.txt");

                File.WriteAllText(destinoPath + "\\arquivo-4.txt", "Blz\r\nBlz\r\nBlz");
                repo.Index.Stage("arquivo-4.txt");
                repo.Commit("Adicionando arquivo-3 e arquivo-4");

                Assert.AreEqual(4, repo.Commits.Count());
                Assert.AreEqual("arquivo.txt", new Metricas(repo).CalculaQuantidadeVezesQueArquivoFoiAlterado().ElementAt(0).Nome);
                Assert.AreEqual(1, new Metricas(repo).CalculaQuantidadeVezesQueArquivoFoiAlterado().ElementAt(0).Alterado);
                
                Assert.AreEqual("arquivo-2.txt", new Metricas(repo).CalculaQuantidadeVezesQueArquivoFoiAlterado().ElementAt(1).Nome);
                Assert.AreEqual(2, new Metricas(repo).CalculaQuantidadeVezesQueArquivoFoiAlterado().ElementAt(1).Alterado);

                Assert.AreEqual("arquivo-3.txt", new Metricas(repo).CalculaQuantidadeVezesQueArquivoFoiAlterado().ElementAt(2).Nome);
                Assert.AreEqual(1, new Metricas(repo).CalculaQuantidadeVezesQueArquivoFoiAlterado().ElementAt(2).Alterado);

                Assert.AreEqual("arquivo-4.txt", new Metricas(repo).CalculaQuantidadeVezesQueArquivoFoiAlterado().ElementAt(3).Nome);
                Assert.AreEqual(1, new Metricas(repo).CalculaQuantidadeVezesQueArquivoFoiAlterado().ElementAt(3).Alterado);
            }

        }
    }
}
