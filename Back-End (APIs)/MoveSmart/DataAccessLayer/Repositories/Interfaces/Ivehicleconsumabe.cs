using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface Ivehicleconsumabe
    {
        Task<ServiceResponse> AddVehicleConsumable(Vehicleconsumable consumables);
        Task<ServiceResponse> UpdateVehicleConsumable(Vehicleconsumable consumables);
        Task<ServiceResponse> DeleteVehicleConsumable(string ConsumableName);
        Task<List<Vehicleconsumable>> GetAllVehicleConsumable();
        Task<Vehicleconsumable> GetVehicleConsumableByName(string ConsumableName);
    }
}
