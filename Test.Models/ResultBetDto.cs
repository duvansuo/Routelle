using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Models
{
    public class ResultBetDto
    {
        public List<BetDto> ListBetDto{ get; set; }
        public short WinningNumber { get; set; }
        public string WinningColor { get; set; }
        public List<WinningBetDto> ListWinningBetDto { get; set; }
    }
}
