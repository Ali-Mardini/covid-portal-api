using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.domain.DTO
{
    public class CountryHistroyDto
    {
        public string ID { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public long Confirmed { get; set; }
        public long Deaths { get; set; }
        public long Recovered { get; set; }
        public long Active { get; set; }
        public DateTime Date { get; set; }
    }
}
