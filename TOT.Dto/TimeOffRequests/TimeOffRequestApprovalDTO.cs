﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TOT.Dto.TimeOffRequests
{
    public class TimeOffRequestApprovalDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Solved")]
        public DateTime? SolvedDate { get; set; }

        public string Reason { get; set; }

        public TimeOffRequestApprovalStatusesDTO Status { get; set; }

        [Display(Name = "Request")]
        public TimeOffRequestDTO TimeOffRequest { get; set; }

        public int TimeOffRequestId { get; set; }
    }
}