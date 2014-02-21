using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CoreBolinha;
using LibGit2Sharp;

namespace CoreBolinhaTest
{
    [TestClass]
    public class GeradorArquivoTest : Paths
    {
        [TestMethod]
        public void DeveGerarArquivoFormatado()
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
                var novoPath = PathAleatorio();

                new GeradorArquivo(origemPath, novoPath).Arquivo();
            }
        }

        [TestMethod]
        public void DeveGerarArquivoFormatadoNovo()
        {
            new GeradorArquivo("https://github.com/vidageek/games.git", "C:\\temp\\meu").Arquivo();
        }
    }
}
