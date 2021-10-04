using covid_portal_api.api.Helpers;
using covid_portal_api.domain.Entities;
using covid_portal_api.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace covid_portal_api.api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CovidCasesController : ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly ApiClient _apiClient;
        private readonly IApiService<Case> _apiService;
        private readonly IApiService<string> _apiSyncService;
        private readonly ILogger<CovidCasesController> _logger;

        public CovidCasesController(ICaseService caseService, ApiClient apiClient, IApiService<Case> apiService, 
            IApiService<string> apiSyncService, ILogger<CovidCasesController> logger)
        {
            _caseService = caseService;
            _apiClient = apiClient;
            _apiService = apiService;
            _apiSyncService = apiSyncService;
            _logger = logger;
        }


        /// <summary>
        /// Method to get covid summary
        /// </summary>
        /// <returns>covid summary</returns>
        [HttpGet("covid-summary")]
        public async Task<IActionResult> GetCovidSummary()
        {
            try
            {
                // read global data from database
                var result = await _caseService.Read("Global");

                // check if the result is not null
                if (result != null)
                {
                    var response = _apiService.BuildResponse("true", result, "");
                    return Ok(response);
                }
                else
                {
                    var response = _apiService.BuildResponse("true", new List<Case>(), "No Result Found!");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                var response = _apiService.BuildResponse("false", new List<Case>(), ex.Message);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Method to get covid summary based on country name and a timespan
        /// </summary>
        /// <param name="counrtyName">Country name</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <returns>list of data</returns>
        [HttpGet("history/{countryName}")]
        public async Task<IActionResult> GetCountrySummary(string countryName, DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _caseService.ReadAll(countryName, startDate, endDate);

                if(result != null)
                {
                    var response = _apiService.BuildResponse("true", result, "");
                    return Ok(response);
                }
                else
                {
                    var response = _apiService.BuildResponse("true", new List<Case>(), "No Result Found!");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                var response = _apiService.BuildResponse("false", new List<Case>(), ex.Message);
                return BadRequest(response);
            }
        }


        /// <summary>
        /// Method to sync database with covid data
        /// </summary>
        /// <returns>the status of database </returns>
        [HttpGet("sync-data")]
        public async Task<IActionResult> SyncData()
        {
            try
            {
                var summaryData = await _apiClient.GetCovidSummary();

                var result = await _caseService.SyncData(summaryData);

                if (result)
                {
                    var response = _apiSyncService.BuildResponse("true", new List<string>(), "Your Database is synced!");
                    return Ok(response);
                }
                else
                {
                    var response = _apiSyncService.BuildResponse("false", new List<string>(), "Something went wrong!");
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                var response = _apiSyncService.BuildResponse("false", new List<string>(), ex.Message);
                return BadRequest(response);
            }
        }
    }
}
