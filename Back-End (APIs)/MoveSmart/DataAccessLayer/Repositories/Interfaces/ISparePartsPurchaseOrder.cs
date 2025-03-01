using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    interface ISparePartsPurchaseOrder
    {
        Task<List<Sparepartspurchaseorder>> GetAllSparePartsPurchaseOrder();
        Task<ServiceResponse> AddSparePartsPurchaseOrder(Sparepartspurchaseorder order);
        Task<ServiceResponse> UpdateSparePartsPurchaseOrder(Sparepartspurchaseorder order);
        Task<ServiceResponse> DeleteSparePartPurchaseOrder(int OrderID);
        Task<Sparepartspurchaseorder> GetConsumablesPurchaseOrderByID(int OrderID);
        Task ApproveRequestAsync(int OrderID);
        Task RejectRequestAsync(int OrderID);
        Task CancelRequestAsync(int OrderID);
        Task CompleteRequestAsync(int OrderID, DateTime endDate);

        Task UpdateStatusAsync(int orderId, string status);

        Task<List<Sparepartspurchaseorder>> GetSparePartsPurchaseOrderByStatus(string Status);
        Task<List<Sparepartspurchaseorder>> GetSparePartsPurchaseOrderByUser(int UserId);
    }
}
