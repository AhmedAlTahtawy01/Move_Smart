using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    interface IConsumablesReplacement
    {
        Task<ServiceResponse> UpdateConsumablesReplacement(Consumablesreplacement order);
        Task<ServiceResponse> AddConsumablesReplacement(Consumablesreplacement order);
        Task<List<Consumableswithdrawapplication>> GetAllConsumablesReplacement();
        Task<ServiceResponse> DeleteConsumablesReplacement(int ApplicationID);
        Task<Consumablesreplacement> GetConsumablesReplacementByID(int ApplicationID);
        Task ApproveRequestAsync(int WithdrawApplicationID);
        Task RejectRequestAsync(int WithdrawApplicationID);
        Task CancelRequestAsync(int WithdrawApplicationID);
        Task CompleteRequestAsync(int ApplicationID);
    }
}
