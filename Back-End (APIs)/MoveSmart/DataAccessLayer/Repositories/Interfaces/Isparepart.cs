using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface Isparepart
    {
        Task<ServiceResponse> AddSparePart(Sparepart spare);
        Task<ServiceResponse> UpdateSparePart(Sparepart spare);
        Task<ServiceResponse> DeleteSparePart(string PartName);
        Task<List<Sparepart>> GetAllSparePart();
        Task<Sparepart> GetSparePartByName(string PartName);
        Task<ServiceResponse> UpdateByNameSparePart(string PartName,Sparepart sparepart); 
    }
}

//[HttpPut("{id}")]
//public async Task<IActionResult> PutAsync(Guid id, UpdateOrderDto updateItemDto)
//{
//    var existingOrder = await repository.GetAsync(id);
//    if (existingOrder == null)
//    {
//        return NotFound();
//    }
//    existingOrder.Address = updateItemDto.Address;
//    existingOrder.Quantity = updateItemDto.Quantity;
//    await repository.UpdateAsync(existingOrder);
//    return NoContent();
//}