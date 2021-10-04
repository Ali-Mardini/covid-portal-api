using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.domain.DTO
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }

        public List<T> Response { get; set; }

        public string Message { get; set; }

    }
}
