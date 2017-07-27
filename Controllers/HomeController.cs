using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechTime.ViewModel;
using Microsoft.AspNetCore.Authorization;
using TechTime.Models;

namespace TechTime.Controllers
{
    public class HomeController : Controller
    {
        private IRecordRepository _repo;

        public HomeController(IRecordRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Home");
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
            return View();
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobEntryViewModel viewModel)
        {
            var entry = _repo.GetJobEntries().FirstOrDefault(x => 
            x.Customer.CustomerId == viewModel.CustomerId &&
            x.WorkDescription == viewModel.WorkDescription &&
            x.Hours == viewModel.Hours &&
            x.JobType == viewModel.JobType);

            if (entry != null)
            {
                ViewBag.Message = "Duplicate job entry already exists";
            }
            else
            {
                var customer = _repo.GetCustomers().FirstOrDefault(x => x.CustomerId == viewModel.CustomerId);
                if (customer == null)
                {
                    ViewBag.Message = "Customer does not exist";
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
                        ViewBag.Message = "Job created successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Could not add entry to the database";
                    }
                }
                
            }
            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
