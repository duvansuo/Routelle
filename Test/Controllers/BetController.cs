using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Test.Extensions;
using Test.Models;
using Test.Services.Interfaces;

namespace Test.Controllers
{
    [ApiController]
    public class BetController : Controller
    {
        readonly IBetService _IBetService;

        public BetController(IBetService iBetService)
        {
            _IBetService = iBetService;
        }
        public async Task<IActionResult> Create(BetDto betDto)
        {
            try
            {
                Request.Headers.TryGetValue("UserId", out var userId);
                betDto.UserId = userId;
                var validate = await _IBetService.ValidateBet(betDto);
                if (!validate.Status) return BadRequest(validate.Mesage);
                betDto.BetId = Guid.NewGuid();
                bool result = await _IBetService.Create(betDto);
                if (!result)
                    return BadRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}