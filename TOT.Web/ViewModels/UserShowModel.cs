using System;
using System.Collections.Generic;
using System.Text;
using TOT.Web.TagHelpers;

namespace TOT.Dto.Identity.Models
{
    public class UserShowModel
    {
        public IEnumerable<UserUpdateDTO> Users { get; set; }
        public UserFilterModel UserFilter { get; set; }
        public UserSortViewModel UserSortView { get; set; }
        public UserPageViewModel UserPageView { get; set; }
    }
}
