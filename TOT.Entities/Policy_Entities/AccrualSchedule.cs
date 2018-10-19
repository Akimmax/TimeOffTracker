namespace TOT.Entities.Policy_Entities
{
    public class AccrualSchedule
    {
        public int Id { get; set; }
        public double TimeAmount { get; set; }//time counting back from hired date
        public TimeMeasures TimeMeasure { get; set; }//Measure (days,month,years) of time counting back from hired date
        public double AmmountAccruedTime { get; set; }//time in hours given as timeoffs 
        public string AmmountAccruedTimeDates { get; set; }// Json including dates of resets like {{-1,0},{-1,14}}, mean 1st day of every month
                                                            //and 15th day of every month
    }
}
