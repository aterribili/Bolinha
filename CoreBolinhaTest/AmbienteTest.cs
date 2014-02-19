using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using LibGit2Sharp;
using CoreBolinha;


namespace CoreBolinhaTest
{
    [TestClass]
    public class AmbienteTest : Paths
    {
        [TestMethod]
        public void DeveClonarRepositorio()
        {
            var destinoPath = PathAleatorio();
            var origemPath = Repository.Init(PathAleatorio(), true);

            new Ambiente(destinoPath, origemPath);

            Assert.IsTrue(Directory.Exists(destinoPath));

            using (var repo = new Repository(destinoPath))
            {
                Assert.IsFalse(repo.Info.IsBare);
            }
        }
    }
}
