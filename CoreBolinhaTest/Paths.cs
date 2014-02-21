using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBolinha
{
    public abstract class Paths
    {
        private List<String> paths = new List<String>();
        private int contador = 0;

        public String PathAleatorio()
        {
            var path = Path.Combine(Path.GetTempPath(), "bolinha-" + contador++);
            paths.Add(path);

            return path;
        }

        [TestCleanup]
        public void TearDown()
        {
            paths.ForEach((path) => DeleteDirectory(path));
        }

        private void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}
