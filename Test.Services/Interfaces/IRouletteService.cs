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
        Task<bool> OpenRoulette(Guid rouletteId);
        Task<List<RouletteDto>> GetAll();
        Task<bool> ValidRoulette(Guid rouletteId);
    }
}
