using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Test.Extensions;
using Test.Models;
using Test.Repository.Interfaces;
namespace Test.Repository
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IDatabase db;
        readonly string RouletteKey = Environment.GetEnvironmentVariable("RouletteKey");
        public RouletteRepository(IDatabase redis)
        {
            db = redis;
        }
        public async Task<bool> Create(RouletteDto rouletteDto)
        {
            return await db.HashSetAsync(RouletteKey, rouletteDto.RouletteId.ToString(), JsonConvert.SerializeObject(rouletteDto));
        }
        public async Task<List<RouletteDto>> GetAll()
        {
            var result = await db.HashGetAllAsync(RouletteKey);
            List<RouletteDto> listRouletteDto = new List<RouletteDto>();
            foreach (var item in result)
            {
                listRouletteDto.Add(JsonConvert.DeserializeObject<RouletteDto>(item.Value));
            }
            return listRouletteDto;
        }
        public async Task<RouletteDto> GetById(Guid rouletteId)
        {
            var result = await db.HashGetAsync(RouletteKey, rouletteId.ToString());
            if (string.IsNullOrEmpty(result))
                throw new Exception($"{Constants.EXISTROULETTE}{rouletteId}");
            return JsonConvert.DeserializeObject<RouletteDto>(result);
        }


    }
}
