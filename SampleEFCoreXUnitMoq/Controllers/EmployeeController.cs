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
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _employeeRepo.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();

            var updatedEmployee = await _employeeRepo.Update(employee);

            if (updatedEmployee == null) return BadRequest();

            return NoContent();
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await _employeeRepo.Add(employee);

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(Guid id)
        {
            var employee = await _employeeRepo.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepo.Remove(employee);

            return employee;
        }
    }
}
