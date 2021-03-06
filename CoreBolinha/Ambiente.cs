﻿using System;
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
        public readonly String Path;

        public Ambiente(String path, String origemPath)
        {
            this.Path = path;
            Repository.Clone(origemPath, path);
        }

        public List<String> PegaNomeDosArquivos()
        {
            return PegaDiretoriosInfinitamente(Path);
        }

        private List<String> PegaDiretoriosInfinitamente(String path)
        {
            return Directory.GetDirectories(path).
                SelectMany((e) => PegaDiretoriosInfinitamente(e)).
                Concat(Directory.GetFiles(path)).Where((nome) => !nome.Contains(".git")).ToList();
        }
    }
}
