using System;
using System.Collections.Generic;
using System.Text;
using TOT.Dto.TimeOffRequests;
using TOT.Web.TagHelpers;

namespace TOT.Dto.Identity.Models
{
    public class RequestShowModel
    {
        public IEnumerable<TimeOffRequestDTO> Requests { get; set; }
        public TimeOffRequestFilterModel RequestFilter { get; set; }
    }
}
