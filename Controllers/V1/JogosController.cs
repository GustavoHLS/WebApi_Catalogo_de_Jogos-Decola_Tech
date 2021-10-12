using Catálogo_Jogos.InputModel;
using Catálogo_Jogos.ViewModel;
using Catálogo_Jogos.Services;
using Catálogo_Jogos.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace Catálogo_Jogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoservice;
        public JogosController(IJogoService jogoService)
        {
            _jogoservice = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoservice.Obter(pagina, quantidade);
            if(jogos.Count() == 0)
            {
                return NoContent();
            }
            return Ok(jogos);
        }
         [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoservice.Obter(idJogo);
            if(jogo == null)
            {
                return NoContent();
            }
            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoservice.Inserir(jogoInputModel);
                return Ok(jogo);
            }
            catch (JogoJaCadastradoException)
            {
                return UnprocessableEntity("Já existe um jogo com este nome");
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoservice.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch(JogoNaoCadastradoException)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("{idJogo:guid}/preco{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoservice.Atualizar(idJogo, preco);
                return Ok();
            }
            catch(JogoNaoCadastradoException)
            {
                return NotFound("Não existe esse jogo");
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoservice.Remover(idJogo);
                return Ok();
            }
            catch(JogoNaoCadastradoException)
            {
                return NotFound("Não existe esse jogo");
            }
        }
    }

}