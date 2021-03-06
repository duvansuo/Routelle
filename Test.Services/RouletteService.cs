using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Extensions;
using Test.Models;
using Test.Repository.Interfaces;
using Test.Services.Interfaces;

namespace Test.Services
{
    public class RouletteService : IRouletteService
    {
        readonly IRouletteRepository _IRouletteRepository;
        public RouletteService(IRouletteRepository repository)
        {
            _IRouletteRepository = repository;
        }
        public async Task<string> Create()
        {
            try
            {
                RouletteDto rouletteDto = new RouletteDto()
                {
                    RouletteId = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow
                };
                return await _IRouletteRepository.Create(rouletteDto) ? rouletteDto.RouletteId.ToString() : throw new Exception(Constants.ERRORCREATE);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RouletteDto>> GetAll()
        {
            return await _IRouletteRepository.GetAll();
        }

        public async Task<bool> ChangeStateRoulette(Guid rouletteId, bool State)
        {
            var exist = await _IRouletteRepository.GetById(rouletteId);
            if (exist != null)
            {
                if (exist.OpenState == State) return false;
                exist.OpenState = State;
                await _IRouletteRepository.Create(exist);
                return true;
            }
            throw new Exception(State ? Constants.ERROROPENROULETTE : Constants.ERRORCLOSEROULETTE);
        }

        public async Task<bool> ValidRoulette(Guid rouletteId)
        {
            RouletteDto rouletteDto = await _IRouletteRepository.GetById(rouletteId);
            return (rouletteDto.OpenState);
        }
    }
}
