using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IConsumablesWithdrawApplication
    {
        Task<ServiceResponse> UpdateConsumablesWithdrawApplication(Consumableswithdrawapplication order);
        Task<ServiceResponse> AddConsumablesWithdrawApplication(Consumableswithdrawapplication order);
        Task<List<Consumableswithdrawapplication>> GetAllConsumablesWithdrawApplication();
        Task<ServiceResponse> DeleteConsumablesWithdrawApplication(int WithdrawApplicationID);
        Task<Consumableswithdrawapplication> GetConsumablesWithdrawApplicationByID(int WithdrawApplicationID);
        Task ApproveRequestAsync(int WithdrawApplicationID);
        Task RejectRequestAsync(int WithdrawApplicationID);
        Task CancelRequestAsync(int WithdrawApplicationID);
        Task CompleteRequestAsync(int WithdrawApplicationID, DateTime endDate);



    }
}
