namespace Logix.API.ViewModels
{
    public class AuthModel
    {
        public long UserId { get; set; }
        public int EmpId { get; set; }
        public long FacilityId { get; set; }
        public long FinYear { get; set; }
        public int GroupId { get; set; }
        public int Language { get; set; }
        public long PeriodId { get; set; }
        public string? OldBaseUrl { get; set; }
        public string? CoreBaseUrl { get; set; }
        public string? Token { get; set; }
    }
}
