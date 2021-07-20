using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Extensions;
using Test.Services.Interfaces;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RouletteController : Controller
    {
        readonly IRouletteService _IRouletteService;

        public RouletteController(IRouletteService iRouletteService)
        {
            _IRouletteService = iRouletteService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Create()
        {
            try
            {
                var resultId = await _IRouletteService.Create();
                return Ok(resultId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            try
            {
                return Ok(await _IRouletteService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPut("Open")]
        public async Task<ActionResult<string>> Open(Guid rouletteId)
        {
            try
            {
                var resultId = await _IRouletteService.ChangeStateRoulette(rouletteId,true);
                if (resultId) return Ok(new { state = true, Mensage = Constants.SUCCESSFUL });
                return BadRequest(new { state = false, Mensage = Constants.ROULETTENOWOPEN });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { state = false, Mensage = ex.Message });
            }
        }
       

    }
}