using BlockTime_Tracking.Interfaces;
using BlockTime_Tracking.Repositories;
using BlockTime_Tracking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTime_Tracking.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentosController : ControllerBase
    {
        private IEquipamentoRepository _EquipamentoRepository { get; set; }

        public EquipamentosController()
        {
            _EquipamentoRepository = new EquipamentoRepository();
        }

        [HttpPost]
        public IActionResult CriarNote(NoteViewModel noteAgente)
        {
            _EquipamentoRepository.Criar(noteAgente);
            return StatusCode(200);
        }

        [HttpPut]
        public IActionResult AtualizarNote(NoteViewModel noteAgente)
        {
            _EquipamentoRepository.AtualizarEquipamento(noteAgente);
            return StatusCode(200);
        }

        [HttpPut("/Nome")]
        public IActionResult BuscarPorNome(NoteViewModel noteAgente)
        {
            var equip = _EquipamentoRepository.BuscarPorNome(noteAgente.NomeNotebook);
            if (equip == null)
            {
                return BadRequest();
            }
            return StatusCode(200);
        }

        [HttpGet]
        public IActionResult ListarEquipamentos()
        {
            return Ok(_EquipamentoRepository.ListarEquipamentos());
        }
    }
}
