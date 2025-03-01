using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Repos
{

   public class applicationRepo : Iapplication
    {
        private readonly appDBContext _appDBContext;
        public applicationRepo(appDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public Task<ServiceResponse> AddApplication(Application app)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteApplication(int ApplicationID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Application>> GetAllApplication()
        {
            throw new NotImplementedException();
        }

        public Task<Application> GetApplication(int ApplicationID)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> UpdateApplication(Application app)
        {
            
            _appDBContext.Applications.Update(app);
            await _appDBContext.SaveChangesAsync();
            return new ServiceResponse(true, "Updattted Successfully!");

        }
    }
}
