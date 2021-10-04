using covid_portal_api.domain.DTO;
using covid_portal_api.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace covid_portal_api.interfaces
{
    public interface ICaseService
    {
        /// <summary>
        /// Method to Create new record
        /// </summary>
        /// <param name="entity">The entity object</param>
        /// <returns>Created object</returns>
        public Task<Case> InsertNewCase(Case entity);

        /// <summary>
        /// Method to update a record 
        /// </summary>
        /// <param name="entity">The entity object</param>
        /// <returns>Updated object</returns>
        public Task<Case> Update(Case entity);

        /// <summary>
        /// Method to delete a record by Id
        /// </summary>
        /// <param name="id">record Id</param>
        /// <returns>True or False</returns>
        public Task<bool> DeleteCase(Guid id);

        /// <summary>
        /// Method to get covid data by Id 
        /// </summary>
        /// <param name="option">Paramete option(Id)</param>
        /// <returns>Record</returns>
        public Task<Case> Read(Guid option);

        /// <summary>
        /// Method to get covid data by counrty 
        /// </summary>
        /// <param name="option">Paramete option(counrtyName)</param>
        /// <returns>Record</returns>
        public Task<Case> Read(string option);

        /// <summary>
        /// Method to get list of data
        /// </summary>
        /// <param name="option">Parameter option(countryName)</param>
        /// <param name="startDate">start date filter</param>
        /// <param name="endDate">end date filter</param>
        /// <returns>List of data</returns>
        Task<List<Case>> ReadAll(string option, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Method to feed data in database
        /// </summary>
        /// <returns>True or false</returns>
        Task<bool> SyncData(CovidSummaryDto data);
    }
}
