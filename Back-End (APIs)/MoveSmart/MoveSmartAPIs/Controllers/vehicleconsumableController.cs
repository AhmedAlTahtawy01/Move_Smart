using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class vehicleconsumableController : ControllerBase
    {
        private readonly Ivehicleconsumabe _ivehicleconsumabe;
        public vehicleconsumableController(Ivehicleconsumabe ivehicleconsumabe)
        {
            _ivehicleconsumabe = ivehicleconsumabe;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVehicleConsumable()
        {
            var data = await _ivehicleconsumabe.GetAllVehicleConsumable();
            return Ok(data);
        }
        [HttpGet("{ConsumableName}")]
        public async Task<IActionResult> GetSparePartByName(string ConsumableName)
        {
            var data = await _ivehicleconsumabe.GetVehicleConsumableByName(ConsumableName);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddVehicleConsumable([FromBody] Vehicleconsumable consume)
        {
            var data = await _ivehicleconsumabe.AddVehicleConsumable(consume);
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSparePart([FromBody] Vehicleconsumable consume)
        {
            var data = await _ivehicleconsumabe.UpdateVehicleConsumable(consume);
            return Ok(data);
        }
        [HttpDelete]
        [Route("{ConsumableName}")]
        public async Task<IActionResult> DeleteVehicleConsumable(string ConsumableName)
        {
            var data = await _ivehicleconsumabe.DeleteVehicleConsumable(ConsumableName);
            return Ok(data);
        }

    }
}
