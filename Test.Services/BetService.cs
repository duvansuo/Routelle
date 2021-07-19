using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Extensions;
using Test.Models;
using Test.Repository.Interfaces;
using Test.Services.Interfaces;

namespace Test.Services
{
    public class BetService : IBetService
    {
        readonly IBetRepository _IBetRepository;
        readonly IRouletteService _IRouletteService;
        public BetService(IBetRepository repository, IRouletteService iRouletteService)
        {
            _IBetRepository = repository;
            _IRouletteService = iRouletteService;
        }
        public async Task<bool> Create(BetDto betDto)
        {
            try
            {
                betDto.BetId = Guid.NewGuid();
                var ss = await _IBetRepository.Create(betDto) ? betDto.BetId.ToString() : throw new Exception(Constants.ERRORCREATE);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ValidateDto> ValidateBet(BetDto betDto)
        {
            ValidateDto validateDto = new ValidateDto();
            if (string.IsNullOrEmpty(betDto.UserId)) validateDto.Mesage += $"-{Constants.USERNULL}";
            if (!ValidateColor(betDto.Color)) validateDto.Mesage += $"-{Constants.INVALIDCOLOR}";
            if (!string.IsNullOrEmpty(betDto.Color) && betDto.Number != 0) validateDto.Mesage += $"-{Constants.NUMBERORCOLOR}";
            if (!await _IRouletteService.ValidRoulette(betDto.RouletteId)) validateDto.Mesage += $"-{Constants.ROULETTECLOSED }";
            validateDto.Status = string.IsNullOrEmpty(validateDto.Mesage);
            return validateDto;
        }
        private bool ValidateColor(string color)
        {
            switch (color.ToLower())
            {
                case "negro":
                case "rojo":
                    return true;
                default:
                    return false;
            }
        }
    }
}
