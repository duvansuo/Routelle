using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Services.Interfaces
{
    public interface IRouletteService
    {
        Task<string> Create();
        Task<bool> ChangeStateRoulette(Guid rouletteId, bool state);
        Task<List<RouletteDto>> GetAll();
        Task<bool> ValidRoulette(Guid rouletteId);
    }
}
