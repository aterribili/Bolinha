using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace CoreBolinha
{
    public class Ambiente
    {
        public void ClonaRepositorio(String origemPath, String destinoPath)
        {
            Repository.Clone(origemPath, destinoPath);
        }
    }
}
