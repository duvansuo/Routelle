using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Services.Interfaces
{
    public interface IBetService
    {
        Task<bool> Create(BetDto betDto);
        Task<ValidateDto> ValidateBet(BetDto betDto);
        Task<ResultBetDto> CloseBet(Guid rouletteId);
    }
}
