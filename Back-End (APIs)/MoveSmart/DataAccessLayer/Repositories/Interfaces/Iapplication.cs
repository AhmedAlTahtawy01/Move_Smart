using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
     public interface Iapplication
    {
        Task<ServiceResponse> AddApplication(Application app);
        Task<ServiceResponse> UpdateApplication(Application app);
        Task<ServiceResponse> DeleteApplication(int ApplicationID);
        Task<List<Application>> GetAllApplication();
        //Task<application> GetApplicationByID(int ApplicationID);
    }
}
