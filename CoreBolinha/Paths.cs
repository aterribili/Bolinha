using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBolinha
{
    public class Paths
    {
        public String PathAleatorio()
        {
            var random = new Random();
            return Path.Combine(Path.GetTempPath(), "bolinha-" + System.Guid.NewGuid());
        }
    }
}
