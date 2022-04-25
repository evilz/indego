

using System.Threading.Tasks;
using Refit;

namespace Indego.Client
{

    public record AuthenticateRequest(string device, string os_type, string os_version, string dvc_manuf, string dvc_type)
    {
        public static readonly AuthenticateRequest Default = new("", "Android", "4.0", "unknown", "unknown");
    };
    public record AuthenticateResponse(Guid ContextId, Guid UserId, string Alm_sn);

    public enum IndegoStates
    {
        ReadingStatus = 0,
        Charging = 257,
        Docked = 258,
        DockedSoftwareIpdate = 259,
        Docked2 = 260,
        Docked3 = 261,
        DockedLoadingMap = 262,
        DockedSavingMap = 263,
        Mowing = 513,
        Relocalising = 514,
        LoadingMap = 515,
        LearningLawn = 516,
        Paused = 517,
        BorderCut = 518,
        IdleInLawn = 519,
        ReturningToDock = 769,
        ReturningToDock2 = 770,
        ReturningToDockBatteryLow = 771,
        ReturningToDockCalendarTimeslotEnded = 772,
        ReturningToDockBatteryTempRange = 773,
        ReturningToDock3 = 774,
        ReturningToDockLawnComplete = 775,
        ReturningToDockRelocalising = 776,
        DiagnosticMode = 1025,
        EndOfLive = 1026,
        SoftwareUpdate = 1281,
    }

    public class StatusResponse
    {
        public IndegoStates State { get; set; }
        public long Mowed { get; set; }
        public long MowedTs { get; set; }
        public long Mowmode { get; set; }
        public long MapsvgcacheTs { get; set; }
        public Runtime Runtime { get; set; }
        public long Error { get; set; }
        public bool MapUpdateAvailable { get; set; }
    }

    public class Runtime
    {
        public Session Total { get; set; }
        public Session Session { get; set; }
    }

    public class Session
    {
        public long Operate { get; set; }
        public long Charge { get; set; }
    }


    public record Alert(string alm_sn, string alert_id, string headline, DateTime date, string message, string read_status, string flag);

    [Headers("Content-Type: application/json")]
    public interface IIndegoClient
    {
        [Post("/authenticate")]
        Task<ApiResponse<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request, [Header("Authorization")] string authorization);

        [Get("/alms/{serial}/state")]
        Task<ApiResponse<StatusResponse>> GetStatusAsync(string serial, [Header("x-im-context-id")] Guid ContextId);

        [Get("/alms/{serial}/map")]
        Task<ApiResponse<string>> GetMapAsync(string serial, [Header("x-im-context-id")] Guid ContextId);

        [Get("/alerts")]
        Task<ApiResponse<Alert[]>> GetAlertsAsync([Header("x-im-context-id")] Guid ContextId);

    }
}