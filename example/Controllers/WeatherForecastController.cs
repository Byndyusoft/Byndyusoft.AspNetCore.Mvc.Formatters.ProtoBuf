using System;
using System.Linq;
using Byndyusoft.AspNetCore.Mvc.Formatters.ProtoBuf.Models;
using Microsoft.AspNetCore.Mvc;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.ProtoBuf.Controllers
{
    /// <summary>
    ///     WeatherForecastController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        ///     Get
        /// </summary>
        [HttpGet]
        [FormatFilter]
        public IActionResult Get()
        {
            var rng = new Random();
            var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
            })
                .ToArray();

            return Ok(forecast);
        }
    }
}