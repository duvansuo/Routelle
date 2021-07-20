using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.Repository.Interfaces;

namespace Test.Repository
{
    public class BetRepository : IBetRepository
    {
        private readonly IDatabase db;
        readonly string BetKey = Environment.GetEnvironmentVariable("BetKey");
        public BetRepository(IDatabase redis)
        {
            db = redis;
        }
        public async Task<bool> Create(BetDto betDto)
        {
            return await db.HashSetAsync(BetKey, betDto.BetId.ToString(), JsonConvert.SerializeObject(betDto));
        }
        public async Task<List<BetDto>> GetAll()
        {
            var result = await db.HashGetAllAsync(BetKey);
            List<BetDto> listBet = new List<BetDto>();
            foreach (var item in result)
            {
                listBet.Add(JsonConvert.DeserializeObject<BetDto>(item.Value));
            }
            return listBet;
        }
    }
}
