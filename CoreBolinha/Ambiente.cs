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
        public void ClonaRepositorio(String origemPath, String destinoPath)
        {
            Repository.Clone(origemPath, destinoPath);
        }

        public List<String> PegaNomeDosArquivos(String origemPath)
        {
            var lista = new List<String>();
            var nomes = Directory.GetFiles(origemPath);

            foreach (String nome in nomes)
                lista.Add(nome);

            return lista;
        }
    }
}
