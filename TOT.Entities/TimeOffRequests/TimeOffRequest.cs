using System;
using System.Collections.Generic;

namespace TOT.Entities.TimeOffRequests
{
    public class TimeOffRequest
    {
        public TimeOffRequest()
        {
            Checks = new List<Check>();
        }
        public int Id { get; set; }
        public string User { get; set; } //employee who request time off
        public TimeOffType Type { get; set; }//Type of timeoff
        public DateTime StartsAt { get; set; }// date of starting timeoff
        public DateTime EndsOn { get; set; }// date of timeoff ending
        public string Note { get; set; }// extra information
        public ICollection<Check> Checks { get; set; }
    }
}
