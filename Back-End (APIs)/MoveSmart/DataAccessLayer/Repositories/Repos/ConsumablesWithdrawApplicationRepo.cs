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
    class ConsumablesWithdrawApplicationRepo : IConsumablesWithdrawApplication
    {
        public Task<ServiceResponse> AddConsumablesWithdrawApplication(Consumableswithdrawapplication order)
        {
            throw new NotImplementedException();
        }

        public Task ApproveRequestAsync(int WithdrawApplicationID)
        {
            throw new NotImplementedException();
        }

        public Task CancelRequestAsync(int WithdrawApplicationID)
        {
            throw new NotImplementedException();
        }

        public Task CompleteRequestAsync(int WithdrawApplicationID, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteConsumablesWithdrawApplication(int WithdrawApplicationID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Consumableswithdrawapplication>> GetAllConsumablesWithdrawApplication()
        {
            throw new NotImplementedException();
        }

        public Task<Consumableswithdrawapplication> GetConsumablesWithdrawApplicationByID(int WithdrawApplicationID)
        {
            throw new NotImplementedException();
        }

        public Task RejectRequestAsync(int WithdrawApplicationID)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateConsumablesWithdrawApplication(Consumableswithdrawapplication order)
        {
            throw new NotImplementedException();
        }
    }
}
