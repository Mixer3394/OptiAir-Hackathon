using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Contexts;
using RestApi.Helpers;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : Controller
    {
        #region Fields

        private OptiAirDbContext _Context;

        #endregion

        #region Constructors

        public DevicesController(OptiAirDbContext context)
        {
            _Context = context;

            //if (_Context.Devices.Count() == 0)
            //{
            //    var device = new Device()
            //    {
            //        Longitude = 18.539444,
            //        Latitude = 54.519167,
            //        Name = "Initial",
            //        IsVerified = true,
            //        MAC = "00:0A:E6:3E:FD:E1"
            //    };



                //    _Context.Devices.Add(device);

                //    _Context.Devices.Add(new Device()
                //    {
                //        Longitude = 18.538709,
                //        Latitude = 54.521048,
                //        Name = "Initial1",
                //        IsVerified = true,
                //        MAC = "00:0A:E6:3E:FD:E0"
                //    });

                //    _Context.Devices.Add(new Device()
                //    {
                //        Longitude = 18.5419,
                //        Latitude = 54.517392,
                //        Name = "Initial2",
                //        IsVerified = true,
                //        MAC = "00:0A:E6:3E:FD:E2"
                //    });

                //    _Context.Devices.Add(new Device()
                //    {
                //        Longitude = 18.534997,
                //        Latitude = 54.517562,
                //        Name = "Initial3",
                //        IsVerified = true,
                //        MAC = "00:0A:E6:3E:FD:E3"
                //    });

                //    _Context.Devices.Add(new Device()
                //    {
                //        Longitude = 18.530847,
                //        Latitude = 54.521819,
                //        Name = "Initial4",
                //        IsVerified = true,
                //        MAC = "00:0A:E6:3E:FD:E4"
                //    });

                //    _Context.Devices.Add(new Device()
                //    {
                //        Longitude = 18.52824,
                //        Latitude = 54.518502,
                //        Name = "Initial4",
                //        IsVerified = true,
                //        MAC = "00:0A:E6:3E:FD:E5"
                //    });

                //    _Context.Devices.Add(new Device()
                //    {
                //        Longitude = 18.52224,
                //        Latitude = 54.512502,
                //        Name = "Morski",
                //        IsVerified = true,
                //        MAC = "d4:25:8b:e9:22:fd"
                //    });

            //    _Context.SaveChanges();
            //}
        }

        #endregion

        #region Methods

        #region GET

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            var devices = await _Context.Devices.ToListAsync();

            foreach(Device d in devices)
            {
                var measurements = _Context.Measurements.Where(m => m.MAC == d.MAC).ToList().TakeLast(1);
                d.Measurements = measurements.ToList();
            }
            return devices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _Context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        [HttpPost("between")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices([FromBody]DataBetween data)
        {
            DateTime date = DateTime.ParseExact(data.DateTime, "yyyyMMddHHmm",
                                          System.Globalization.CultureInfo.InvariantCulture);


            var devices = await _Context.Devices.ToListAsync();

            //var date = data.DateTime;

            foreach (Device d in devices)
            {
                var measurements = _Context.Measurements.Where(m => m.MAC == d.MAC).Where(m => (m.DateTime >= date.AddHours(-1)) && (m.DateTime <= date)).ToList().TakeLast(1);
                d.Measurements = measurements.ToList();
            }
            return devices.Where(x => x.Measurements.Count() > 0).ToList();
        }

        #endregion

        #region PUT

        [HttpPut("{MAC}"), Authorize]
        public async Task<ActionResult<Device>>  EditDevice(string mac, Device device)
        {
            if (mac != device.MAC)
            {
                return BadRequest();
            }

            _Context.Entry(device).State = EntityState.Modified;
            await _Context.SaveChangesAsync();

            return NoContent();

        }

        #endregion

        #region POST
        [HttpPost, Authorize]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            
            if (_Context.Devices.Where(d => d.MAC == device.MAC).ToList().Count() > 0)
            {
                BadRequestErrorMessageResult badRequestErrorMessageResult = new BadRequestErrorMessageResult($"This MAC address({device.MAC}) is already used");

                return BadRequest(badRequestErrorMessageResult);
            }

            if (Validator.MacValidate(device.MAC))
            {
                _Context.Devices.Add(device);
                await _Context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDevice), new { id = device.MAC }, device);
            }
            else
            {
                return BadRequest(new BadRequestErrorMessageResult($"Bad MAC address({device.MAC})"));
            }
          
        }
        #endregion

        #region DELETE

        [HttpDelete("{MAC}"), Authorize]
        public async Task<IActionResult> DeleteDevice(string mac)
        {
            var device = await _Context.Devices.FindAsync(mac);

            if (device == null)
            {
                return NotFound();
            }

            _Context.Devices.Remove(device);
            await _Context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #endregion
    }

    public class DataBetween
    {
        public string DateTime { get; set; }
    }
}
