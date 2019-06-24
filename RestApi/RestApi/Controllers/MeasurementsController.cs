using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Contexts;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    PM1 = 1,
                    PM10 = 10,
                    PM25 = 2.5,
                    Preasure = 1028.00,
                    Damp = 45,
                    DeviceId = 1
                };

                _Context.Measurements.Add(measurement);

                _Context.SaveChanges();
            }
        }

        #endregion

        #region Methods

        #region GET

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetMeasurements()
        {
            return await _Context.Measurements.ToListAsync();
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



        //[HttpPost("{Id}")]
        //public async Task<ActionResult<Device>> EditDevice(int id, Device device)
        //{
        //    if (id != device.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _Context.Entry(device).State = EntityState.Modified;
        //    await _Context.SaveChangesAsync();

        //    return NoContent();

        //}

        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult<Measurement>> PostMeasurement(Measurement measurement)
        {
            _Context.Measurements.Add(measurement);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMeasurement), new { id = measurement.Id }, measurement);
        }
        #endregion

        //#region DELETE

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDevice(int id)
        //{
        //    var device = await _Context.Devices.FindAsync(id);

        //    if (device == null)
        //    {
        //        return NotFound();
        //    }

        //    _Context.Devices.Remove(device);
        //    await _Context.SaveChangesAsync();

        //    return NoContent();
        //}

        //#endregion

        #endregion

    }
}
