using System;

namespace TimeOffTracker.Entities.Request_Entities
{
    public class Check
    {
        public int Id { get; set; }
        public string Manager { get; set; }//employee who should accept request
        public DateTimeOffset SolvedDate { get; set; }// date of decision
        public string Reason { get; set; }// reason of decision
        public string Status { get; set; }// status of request {requsted, in_progres, denied, accepted}

        public TimeOffRequest TimeOffRequest { get; set; }
    }
}
