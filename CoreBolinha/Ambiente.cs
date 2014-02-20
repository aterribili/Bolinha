using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using System.IO;

namespace CoreBolinha
{
    public class Ambiente
    {
        private readonly String path;

        public Ambiente(String path, String origemPath)
        {
            this.path = path;
            Repository.Clone(origemPath, path);
        }

        private List<String> PegaNomeDosArquivos()
        {

            return Directory.GetDirectories(path).SelectMany((dd) => Directory.GetFiles(dd)).Concat(Directory.GetFiles(path)).ToList();
        }

        public List<String> PegaDiretoriosInfinitamente(String path)
        {
            if (path == this.path)
                return PegaNomeDosArquivos();

            return Directory.GetDirectories(path).ToList().SelectMany(()=> PegaNomeDosArquivos()).ToList();
        }
    }
}
