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
            }

            Assert.IsTrue(File.Exists(destinoPath + "\\arquivo.txt"));
            Assert.IsTrue(File.Exists(destinoPath + "\\arquivo-2.txt"));
            Assert.IsTrue(File.Exists(destinoPath + "\\arquivo-3.txt"));

            var novoPath = PathAleatorio();
            var metricas = new Metricas();

            var listaArquivos = new Ambiente(novoPath, origemPath).PegaNomeDosArquivos();

            Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDosArquivos(listaArquivos)[0]);
            Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDosArquivos(listaArquivos)[1]);
            Assert.AreEqual(3, metricas.CalculaQuantidadeLinhasDosArquivos(listaArquivos)[2]);
        }
    }
}
