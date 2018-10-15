using System;

namespace TOT.Entities.Request_Entities
{
    public class Check
    {
        public int Id { get; set; }
        public string UserId { get; set; }//employee who should accept request
        public DateTime SolvedDate { get; set; }// date of decision
        public string Reason { get; set; }// reason of decision
        public RequestStatus Status { get; set; }// status of request {requsted, in_progres, denied, accepted}

        public TimeOffRequest TimeOffRequest { get; set; }
    }
}
