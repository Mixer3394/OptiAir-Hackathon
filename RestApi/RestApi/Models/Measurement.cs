using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public double PM1 { get; set; }
        public double PM25 { get; set; }
        public double PM10 { get; set; }
        public double Temperature { get; set; }
        public double Damp { get; set; }
        public double Preasure { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
}
