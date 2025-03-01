using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Repos
{
    class SparepartsWithdrawApplicationRepo : ISparepartsWithdrawApplication
    {
        public Task<ServiceResponse> AddSparepartsWithdrawApplication(Consumablespurchaseorder order)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> AddSparepartsWithdrawApplication(Sparepartswithdrawapplication order)
        {
            throw new NotImplementedException();
        }

        public Task ApproveRequestAsync(int OrderID)
        {
            throw new NotImplementedException();
        }

        public Task CancelRequestAsync(int OrderID)
        {
            throw new NotImplementedException();
        }

        public Task CompleteRequestAsync(int OrderID, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteSparepartsWithdrawApplication(int OrderID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Sparepartswithdrawapplication>> GetAllSparepartsWithdrawApplication()
        {
            throw new NotImplementedException();
        }

        public Task<Sparepartswithdrawapplication> GetSparepartsWithdrawApplication(int OrderID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Sparepartswithdrawapplication>> GetSparepartsWithdrawApplicationByStatus(string Status)
        {
            throw new NotImplementedException();
        }

        public Task<List<Sparepartswithdrawapplication>> GetSparepartsWithdrawApplicationByUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task RejectRequestAsync(int OrderID)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateSparepartsWithdrawApplication(Consumablespurchaseorder order)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateSparepartsWithdrawApplication(Sparepartswithdrawapplication order)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatusAsync(int orderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
