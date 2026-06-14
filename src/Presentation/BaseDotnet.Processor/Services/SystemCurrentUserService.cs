using BaseDotnet.Application.Interfaces;

namespace BaseDotnet.Processor.Services
{
    public class SystemCurrentUserService : ICurrentUserService
    {
        public string? UserId => "BackgroundProcessor";
    }
}
