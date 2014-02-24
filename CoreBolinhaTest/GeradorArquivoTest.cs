using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CoreBolinha;
using LibGit2Sharp;

namespace CoreBolinhaTest
{

    [TestClass]
    public class GeradorArquivoTest : Setup
    {
        [TestMethod]
        public void DeveGerarArquivoFormatado()
        {
            var origemPath = Prepara();

            var novoPath = PathAleatorio();
            new GeradorArquivo(origemPath, novoPath).Arquivo();
        }

        [TestMethod]
        public void DeveGerarArquivoFormatadoNovo()
        {
            new GeradorArquivo("https://github.com/vidageek/games.git", "C:\\temp\\meu").Arquivo();
        }
    }
}

