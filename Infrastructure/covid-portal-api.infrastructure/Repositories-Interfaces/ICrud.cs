using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.infrastructure.Repositories_Interfaces
{
    public interface ICrud<T>
    {
        /// <summary>
        /// Method to Create new record
        /// </summary>
        /// <param name="entity">The entity object</param>
        /// <returns>Created object</returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Method to create new list of records
        /// </summary>
        /// <param name="entities">List of entites</param>
        /// <returns>Created List</returns>
        Task<List<T>> CreateAll(List<T> entities);

        /// <summary>
        /// Method to update a record 
        /// </summary>
        /// <param name="entity">The entity object</param>
        /// <returns>Updated object</returns>
        Task<T> Update(T entity);

        /// <summary>
        /// Method to delete a record by Id
        /// </summary>
        /// <param name="id">record Id</param>
        /// <returns>True or False</returns>
        Task<bool> Delete(Guid id);

        /// <summary>
        /// Method to get covid data by Id 
        /// </summary>
        /// <param name="option">Parameter option(Id)</param>
        /// <returns>Record</returns>
         Task<T> Read(Guid option);

        /// <summary>
        /// Method to get list of data
        /// </summary>
        /// <param name="option">Parameter option(countryName)</param>
        /// <param name="startDate">start date filter</param>
        /// <param name="endDate">end date filter</param>
        /// <returns>List of data</returns>
        Task<List<T>> ReadAll(string option, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Method to get covid data by counrty 
        /// </summary>
        /// <param name="option">Parameter option(counrtyName)</param>
        /// <returns>Record</returns>
        Task<T> Read(string option);
    }
}
