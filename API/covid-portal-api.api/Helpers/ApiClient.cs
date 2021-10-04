using covid_portal_api.domain.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace covid_portal_api.api.Helpers
{
    public class ApiClient
    {
        private readonly HttpClient _apiClient;

        public ApiClient(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }


        /// <summary>
        /// Method to get covid summary data
        /// </summary>
        /// <returns>Covid summary Dto</returns>
        public async Task<CovidSummaryDto> GetCovidSummary()
        {
            var response = await _apiClient.GetAsync("summary");
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<CovidSummaryDto>(result);

            return null;
        }

        /// <summary>
        /// Method to get covid history data by country name for a specific period of time
        /// </summary>
        /// <param name="countryName">Country Name</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>List of Country History dto</returns>
        public async Task<List<CountryHistroyDto>> GetCovidHistoryByCountryName(string countryName, DateTime startDate, 
            DateTime endDate)
        {
            var response = await _apiClient.GetAsync($"country/{countryName}?from={startDate}&to={endDate}");
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<CountryHistroyDto>>(result);

            return null;
        }
    }
}
