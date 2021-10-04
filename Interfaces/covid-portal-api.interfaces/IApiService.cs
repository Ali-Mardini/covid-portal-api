using covid_portal_api.domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.interfaces
{
    public interface IApiService<T>
    {
        /// <summary>
        /// Method to build a response
        /// </summary>
        /// <param name="status">result status</param>
        /// <param name="responseBody">response body</param>
        /// <param name="message">response message</param>
        /// <returns>Api Response</returns>
        ApiResponse<T> BuildResponse(string status, List<T> responseBody, string message);

        /// <summary>
        /// Method to build a response
        /// </summary>
        /// <param name="status">result status</param>
        /// <param name="responseBody">response body</param>
        /// <param name="message">response message</param>
        /// <returns>Api Response</returns>
        ApiResponse<T> BuildResponse(string status, T responseBody, string message);
    }
}
