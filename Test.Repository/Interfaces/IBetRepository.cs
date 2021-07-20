using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Repository.Interfaces
{
    public interface IBetRepository
    {
        Task<bool> Create(BetDto betDto);
        Task<List<BetDto>> GetAll();
    }
}
