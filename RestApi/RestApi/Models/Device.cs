using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class Device
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MAC { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        public bool IsVerify { get; set; } = false;
        public virtual ICollection<Measurement> Measurements { get; set; }

    }
}
