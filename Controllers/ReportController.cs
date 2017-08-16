using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;

namespace TechTime.Controllers
{
    public class ReportController : Controller
    {
        private IRecordRepository _repo;
        private ILogger<ReportController> _logger;
        private IAuthorizationService _authService;
        private UserManager<UserLogin> _userManager;

        public ReportController(IRecordRepository repo, 
            ILogger<ReportController> logger, 
            IAuthorizationService authService, 
            UserManager<UserLogin> userManager)
        {
            _repo = repo;
            _logger = logger;
            _authService = authService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> JobDetails(int id)
        {
            var model = _repo.GetJobEntries().FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                var authResult = await _authService.AuthorizeAsync(User, model, Constants.View);
                if (authResult)
                {
                    return View(model);
                }

                return Unauthorized();
            }

            return NotFound();

        }
    }
}
