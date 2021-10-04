using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.domain.DTO
{
    public class CovidSummaryDto
    {
        public Guid ID { get; set; }
        public string Message { get; set; }
        public Global Global { get; set; }
        public List<CountryDto> Countries { get; set; }
        public DateTime Date { get; set; }

    }

    public class CountryDto
    {
        public Guid ID { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public long NewConfirmed { get; set; }
        public long TotalConfirmed { get; set; }
        public long NewDeaths { get; set; }
        public long TotalDeaths { get; set; }
        public long NewRecovered { get; set; }
        public long TotalRecovered { get; set; }
        public DateTime Date { get; set; }
        public Premium Premium { get; set; }
    }

    public class Premium
    {
    }

    public class Global
    {
        public long NewConfirmed { get; set; }
        public long TotalConfirmed { get; set; }
        public long NewDeaths { get; set; }
        public long TotalDeaths { get; set; }
        public long NewRecovered { get; set; }
        public long TotalRecovered { get; set; }
        public DateTime Date { get; set; }
    }
}
