namespace NZWalksAPI.Helpers
{
    public class WalkQueryObject
    {
        public string Name { get; set; } = string.Empty;
        public double LengthInKm { get; set; }
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
