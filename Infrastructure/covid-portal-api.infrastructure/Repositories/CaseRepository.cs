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
    public class CaseRepository : ICrud<Case>
    {
        private readonly CovidContext _context;

        public CaseRepository(CovidContext context)
        {
            _context = context;
        }

        public async Task<Case> Create(Case entity)
        {
            await _context.AddAsync(entity);

            // save changes to database
            var result = await _context.SaveChangesAsync();

            // check if the changes has been saved
            if(result > 0)
            {
                // return created entity
                return entity;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<List<Case>> CreateAll(List<Case> entities)
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
           Case caseRecord = await _context.Cases.FirstOrDefaultAsync(x => x.CaseId == id);

            // remove the record and save the changes
            _context.Remove(caseRecord);
            var result = await _context.SaveChangesAsync();

            // check if the record has been deleted successfully
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Case> Read(Guid option)
        {
            return await _context.Cases.FirstOrDefaultAsync(x => x.CaseId == option);
        }

        public async Task<Case> Read(string option)
        {
            return await _context.Cases.Include(x => x.Country).FirstOrDefaultAsync(x => x.Country.CountryName == option);
        }

        public async Task<List<Case>> ReadAll(string option, DateTime startDate, DateTime endDate)
        {
            return await _context.Cases.Include(x => x.Country)
                .Where(x => x.Country.CountryName.ToLower() == option.ToLower() && x.CaseDate >= startDate && x.CaseDate <= endDate).ToListAsync();
        }

        public async Task<Case> Update(Case entity)
        {
            Case caseRecord = await _context.Cases.FirstOrDefaultAsync(x => x.CaseId == entity.CaseId);

            if(caseRecord != null)
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
