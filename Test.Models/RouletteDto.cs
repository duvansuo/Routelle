using System;
namespace Test.Models
{
    public class RouletteDto
    {
        public Guid RouletteId { get; set; }
        public bool OpenState { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}
