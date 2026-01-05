namespace JobPortal.API.DTOs
{
    public class JobCreateDto
    {
        public required string Title { get; set; }
        public required string Company { get; set; }
        public decimal Salary { get; set; }
    }
}
