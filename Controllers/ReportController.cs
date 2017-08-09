using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;

namespace TechTime.Controllers
{
    public class ReportController : Controller
    {
        private IRecordRepository _repo;
        private ILogger<ReportController> _logger;

        public ReportController(IRecordRepository repo, ILogger<ReportController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [Authorize]
        public IActionResult JobDetails(int id)
        {
            var model = _repo.GetJobEntries().FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                return View(model);
            }

            _logger.LogError($"Failed to load job entry {id}");
            return BadRequest("Could not find requested job entry");

        }
    }
}
