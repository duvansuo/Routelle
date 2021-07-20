using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Models
{
    public class WinningBetDto : BetDto
    {
        public double? AmountEarned { get; set; }
        public string WonBy{ get; set; }
    }
}
