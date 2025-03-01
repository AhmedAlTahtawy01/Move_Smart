using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoveSmartAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly Iapplication _iapplication;
        public ApplicationController(Iapplication iapplication)
        {
            _iapplication = iapplication;
        }
       [HttpPut]
        public async Task<IActionResult> UpdateApplication(Application app)
        {
            var data = await _iapplication.UpdateApplication(app);
            return Ok(data);

        }
    }
}
