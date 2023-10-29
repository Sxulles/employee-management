using AutoMapper;
using EmployeeERP.Models.DbEntities;
using EmployeeERP.Models.DTO;
using EmployeeERP.Models.ViewModel;
using EmployeeERP.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeERP.Controllers
{
    public class RequestController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<TimeOffRequest> _timeOffRepository;


        public RequestController(IMapper mapper, IGenericRepository<Employee> employeeRepository, IGenericRepository<TimeOffRequest> timeOffRepository )
        {
            _employeeRepository = employeeRepository;
            _timeOffRepository = timeOffRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Employees"] = _mapper.Map<ICollection<Employee>, ICollection<EmployeeDto>>(await _employeeRepository.GetAllAsync());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeOffRequest(CreateRequestViewModel timeOffRequest) 
        {

            ViewData["Employees"] = _mapper.Map<ICollection<Employee>, ICollection<EmployeeDto>>(await _employeeRepository.GetAllAsync());
            if (ModelState.IsValid)
            {
                var request = new TimeOffRequest
                {
                    TimeOffStart = timeOffRequest.TimeOffStart,
                    TimeOffEnd = timeOffRequest.TimeOffEnd,
                    Description = timeOffRequest.Description,
                    EmployeeId = timeOffRequest.EmployeeId,
                };
                var success = await _timeOffRepository.AddAsync(request);
                if (!success)
                {
                    ModelState.AddModelError(string.Empty, "Hiba történt a feltöltés során.");
                    return View("Index", timeOffRequest);
                }
                return RedirectToAction("Index");
            }

            // Ha az űrlap adatok nem érvényesek, akkor maradj a jelenlegi nézeten és jelenítsd meg a hibákat.
            return View("Index", timeOffRequest);
        }
    }
}
