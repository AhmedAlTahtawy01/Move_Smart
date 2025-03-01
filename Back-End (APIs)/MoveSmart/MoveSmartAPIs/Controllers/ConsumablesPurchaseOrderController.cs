using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoveSmartAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumablesPurchaseOrderController : ControllerBase
    {
        private readonly IConsumablesPurchaseOrder _iconsumablespurchaseorder;
        public ConsumablesPurchaseOrderController(IConsumablesPurchaseOrder iconsumablespurchaseorder)
        {
            _iconsumablespurchaseorder = iconsumablespurchaseorder;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllConsumablesPurchaseOrder()
        {
            var data = await _iconsumablespurchaseorder.GetAllConsumablesPurchaseOrder();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddConsumablesPurchaseOrder([FromBody]Consumablespurchaseorder order )
        {
            var data = await _iconsumablespurchaseorder.AddConsumablesPurchaseOrder(order);
            return Ok(data);
        }
        //[HttpPost("{id}/approve")]
        //public async Task<IActionResult> ApproveRequest(int id)
        //{
        //    try
        //    {
        //        await _iconsumablespurchaseorder.ApproveRequestAsync(id);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectRequest(int id)
        {
            try
            {
                await _iconsumablespurchaseorder.RejectRequestAsync(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("{id}/complete")]
        //public async Task<IActionResult> CompleteRequest(int id, )
        //{
        //    try
        //    {
        //        await _iconsumablespurchaseorder.CompleteRequestAsync(id,   );
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelRequestAsync(int id)
        {
            try
            {
                await _iconsumablespurchaseorder.CancelRequestAsync(id);
                return Ok();

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("{id},{status}/cancel")]
        public async Task<IActionResult> UpdateStatusAsync(int id , string status)
        {
            try
            {
                await _iconsumablespurchaseorder.UpdateStatusAsync(id, status);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("{UserId}/getByUser")]
        public async Task<IActionResult> GetConsumablesPurchaseOrderByUser(int UserId)
        {
            var data = await _iconsumablespurchaseorder.GetConsumablesPurchaseOrderByUser(UserId);
            return Ok(data);
        }
        [HttpGet("{Status}/getByStatus")]
        public async Task<IActionResult> GetConsumablesPurchaseOrderByStatus(string Status)
        {
            var data = await _iconsumablespurchaseorder.GetConsumablesPurchaseOrderByStatus(Status);
            return Ok(data);
        }
    }
}
