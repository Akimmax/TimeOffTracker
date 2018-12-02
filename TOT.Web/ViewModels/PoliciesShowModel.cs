using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOT.Dto.TimeOffPolicies;
using TOT.Dto.TimeOffPolicies.Models;

namespace TOT.Web.ViewModels
{
    public class PoliciesShowModel
    {
        public IEnumerable<EmployeePositionTimeOffPolicyDTO> Policies { get; set; }
        public PolicyFilterModel PolicyFilter { get; set; }
    }
}
