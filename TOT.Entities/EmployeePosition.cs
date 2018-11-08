namespace TOT.Entities
{
    public class EmployeePosition
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public enum EmployeePositionEnum : int
    {
        Employee = 1
    }
}
