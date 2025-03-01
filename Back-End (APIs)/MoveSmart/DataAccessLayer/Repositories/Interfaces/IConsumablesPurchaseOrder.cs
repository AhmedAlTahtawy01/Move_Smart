using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
     public interface IConsumablesPurchaseOrder
    {
        Task<List<Consumablespurchaseorder>> GetAllConsumablesPurchaseOrder();
        Task<ServiceResponse> AddConsumablesPurchaseOrder(Consumablespurchaseorder order);
        Task<ServiceResponse> UpdateConsumablesPurchaseOrder(Consumablespurchaseorder order);
        Task<ServiceResponse> DeleteConsumablesPurchaseOrder(int OrderID);
        Task<Consumablespurchaseorder> GetConsumablesPurchaseOrderByID(int OrderID);
        Task ApproveRequestAsync(int OrderID);
        Task RejectRequestAsync(int OrderID);
        Task CancelRequestAsync(int OrderID);
        Task CompleteRequestAsync(int OrderID,DateTime endDate);

        Task UpdateStatusAsync(int orderId, string status);

        Task<List<Consumablespurchaseorder>> GetConsumablesPurchaseOrderByStatus(string Status);
        Task<List<Consumablespurchaseorder>> GetConsumablesPurchaseOrderByUser (int UserId);

    }
}
