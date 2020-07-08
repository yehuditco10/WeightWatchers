using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.Api.DTO
{
    public class Tracking
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public DateTime Date { get; set; }
        public float Weight { get; set; }
        public int Trand { get; set; }
        public float BMI { get; set; }
        public int Comments { get; set; }
    }
}
