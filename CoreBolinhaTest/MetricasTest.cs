using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreBolinha;
using LibGit2Sharp;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;

namespace CoreBolinhaTest
{
    [TestClass]
    public class LibGitTest : Paths
    {
        [TestMethod]
        public void DeveClonarRepositorio()
        {
            var caminhoRepo = Repository.Init(PathAleatorio(), true);
            var repositorio = PathAleatorio();
            
            Repository.Clone(caminhoRepo, repositorio);

            Assert.IsTrue(Directory.Exists(repositorio));

            using (var repo = new Repository(repositorio))
            {
                Assert.IsFalse(repo.Info.IsBare);
            }
        }

        [TestMethod]
        public void DeveCalcularQuantidadeLinhasDosArquivosEmRepositorio()
        {
            var origem = PathAleatorio();
            var repositorio = Repository.Init(origem, false);

            using (var repo = new Repository(repositorio))
            {
                File.WriteAllText(origem + "\\arquivo.txt", "Lambda\r\nLambda\r\nLambda");
                File.WriteAllText(origem + "\\arquivo-2.txt", "Teste\r\nTeste\r\nTeste");
                File.WriteAllText(origem + "\\arquivo-3.txt", "Blz\r\nBlz\r\nBlz");

                repo.Index.Stage("arquivo.txt");
                repo.Index.Stage("arquivo-2.txt");
                repo.Index.Stage("arquivo-3.txt");

                repo.Commit("Initial Commit");
            }

            Assert.IsTrue(File.Exists(origem + "\\arquivo.txt"));
            Assert.IsTrue(File.Exists(origem + "\\arquivo-2.txt"));
            Assert.IsTrue(File.Exists(origem + "\\arquivo-3.txt"));

            var novoPath = PathAleatorio();
            Repository.Clone(repositorio, novoPath);
            
            var metricas = new Metricas(novoPath);

            Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDoArquivo("\\arquivo.txt"));
            Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDoArquivo("\\arquivo-2.txt"));
            Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDoArquivo("\\arquivo-3.txt"));


        }
    }
}
