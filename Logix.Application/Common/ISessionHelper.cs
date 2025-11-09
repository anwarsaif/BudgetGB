using Logix.Application.Extensions;
using Microsoft.AspNetCore.Http;

namespace Logix.Application.Common
{
    public interface ISessionHelper
    {
        bool ClearSession();
        T GetData<T>(string key);
        void AddData<T>(string key, T value);
        void SetMainData(long userId, int empId,int groupId, long facilityId, long finYear, int language, string branches,string calendartype,long branchId, string PermissionsOverAccAccountID);

        long UserId { get; }
        int EmpId { get; }
        long FacilityId { get; }
        long FinYear { get; }
        int GroupId { get; }
        int Language { get; }
        long PeriodId { get; }
        string Branches { get; }
        string OldBaseUrl { get; }
        string CoreBaseUrl { get; }
        string CalendarType { get; }
        long BranchId { get; }
        string PermissionsOverAccAccountID { get; }
    }

    public class SessionHelper : ISessionHelper
    {
        private readonly ISession _session;

        public long UserId => _session.GetData<long>("UserId");
        public int EmpId => _session.GetData<int>("EmpId");
        public long FacilityId => _session.GetData<long>("FacilityId");
        public long FinYear => _session.GetData<long>("FinYear");
        public int GroupId => _session.GetData<int>("GroupId");
        public int Language => _session.GetData<int>("Language");
        public string OldBaseUrl => _session.GetData<string>("OldBaseUrl");
        public string CoreBaseUrl => _session.GetData<string>("CoreBaseUrl");
        public long PeriodId => _session.GetData<long>("Period");
        public string Branches => _session.GetData<string>("Branches");
        public string CalendarType => _session.GetData<string>("CalendarType");
        public string PermissionsOverAccAccountID => _session.GetData<string>("PermissionsOverAccAccountID");

        public long BranchId => _session.GetData<long>("BranchId");
        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor != null)
            {
                if (httpContextAccessor.HttpContext != null)
                {
                    _session = httpContextAccessor.HttpContext.Session;
                }
            }
        }

        public T GetData<T>(string key)
        {
            return _session.GetData<T>(key);
        }
        
        public void AddData<T>(string key, T value)
        {
             _session.AddData<T>(key, value);
        }

        public void SetMainData(long userId, int empId, int groupId, long facilityId, long finYear, int language, string branches,string calendartype,long branchId,string PermissionsOverAccAccountID)
        {
            _session.AddData<long>("UserId", userId);
            _session.AddData<int>("EmpId", empId);
            _session.AddData<int>("GroupId", groupId);
            _session.AddData<long>("FacilityId", facilityId);
            _session.AddData<long>("FinYear", finYear);
            _session.AddData<int>("Language", language);
            _session.AddData<string>("Branches", branches);
            _session.AddData<string>("CalendarType", calendartype);
            _session.AddData<string>("PermissionsOverAccAccountID", PermissionsOverAccAccountID);
            _session.AddData<long>("BranchId", branchId);
        }

        public bool ClearSession()
        {
            try
            {
                _session.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
