using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public double PM1 { get; set; }
        public double PM25 { get; set; }
        public double PM10 { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string MAC { get; set; }
        //public int? DeviceId { get; set; }
        //[ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
    }
}
