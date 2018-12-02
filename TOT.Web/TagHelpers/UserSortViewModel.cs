using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOT.Dto.Identity.Models;

namespace TOT.Web.TagHelpers
{
    public class UserSortViewModel
    {
        public UserSortState NameSort { get; set; }
        public UserSortState SurnameSort { get; set; }
        public UserSortState PatronymicSort { get; set; }
        public UserSortState EmailSort { get; set; }
        public UserSortState HireDateSort { get; set; }
        public UserSortState RolesSort { get; set; }
        public UserSortState FiredSort { get; set; }
        public UserSortState PositionSort { get; set; }
        public UserSortState Current { get; set; }
        public UserSortState SortedBy { get; set; }
        public bool Up { get; set; }

        public UserSortViewModel(UserSortState sortOrder)
        {
            NameSort = UserSortState.NameAsc;
            SurnameSort = UserSortState.SurnameAsc;
            PatronymicSort = UserSortState.PatronymicAsc;
            HireDateSort = UserSortState.HireDateAsc;
            FiredSort = UserSortState.FiredAsc;
            EmailSort = UserSortState.EmailAsc;
            PositionSort = UserSortState.PositionAsc;
            Up = true;

            if (sortOrder == UserSortState.EmailDesc || sortOrder == UserSortState.NameDesc
                || sortOrder == UserSortState.SurnameDesc || sortOrder == UserSortState.PatronymicDesc
                || sortOrder == UserSortState.PositionDesc || sortOrder == UserSortState.FiredDesc
                || sortOrder == UserSortState.HireDateDesc
                )
            {
                Up = false;
            }

            Current = sortOrder;

            switch (sortOrder)
            {
                case UserSortState.NameDesc:
                    SortedBy = NameSort = UserSortState.NameAsc;
                    break;
                case UserSortState.SurnameAsc:
                    SortedBy = SurnameSort = UserSortState.SurnameDesc;
                    break;
                case UserSortState.SurnameDesc:
                    SortedBy = SurnameSort = UserSortState.SurnameAsc;
                    break;
                case UserSortState.PatronymicAsc:
                    SortedBy = PatronymicSort = UserSortState.PatronymicDesc;
                    break;
                case UserSortState.PatronymicDesc:
                    SortedBy = PatronymicSort = UserSortState.PatronymicAsc;
                    break;
                case UserSortState.EmailAsc:
                    SortedBy = EmailSort = UserSortState.EmailDesc;
                    break;
                case UserSortState.EmailDesc:
                    SortedBy = EmailSort = UserSortState.EmailAsc;
                    break;
                case UserSortState.HireDateAsc:
                    SortedBy = HireDateSort = UserSortState.HireDateDesc;
                    break;
                case UserSortState.HireDateDesc:
                    SortedBy = HireDateSort = UserSortState.HireDateAsc;
                    break;
                case UserSortState.FiredDesc:
                    SortedBy = FiredSort = UserSortState.FiredAsc;
                    break;
                case UserSortState.FiredAsc:
                    SortedBy = FiredSort = UserSortState.FiredDesc;
                    break;
                case UserSortState.PositionAsc:
                    SortedBy = PositionSort = UserSortState.PositionDesc;
                    break;
                case UserSortState.PositionDesc:
                    SortedBy = PositionSort = UserSortState.PositionAsc;
                    break;
                default:
                    SortedBy = NameSort = UserSortState.NameDesc;
                    break;
            }
        }
    }
}
