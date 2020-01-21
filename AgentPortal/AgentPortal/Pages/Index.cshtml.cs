using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _env;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public bool ShouldIncludeEs2015Modules()
        {
            var contents = _env.WebRootFileProvider.GetDirectoryContents("js/portalClient");

            if (!contents.Exists)
            {
                return false;
            }

            var hasEs2015Modules = contents.Any(asset => Path.GetFileName(asset.PhysicalPath).Contains("es2015"));
            return hasEs2015Modules;
        }
    }
}
