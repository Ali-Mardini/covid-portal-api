using covid_portal_api.domain.DTO;
using covid_portal_api.domain.Entities;
using covid_portal_api.infrastructure.Repositories_Interfaces;
using covid_portal_api.interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace covid_portal_api.services
{
    public class CaseService : ICaseService
    {
        private readonly ICrud<Case> _crud;
        private readonly ICrud<Country> _countryCrud;

        public CaseService(ICrud<Case> crud, ICrud<Country> countryCrud)
        {
            _crud = crud;
            _countryCrud = countryCrud;
        }

        public async Task<bool> DeleteCase(Guid id)
        {
            return await _crud.Delete(id);
        }

        public async Task<Case> InsertNewCase(Case entity)
        {
            return await _crud.Create(entity);
        }

        public async Task<Case> Read(Guid option)
        {
            return await _crud.Read(option);
        }

        public async Task<Case> Read(string option)
        {
            return await _crud.Read(option); 
        }

        public async Task<List<Case>> ReadAll(string option, DateTime startDate, DateTime endDate)
        {
            return await _crud.ReadAll(option, startDate, endDate);
        }

        public async Task<bool> SyncData(CovidSummaryDto data)
        {
            var global = await _countryCrud.Read("Global");

            if(global == null)
            {
                var globalCountry = new Country
                {
                    CountryId = Guid.NewGuid(),
                    CountryCode = "global",
                    CountryName = "Global",
                    Slug = "global"
                };

                await _countryCrud.Create(globalCountry);
            }
            

            List<Country> countries = new List<Country>();

            foreach (var item in data.Countries)
            {
                var countryData = await _countryCrud.Read(item.Country);

                if(countryData == null)
                {
                    var country = new Country
                    {
                        CountryId = Guid.NewGuid(),
                        CountryCode = item.CountryCode,
                        CountryName = item.Country,
                        Slug = item.Slug
                    };
                    countries.Add(country);
                }
                
            }

            // check if there is any new country to add
            if(countries.Count > 0)
            {
                await _countryCrud.CreateAll(countries);
            }
           
            

            List<Case> cases = new List<Case>();

            var isGlobalCaseExist = await _crud.Read("Global");

            if(isGlobalCaseExist != null)
            {
                if (isGlobalCaseExist.CaseDate == data.Date)
                {
                    var globCase = new Case
                    {
                        CaseId = Guid.NewGuid(),
                        CountryId = global.CountryId,
                        CaseDate = data.Global.Date,
                        NewConfirmed = data.Global.NewConfirmed,
                        Country = global,
                        NewDeaths = data.Global.NewDeaths,
                        NewRecovered = data.Global.NewRecovered,
                        TotalConfirmed = data.Global.TotalConfirmed,
                        TotalDeaths = data.Global.TotalDeaths,
                        TotalRecovered = data.Global.TotalRecovered
                    };

                    cases.Add(globCase);

                    foreach (var record in data.Countries)
                    {

                        var countryInfo = await _countryCrud.Read(record.Country);

                        Case caseInfo = new Case
                        {
                            CaseId = Guid.NewGuid(),
                            CountryId = countryInfo.CountryId,
                            CaseDate = record.Date,
                            NewConfirmed = record.NewConfirmed,
                            Country = countryInfo,
                            NewDeaths = record.NewDeaths,
                            NewRecovered = record.NewRecovered,
                            TotalConfirmed = record.TotalConfirmed,
                            TotalDeaths = record.TotalDeaths,
                            TotalRecovered = record.TotalRecovered
                        };

                        cases.Add(caseInfo);
                    }

                    var recordResult = await _crud.CreateAll(cases);

                    if (recordResult.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

                var globalCase = new Case
                {
                    CaseId = Guid.NewGuid(),
                    CountryId = global.CountryId,
                    CaseDate = data.Global.Date,
                    NewConfirmed = data.Global.NewConfirmed,
                    Country = global,
                    NewDeaths = data.Global.NewDeaths,
                    NewRecovered = data.Global.NewRecovered,
                    TotalConfirmed = data.Global.TotalConfirmed,
                    TotalDeaths = data.Global.TotalDeaths,
                    TotalRecovered = data.Global.TotalRecovered
                };

                cases.Add(globalCase);

                foreach (var record in data.Countries)
                {

                    var countryInfo = await _countryCrud.Read(record.Country);

                    Case caseInfo = new Case
                    {
                        CaseId = Guid.NewGuid(),
                        CountryId = countryInfo.CountryId,
                        CaseDate = record.Date,
                        NewConfirmed = record.NewConfirmed,
                        Country = countryInfo,
                        NewDeaths = record.NewDeaths,
                        NewRecovered = record.NewRecovered,
                        TotalConfirmed = record.TotalConfirmed,
                        TotalDeaths = record.TotalDeaths,
                        TotalRecovered = record.TotalRecovered
                    };

                    cases.Add(caseInfo);
                }

                var caseResult = await _crud.CreateAll(cases);

                if (caseResult.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

        }

        public async Task<Case> Update(Case entity)
        {
            return await _crud.Update(entity);
        }
    }
}
