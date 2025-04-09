using WasteIntoCity.Core.Interfaces.Adapters;

namespace WasteIntoCity.Api.Adapters
{
    public class WebAppEnvironmentAdapter : IAppEnvironmentAdapter
    {
        private readonly IWebHostEnvironment _env;

        public WebAppEnvironmentAdapter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GetRootPath() => _env.WebRootPath;
    }
}
