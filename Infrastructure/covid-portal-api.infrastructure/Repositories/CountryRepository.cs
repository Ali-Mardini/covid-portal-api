using covid_portal_api.domain.Entities;
using covid_portal_api.infrastructure.Data;
using covid_portal_api.infrastructure.Repositories_Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.infrastructure.Repositories
{
    public class CountryRepository : ICrud<Country>
    {
        private readonly CovidContext _context;

        public CountryRepository(CovidContext context)
        {
            _context = context;
        }

        public async Task<Country> Create(Country entity)
        {
            await _context.AddAsync(entity);

            // save changes to database
            var result = await _context.SaveChangesAsync();

            // check if the changes has been saved
            if (result > 0)
            {
                // return created entity
                return entity;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<List<Country>> CreateAll(List<Country> entities)
        {
            await _context.AddRangeAsync(entities);

            // save changes to database
            var result = await _context.SaveChangesAsync();

            // check if the changes has been saved
            if (result > 0)
            {
                // return created entity
                return entities;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            // get the record that need to delete
            Country countryRecord = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == id);

            // remove the record and save the changes
            _context.Remove(countryRecord);
            var result = await _context.SaveChangesAsync();

            // check if the record has been deleted successfully
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Country> Read(Guid option)
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == option);
        }

        public async Task<Country> Read(string option)
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.CountryName == option);
        }

        public Task<List<Country>> ReadAll(string option, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<Country> Update(Country entity)
        {
            Country countryRecord = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == entity.CountryId);

            if (countryRecord != null)
            {
                _context.Attach(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
