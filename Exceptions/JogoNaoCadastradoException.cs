using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catálogo_Jogos.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException()
            : base("Jogo não cadastrado")
        { }
    }
}