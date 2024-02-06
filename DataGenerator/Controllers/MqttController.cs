using DataGenerator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MqttController : ControllerBase
    {
        private readonly IMqttService _mqttService;

        public MqttController(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> send([FromQuery] string topic,
                                              [FromQuery] string message)
        {
            return await _mqttService.SendMessage(message, topic) ? Ok() : StatusCode(500, "Couldn't send message to MQTT service.");
        }

        [HttpPost("startgenerator")]
        public async Task<IActionResult> startGenerator(
                                                        [FromQuery] int minValue,
                                                        [FromQuery] int maxValue,
                                                        [FromQuery] int timeStamp)
        {
            try
            {
                var topics = new List<String>{"heartrate", "temperature", "respirationrate", "pressure" };
                foreach (var topic in topics)
                {
                    for (int i = 1; i < 5; i++) 
                    {
                        await _mqttService.StartGenerator(topic, i, minValue, maxValue, timeStamp);
                    }

                    timeStamp += 100;
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("stopgenerator")]
        public async Task<IActionResult> stopGenerator([FromQuery] string topic)
        {
            _mqttService.StopGenerator(topic);
            return Ok();
        }

    }
}
