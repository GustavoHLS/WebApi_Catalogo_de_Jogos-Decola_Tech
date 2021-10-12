using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catálogo_Jogos.InputModel;
using Catálogo_Jogos.ViewModel;

namespace Catálogo_Jogos.Services
{
    public interface IJogoService :IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter (Guid id);
        Task<JogoViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar(Guid id, JogoInputModel jogo);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}