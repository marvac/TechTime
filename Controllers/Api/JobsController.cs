using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;
using TechTime.ViewModels.Api;

namespace TechTime.Controllers.Api
{
    public class JobsController : Controller
    {
        private IRecordRepository _repo;
        private ILogger<HomeController> _logger;
        private IAuthorizationService _authService;
        private UserManager<UserLogin> _userManager;

        public JobsController(IRecordRepository repo, 
            ILogger<HomeController> logger, 
            IAuthorizationService authService,
            UserManager<UserLogin> userManager)
        {
            _repo = repo;
            _logger = logger;
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("api/editdescription")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDescription(XEditableViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var jobEntry = _repo.GetJobEntries().FirstOrDefault(x => x.Id == viewModel.Pk);
                    if (jobEntry == null)
                    {
                        return BadRequest("Could not find job entry");
                    }

                    jobEntry.WorkDescription = viewModel.Value;

                    _repo.UpdateJobEntry(jobEntry);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok();
                    }

                    return BadRequest("Could not save entry to the database");
                }
            }
            catch (Exception)
            {
                
            }

            return BadRequest("Something went wrong...");
        }

        [HttpPost("api/editstatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(XEditableViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var jobEntry = _repo.GetJobEntries().FirstOrDefault(x => x.Id == viewModel.Pk);

                    jobEntry.OwnerId = _userManager.GetUserId(User);

                    var isAuthorized = await _authService.AuthorizeAsync(User, jobEntry, Constants.EditStatus);
                    if (!isAuthorized)
                    {
                        return new ChallengeResult();
                    }

                    _repo.UpdateJobEntry(jobEntry);
                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok();
                    }

                    return BadRequest("Could not save entry to the database");
                }
            }
            catch (Exception)
            {

            }

            return BadRequest("Something went wrong...");
        }

    }
}
