using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.domain.Entities
{
    public class Country
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public ICollection<Case> Cases { get; set; }
    }
}
