using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catálogo_Jogos.Exceptions
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException()
            : base("Este jogo já está cadastrado")
        { }
    }
}