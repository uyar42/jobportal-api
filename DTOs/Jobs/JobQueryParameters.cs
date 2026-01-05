namespace JobPortal.API.DTOs
{
    public class JobQueryParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Title { get; set; }
        public string? Company { get; set; }

        public string? SortBy { get; set; } // salary | date
        public bool Desc { get; set; } = true;
    }
}
