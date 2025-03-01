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
    public class sparepartRepo :Isparepart
    {
        private readonly appDBContext _appDBContext;
        public sparepartRepo(appDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task<ServiceResponse> AddSparePart(Sparepart spare)
        {
            var check = await _appDBContext.Spareparts.FirstOrDefaultAsync(x=>x.PartName ==spare.PartName);
            if (check != null) 
            {
                return new ServiceResponse(false, "spare part is already exist!");
            }
            if (string.IsNullOrEmpty(spare.PartName) && spare.ValidityLength == 0 && spare.Quantity == 0)
            {
                return new ServiceResponse(false, "spare part cannot be null");
            }
            _appDBContext.Spareparts.Add(spare);
            await _appDBContext.SaveChangesAsync();
            return new ServiceResponse(true, "Created Successfully!");


        }

        public async Task<ServiceResponse> UpdateSparePart(Sparepart spare)
        {
            _appDBContext.Spareparts.Update(spare);
            await _appDBContext.SaveChangesAsync();
            return new ServiceResponse(true, "Updated Successfully!");
        }
        public async Task<ServiceResponse> DeleteSparePart(string PartName)
        {
            var sparepart = await _appDBContext.Spareparts.AsNoTracking().FirstOrDefaultAsync(id => id.PartName == PartName);
            if (sparepart == null)
            {
                return new ServiceResponse(false, "Cannot Be Null");
            }
            _appDBContext.Spareparts.Remove(sparepart);
            await _appDBContext.SaveChangesAsync();
            return new ServiceResponse(true, "Deleted Successfully");
        }

        public async Task<List<Sparepart>> GetAllSparePart()
        {
            return await _appDBContext.Spareparts.AsNoTracking().ToListAsync();
        }

        public async Task<Sparepart> GetSparePartByName(string PartName)
        {
            return await _appDBContext.Spareparts.AsNoTracking().FirstAsync(id => PartName == id.PartName);
        }

        public async Task<ServiceResponse> UpdateByNameSparePart(string PartName, Sparepart spare)
        {
            var existing= await _appDBContext.Spareparts.AsNoTracking().FirstAsync(k=>k.PartName ==PartName);
            if (existing== null)
            {
                return new ServiceResponse(false , "ya 3m da null");
            }
            existing.Quantity = spare.Quantity;
            existing.ValidityLength = spare.ValidityLength;
            existing.PartName = spare.PartName;
            
            //_appDBContext.spareparts.Update(existing);
            await _appDBContext.SaveChangesAsync();
            return new ServiceResponse(true, "updated successfully!");
        }
    }
}
