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
    public class EmpresasController : ControllerBase
    {
        private IEmpresaRepository EmpresaRepository { get; set; }

        public EmpresasController()
        {
            EmpresaRepository = new EmpresaRepository();
        }

        [HttpPost]
        public IActionResult AdcionarEmpresas()
        {
            EmpresaRepository.AdcionarEmpresas();
            return Ok();
        }

        [HttpGet]
        public IActionResult ListarEmpresas()
        {
            return Ok(EmpresaRepository.ListarEmpresas());
        }

        [HttpDelete("{idEmpresa}")]
        public IActionResult DeletarEmpresas(int idEmpresa)
        {
            EmpresaRepository.Deletar(idEmpresa);
            return Ok();
        }
    }
}
