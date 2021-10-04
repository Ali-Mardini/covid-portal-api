using covid_portal_api.domain.DTO;
using covid_portal_api.domain.Entities;
using covid_portal_api.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.services
{
    public class ApiService<T> : IApiService<T>
    {
        public ApiResponse<T> BuildResponse(string status, List<T> responseBody, string message)
        {
            var responseList = new List<T>();

            responseList.AddRange(responseBody); 

            var response = new ApiResponse<T>
            {
                Status = status,
                Response = responseList,
                Message = message
            };

            return response;
        }

        public ApiResponse<T> BuildResponse(string status, T responseBody, string message)
        {
            var responseList = new List<T>();

            responseList.Add(responseBody);

            var response = new ApiResponse<T>
            {
                Status = status,
                Response = responseList,
                Message = message
            };

            return response;
        }
    }
}
