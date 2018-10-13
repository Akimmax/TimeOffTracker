using System;
using System.Collections.Generic;

namespace TimeOffTracker.Entities.Request_Entities
{
    public class TimeOffRequest
    {
        public TimeOffRequest()
        {
            Checks = new List<Check>();
        }
        public int Id { get; set; }
        public string User { get; set; } //string type need to be changed on User
        public TimeOffType TimeOffType { get; set; }//string type should be changed on TimeOfType
        public DateTimeOffset StartTimeOffDate { get; set; }// date of starting timeoff
        public DateTimeOffset EndTimeOffDate { get; set; }// date of timeoff ending
        public string Note { get; set; }// extra information
        public ICollection<Check> Checks { get; set; }
    }
}
