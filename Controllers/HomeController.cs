using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private IAuthorizationService _authService;
        private UserManager<UserLogin> _userManager;

        public HomeController(IRecordRepository repo, 
            ILogger<HomeController> logger, 
            IAuthorizationService authService, 
            UserManager<UserLogin> userManager)
        {
            _repo = repo;
            _logger = logger;
            _authService = authService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var startDate = DateTime.Now.AddMonths(-6);
                var data = _repo.GetJobEntries().Where(x => x.DateCreated >= startDate);

                DashboardViewModel viewModel = new DashboardViewModel();

                viewModel.BarChart.Labels = Enumerable.Range(0, 6)
                    .Select(x => DateTime.Now.AddMonths(x - 5))
                    .Select(date => date.ToString("MMM")).ToArray();


                foreach (var jobType in data.GroupBy(x => x.Type))
                {
                    /* I promise I will fix this garbage after I get some sleep */
                    double hours1 = data.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-5).Month && x.Type == jobType.Key).Select(x => x.Hours).Sum();
                    double hours2 = data.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-4).Month && x.Type == jobType.Key).Select(x => x.Hours).Sum();
                    double hours3 = data.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-3).Month && x.Type == jobType.Key).Select(x => x.Hours).Sum();
                    double hours4 = data.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-2).Month && x.Type == jobType.Key).Select(x => x.Hours).Sum();
                    double hours5 = data.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-1).Month && x.Type == jobType.Key).Select(x => x.Hours).Sum();
                    double hours6 = data.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-0).Month && x.Type == jobType.Key).Select(x => x.Hours).Sum();

                    viewModel.BarChart.DataSets.Add(new BarChartViewModel.Bar
                    {
                        Label = jobType.Key,
                        BackgroundColor = $"rgb({_repo.GetJobByDesc(jobType.Key).ColorCode})",
                        Data = new double[] { hours1, hours2, hours3, hours4, hours5, hours6 }
                    });
                }

                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public IActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> History()
        {
            var model = new List<HistoryViewModel>();

            foreach (var entry in _repo.GetJobEntries())
            {
                var authResult = await _authService.AuthorizeAsync(User, entry, Constants.View);
                if (authResult)
                {
                    model.Add(Mapper.Map<HistoryViewModel>(entry));
                }
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

            if (ModelState.IsValid)
            {
                var entry = _repo.GetJobEntries().FirstOrDefault(x =>
                    x.Customer.CustomerId == viewModel.CustomerId &&
                    x.Description == viewModel.Description &&
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
                        try
                        {
                            var jobEntry = Mapper.Map<JobEntry>(viewModel);
                            //jobEntry.Type = _repo.GetJobTypes().FirstOrDefault(x => x.Description == viewModel.Description);
                            jobEntry.OwnerId = _userManager.GetUserId(User);
                            jobEntry.Customer = customer;

                            _repo.Add(jobEntry);

                            if (await _repo.SaveChangesAsync())
                            {
                                return RedirectToAction("JobDetails", "Report", new { id = jobEntry.Id });
                            }

                            ViewBag.Error = "Could not add this entry to the database";
                            _logger.LogError($"Error saving to database: {viewModel.CustomerId}");
                        }
                        catch(Exception ex)
                        {
                            _logger.LogError(ex.Message);
                        }

                    }
                }
            }

            return Create();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
