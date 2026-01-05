namespace JobPortal.API.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
