using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Repos
{
    public class vehicleconsumabeRepo : Ivehicleconsumabe
    {
        private readonly appDBContext _appDBContext;
        public vehicleconsumabeRepo(appDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task<ServiceResponse> AddVehicleConsumable(Vehicleconsumable consumables)
        {
            var check = await _appDBContext.Vehicleconsumables.FirstOrDefaultAsync(x=>x.ConsumableName==consumables.ConsumableName);    
            if (check != null)
            {
                return new ServiceResponse(false, "vehicle consumables is already exist!");
            }
            if (string.IsNullOrEmpty(consumables.ConsumableName) && consumables.ValidityLength == 0 && consumables.Quantity== 0)
            {
                return new ServiceResponse(false, "vehicle consumables cannot be null");
            }
            _appDBContext.Vehicleconsumables.Add(consumables);
            await _appDBContext.SaveChangesAsync();
            return new ServiceResponse(true, "Created Successfully!");

        }

        public async Task<ServiceResponse> DeleteVehicleConsumable(string ConsumableName)
        {
            var counsume= await _appDBContext.Vehicleconsumables.AsNoTracking().FirstOrDefaultAsync(st => st.ConsumableName== ConsumableName);
            if (counsume == null)
            {
                return new ServiceResponse(false, "Cannot Be Null");
            }
            _appDBContext.Vehicleconsumables.Remove(counsume);
            await _appDBContext.SaveChangesAsync();
            return new ServiceResponse(true, "Deleted Successfully");
        }

        public async Task<List<Vehicleconsumable>> GetAllVehicleConsumable()
        {
            return await _appDBContext.Vehicleconsumables.AsNoTracking().ToListAsync();
        }
        

        public async Task<Vehicleconsumable> GetVehicleConsumableByName(string ConsumableName)
        {
            return await _appDBContext.Vehicleconsumables.AsNoTracking().FirstAsync(id => ConsumableName == id.ConsumableName);
        }

        public async Task<ServiceResponse> UpdateVehicleConsumable(Vehicleconsumable consumables)
        {
            _appDBContext.Vehicleconsumables.Update(consumables);
            await _appDBContext.SaveChangesAsync();
            return new ServiceResponse(true, "Updated Successfully!");
        }

    }
}
