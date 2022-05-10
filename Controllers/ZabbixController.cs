using BlockTime_Tracking.Interfaces;
using BlockTime_Tracking.Repositories;
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
    public class ZabbixController : ControllerBase
    {
        private IZabbixRepository _ZabbixRepository { get; set; }

        public ZabbixController()
        {
            _ZabbixRepository = new ZabbixRepository();
        }

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return Ok(_ZabbixRepository.Login());
        //}

        [HttpGet]
        public IActionResult LoginRestSharp(string hostName)
        {
            return Ok(_ZabbixRepository.GetHostByName(hostName));
        }
    }
}
