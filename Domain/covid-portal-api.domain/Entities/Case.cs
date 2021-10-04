using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.domain.Entities
{
    public class Case
    {
        public Guid CaseId { get; set; }
        public long NewConfirmed { get; set; }
        public long TotalConfirmed { get; set; }
        public long NewDeaths { get; set; }
        public long TotalDeaths { get; set; }
        public long NewRecovered { get; set; }
        public long TotalRecovered { get; set; }
        public DateTime CaseDate { get; set; }
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
    }
}
