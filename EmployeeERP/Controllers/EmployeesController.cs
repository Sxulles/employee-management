using AutoMapper;
using EmployeeERP.Models;
using EmployeeERP.Models.DbEntities;
using EmployeeERP.Models.DTO;
using EmployeeERP.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeERP.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<TimeOffRequest> _timeOffRepository;

        public EmployeesController(IMapper mapper, IGenericRepository<Employee> employeeRepository, IGenericRepository<TimeOffRequest> timeOffRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _timeOffRepository = timeOffRepository;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeDtos = _mapper.Map<ICollection<Employee>, ICollection<EmployeeDto>>(employees);
            

            return View(employeDtos);
        }

        public async Task<IActionResult> Requests(Guid id)
        {
            var employeeRequests = await _timeOffRepository.GetByConditionAsync(request => request.EmployeeId == id, req => req.Employee);
            var employeeDtos = _mapper.Map<ICollection<TimeOffRequest>, ICollection<TimeOffRequestDto>>(employeeRequests); 
            return View(employeeDtos);
        }

        public async Task<IActionResult> UpdateRequest(Guid id)
        {
            var requestDto = _mapper.Map<TimeOffRequest, TimeOffRequestDto>(await _timeOffRepository.GetByIdIncludingAsync(id, req => req.Employee));
            return View(requestDto);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTimeOffRequest(TimeOffRequest timeOffRequest)
        {
            if (ModelState.IsValid)
            {
                var success = await _timeOffRepository.UpdateAsync(timeOffRequest);
                if(success)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Index", timeOffRequest);
        }
    }
}
