using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catálogo_Jogos.Repositories;
using Catálogo_Jogos.InputModel;
using Catálogo_Jogos.ViewModel;
using Catálogo_Jogos.Exceptions;
using Catálogo_Jogos.Entities;

namespace Catálogo_Jogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
       
        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);
            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }
        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if(jogo == null)
            {
                return null;
            }
            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco 
            };
        }
        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.produtora);
            if(entidadeJogo.Count > 0)
            {
                throw new JogoJaCadastradoException();
            }
            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.produtora,
                Preco = jogo.Preco
            };
            await _jogoRepository.Inserir(jogoInsert);
            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogoInsert.Nome,
                Produtora = jogoInsert.Produtora,
                Preco = jogoInsert.Preco
            };
        }
        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);
            if(entidadeJogo == null)
            {
                throw new JogoNaoCadastradoException();
            }
            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.produtora;
            entidadeJogo.Preco = jogo.Preco;
            await _jogoRepository.Atualizar(entidadeJogo);
        }
        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);
            if(entidadeJogo == null)
            {
                throw new JogoNaoCadastradoException();
            }
            entidadeJogo.Preco = preco;
            await _jogoRepository.Atualizar(entidadeJogo);
        }
        public async Task Remover(Guid id)
        {
            var jogo = _jogoRepository.Obter(id);
            if(jogo == null)
            {
                throw new JogoNaoCadastradoException();
            }
            await _jogoRepository.Remover(id);
        }
        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}