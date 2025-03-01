using DataAccessLayer.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Repositories.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sparepartController : ControllerBase
    {
        private readonly Isparepart _isparepart;

        public sparepartController(Isparepart isparepart)
        {
             _isparepart =isparepart;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSparePart()
        {
            var data = await _isparepart.GetAllSparePart();
            return Ok(data);    
        }
        [HttpGet("{PartName}")]
        public async Task<IActionResult> GetSparePartByName(string PartName)
        {
            var data = await _isparepart.GetSparePartByName(PartName);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddSparePart([FromBody] Sparepart spare)
        {
            var data = await _isparepart.AddSparePart(spare);
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSparePart([FromBody] Sparepart spare)
        {
            var data = await _isparepart.UpdateSparePart(spare);
            return Ok(data);
        }
        [HttpDelete]
        [Route("{PartName}")]
        public async Task<IActionResult> DeleteSparePart(string PartName)
        {
            var data =await _isparepart.DeleteSparePart(PartName);
            return Ok(data);
        }
        [HttpPut("{PartName}")]
        public async Task<IActionResult> UpdateByNameSparePart(string PartName ,Sparepart spare ) 
        {
            var data = await _isparepart.UpdateByNameSparePart(PartName ,spare);
            return Ok(data);
        }
        
    }
}
 