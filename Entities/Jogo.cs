using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catálogo_Jogos.Entities
{
    public class Jogo
    {
        public Guid Id {get; set;}
        public string Nome {get; set;}
        public string Produtora {get; set;}
        public double Preco {get; set;}
    }
}