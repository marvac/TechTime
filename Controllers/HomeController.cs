using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TechTime.Models;
using TechTime.ViewModels;
using TechTime.Models.Enum;

namespace TechTime.Controllers
{
    public class HomeController : Controller
    {
        private IRecordRepository _repo;

        public HomeController(IRecordRepository repo)
        {
            _repo = repo;
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
        public IActionResult About()
        {
            return View();
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
                model.Add(new HistoryViewModel
                {
                    ContactName = jobEntry.ContactName,
                    Customer = jobEntry.Customer,
                    Hours = jobEntry.Hours,
                    JobType = jobEntry.JobType,
                    WorkDescription = jobEntry.WorkDescription,
                    Id = jobEntry.Id
                });
            }

            return View(model);
        }

        
        public IActionResult Create()
        {
            var model = new JobEntryViewModel
            {
                CustomerList = _repo.GetCustomers().ToList(),
                JobTypes = Enum.GetValues(typeof(JobType)).Cast<JobType>().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobEntryViewModel viewModel)
        {
            ViewBag.Error = null;
            ViewBag.Success = null;

            var entry = _repo.GetJobEntries().FirstOrDefault(x => 
            x.Customer.CustomerId == viewModel.CustomerId &&
            x.WorkDescription == viewModel.WorkDescription &&
            x.Hours == viewModel.Hours &&
            x.JobType == viewModel.JobType);

            if (entry != null)
            {
                ViewBag.Error = "Duplicate job entry already exists";
            }
            else
            {
                var customer = _repo.GetCustomers().FirstOrDefault(x => x.CustomerId == viewModel.CustomerId);
                if (customer == null)
                {
                    ViewBag.Error = "Customer does not exist";
                }
                else
                {
                    _repo.Add(new JobEntry
                    {
                        ContactName = viewModel.ContactName,
                        Customer = customer,
                        Hours = viewModel.Hours,
                        JobType = viewModel.JobType,
                        WorkDescription = viewModel.WorkDescription
                    });

                    if (await _repo.SaveChangesAsync())
                    {
                        ViewBag.Success = "Job created successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Could not add entry to the database";
                    }
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
