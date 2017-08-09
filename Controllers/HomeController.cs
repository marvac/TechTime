using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;
using TechTime.ViewModels;

namespace TechTime.Controllers
{
    public class HomeController : Controller
    {
        private IRecordRepository _repo;
        private ILogger<HomeController> _logger;

        public HomeController(IRecordRepository repo, ILogger<HomeController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Auth");
        }

        [Authorize]
        public IActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public IActionResult History()
        {
            var model = new List<HistoryViewModel>();
            foreach (var jobEntry in _repo.GetJobEntries())
            {
                var entry = Mapper.Map<HistoryViewModel>(jobEntry);
                model.Add(entry);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var model = new JobEntryViewModel
            {
                CustomerList = _repo.GetCustomers().ToList(),
                JobTypes = _repo.GetJobTypes().ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(JobEntryViewModel viewModel)
        {
            ViewBag.Error = null;

            var entry = _repo.GetJobEntries().FirstOrDefault(x => 
            x.Customer.CustomerId == viewModel.CustomerId &&
            x.WorkDescription == viewModel.WorkDescription &&
            x.Hours == viewModel.Hours);

            if (entry != null)
            {
                ViewBag.Error = "Duplicate job entry already exists";
            }
            else
            {
                var customer = _repo.GetCustomers().FirstOrDefault(x => x.CustomerId == viewModel.CustomerId);
                if (customer == null)
                {
                    ViewBag.Error = "Customer does not exist...";
                    _logger.LogError($"Tried to create job entry with invalid customer ID: {viewModel.CustomerId}");
                }
                else
                {
                    var jobEntry = Mapper.Map<JobEntry>(viewModel);
                    jobEntry.Tech = User.Identity.Name;
                    jobEntry.Customer = customer;

                    _repo.Add(jobEntry);

                    if (await _repo.SaveChangesAsync())
                    {
                        return RedirectToAction("JobDetails", "Report", new { id = jobEntry.Id });
                    }

                    ViewBag.Error = "Could not add this entry to the database";
                    _logger.LogError($"Error saving to database: {viewModel.CustomerId}");

                }
            }

            return Create();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
