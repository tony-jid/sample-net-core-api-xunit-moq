using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleEFCoreXUnitMoq.Data.Contexts.Master;
using SampleEFCoreXUnitMoq.Models;
using SampleEFCoreXUnitMoq.Repositories.Interfaces;

namespace SampleEFCoreXUnitMoq.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        //DataMasterContext _context;
        //private IEmployeeService _employeeService;
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeRepo.GetAll());
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var employee = await _employeeRepo.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            if (id == Guid.Empty) return BadRequest();
            if (string.IsNullOrWhiteSpace(employee.Name)) return BadRequest();

            employee.Id = id;
            var updatedEmployee = await _employeeRepo.Update(employee);

            if (updatedEmployee == null) return BadRequest();

            return Ok(updatedEmployee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            if (employee == null) return BadRequest();
            if (string.IsNullOrWhiteSpace(employee.Name)) return BadRequest();

            await _employeeRepo.Add(employee);

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var employee = await _employeeRepo.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepo.Remove(employee);

            return Ok(employee);
        }
    }
}
