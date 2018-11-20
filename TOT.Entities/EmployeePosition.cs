namespace TOT.Entities
{
    public class EmployeePosition
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool Equals(EmployeePosition other)
        {
            return Title.Equals(other.Title);
        }
    }

    public enum EmployeePositionEnum : int
    {
        Admin = 1,
        Employee = 2
    }
}
