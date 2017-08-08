using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ReportController(IRecordRepository repo)
        {
            _repo = repo;
        }

        [Authorize]
        public IActionResult JobDetails(int id)
        {
            var model = _repo.GetJobEntries().FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                return View(model);
            }

            return BadRequest("Could not find requested job entry");
        }
    }
}
