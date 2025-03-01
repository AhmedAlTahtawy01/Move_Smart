using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    interface ISparepartsWithdrawApplication
    {
        Task<List<Sparepartswithdrawapplication>> GetAllSparepartsWithdrawApplication();
        Task<ServiceResponse> AddSparepartsWithdrawApplication(Sparepartswithdrawapplication order);
        Task<ServiceResponse> UpdateSparepartsWithdrawApplication(Sparepartswithdrawapplication order);
        Task<ServiceResponse> DeleteSparepartsWithdrawApplication(int OrderID);
        Task<Sparepartswithdrawapplication> GetSparepartsWithdrawApplication(int OrderID);
        Task ApproveRequestAsync(int OrderID);
        Task RejectRequestAsync(int OrderID);
        Task CancelRequestAsync(int OrderID);
        Task CompleteRequestAsync(int OrderID, DateTime endDate);

        Task UpdateStatusAsync(int orderId, string status);

        Task<List<Sparepartswithdrawapplication>> GetSparepartsWithdrawApplicationByStatus(string Status);
        Task<List<Sparepartswithdrawapplication>> GetSparepartsWithdrawApplicationByUser(int UserId);
    }
}
