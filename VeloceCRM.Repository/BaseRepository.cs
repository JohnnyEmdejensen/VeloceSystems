using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class BaseRepository
    {
        public string LicenseKey { get; set; } = string.Empty;
        public ApiContext? ApiContext { get; set; }
    }
}
