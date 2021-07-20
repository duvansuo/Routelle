using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Test.Extensions;

namespace Test.Models
{
    public class BetDto
    {
        public Guid BetId { get; set; }
        [Range(minimum: 0, maximum: 36, ErrorMessage = Constants.ERRORNUMBER)]
        public short Number { get; set; }
        public string Color { get; set; }
        [Range(minimum: 1, maximum: 10000, ErrorMessage = Constants.ERRORAMOUNT)]
        public double Amount { get; set; }
        [Required]
        public Guid RouletteId { get; set; }
        public string UserId { get; set; }
    }
}
