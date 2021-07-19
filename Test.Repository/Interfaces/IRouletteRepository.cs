using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
namespace Test.Repository.Interfaces
{
    public interface IRouletteRepository
    {
        Task<bool> Create(RouletteDto rouletteDto);
        Task<RouletteDto> GetById(Guid rouletteId);
        Task<List<RouletteDto>> GetAll();
    }
}
