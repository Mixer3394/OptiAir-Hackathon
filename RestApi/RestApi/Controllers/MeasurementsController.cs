using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Contexts;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : Controller
    {

        #region Fields

        private OptiAirDbContext _Context;

        #endregion

        #region Constructors

        public MeasurementsController(OptiAirDbContext context)
        {
            _Context = context;

            if (_Context.Measurements.Count() == 0)
            {
                var measurement = new Measurement()
                {
                    PM1 = 3,
                    PM10 = 5,
                    PM25 = 4,
                    Pressure = 1028.00,
                    Humidity = 90,
                    Temperature = 22,
                    MAC = "00:0A:E6:3E:FD:E1"                    
                };

                _Context.Measurements.Add(measurement);

                _Context.Measurements.Add(new Measurement()
                {
                    PM1 = 3,
                    PM10 = 6,
                    PM25 = 5,
                    Pressure = 1027.00,
                    Humidity = 89,
                    Temperature = 22,
                    MAC = "00:0A:E6:3E:FD:E0"
                });

                _Context.Measurements.Add(new Measurement()
                {
                    PM1 = 9,
                    PM10 = 18,
                    PM25 = 13,
                    Pressure = 1028.00,
                    Humidity = 90,
                    Temperature = 22,
                    MAC = "00:0A:E6:3E:FD:E1"
                });


                _Context.Measurements.Add(new Measurement()
                {
                    PM1 = 2,
                    PM10 = 3,
                    PM25 = 4,
                    Pressure = 1029.00,
                    Humidity = 88,
                    Temperature = 20,
                    MAC = "00:0A:E6:3E:FD:E2"
                });

                _Context.Measurements.Add(new Measurement()
                {
                    PM1 = 5,
                    PM10 = 4,
                    PM25 = 3,
                    Pressure = 1030.00,
                    Humidity = 92,
                    Temperature = 18,
                    MAC = "00:0A:E6:3E:FD:E3"
                });

                _Context.Measurements.Add(new Measurement()
                {
                    PM1 = 3,
                    PM10 = 4,
                    PM25 = 2.5,
                    Pressure = 1026.00,
                    Humidity = 90,
                    Temperature = 17,
                    MAC = "00:0A:E6:3E:FD:E4"
                });

                _Context.Measurements.Add(new Measurement()
                {
                    PM1 = 6,
                    PM10 = 4,
                    PM25 = 3,
                    Pressure = 1028.00,
                    Humidity = 84,
                    Temperature = 17,
                    MAC = "d4:25:8b:e9:22:fd"
                });

           

                _Context.SaveChanges();
            }
        }

        #endregion

        #region Methods

        #region GET

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetMeasurements()
        {
            var measurements = await _Context.Measurements.ToListAsync();

            foreach(Measurement measurement in measurements)
            {
                measurement.Device = _Context.Devices.Where(d => d.MAC == measurement.MAC).FirstOrDefault();
                
            }
            return measurements;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Measurement>> GetMeasurement(int id)
        {
            var measurement = await _Context.Measurements.FindAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }


            return measurement;
        }

        #endregion

        #region PUT

        [HttpPost("{Id}"), Authorize]
        public async Task<ActionResult<Device>> EditDevice(int id, Measurement measurement)
        {
            if (id != measurement.MeasurementId)
            {
                return BadRequest();
            }

            _Context.Entry(measurement).State = EntityState.Modified;
            await _Context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region POST
        
        [HttpPost,Authorize]
        public async Task<ActionResult<Measurement>> PostMeasurement(Measurement measurement)
        {
            if(_Context.Devices.Where(d=>d.MAC == measurement.MAC).ToList().Count() > 0)
            {
                _Context.Measurements.Add(measurement);
                await _Context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMeasurement), new { id = measurement.MeasurementId }, measurement);
            }
            else
            {
                return BadRequest(new BadRequestErrorMessageResult($"Unknown device ({measurement.MAC})"));
            }


        }
        #endregion

        #region DELETE

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            var measurement = await _Context.Measurements.FindAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }

            _Context.Measurements.Remove(measurement);
            await _Context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #endregion

    }
}
