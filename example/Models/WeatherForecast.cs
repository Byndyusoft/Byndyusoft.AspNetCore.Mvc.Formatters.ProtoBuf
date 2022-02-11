using System;
using System.Runtime.Serialization;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.ProtoBuf.Models
{
    /// <summary>
    ///     WeatherForecast
    /// </summary>
    [DataContract]
    public class WeatherForecast
    {
        /// <summary>
        ///     Date
        /// </summary>
        [DataMember] public DateTime Date { get; set; }

        /// <summary>
        ///     TemperatureC
        /// </summary>
        [DataMember] public int TemperatureC { get; set; }

        /// <summary>
        ///     TemperatureF
        /// </summary>
        [DataMember] public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        ///   Summary  
        /// </summary>
        [DataMember] public string Summary { get; set; } = default!;
    }
}
