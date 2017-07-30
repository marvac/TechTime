using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;
using TechTime.ViewModels.Api;

namespace TechTime.Controllers.Api
{
    public class JobsController : Controller
    {
        private IRecordRepository _repo;

        public JobsController(IRecordRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("api/editdescription")]
        public async Task<IActionResult> Post(XEditableViewModel viewModel)
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
    }
}
