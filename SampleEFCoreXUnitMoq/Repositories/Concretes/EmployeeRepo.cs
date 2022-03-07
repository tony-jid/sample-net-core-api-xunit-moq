using Microsoft.EntityFrameworkCore;
using SampleEFCoreXUnitMoq.Data.Contexts.Master;
using SampleEFCoreXUnitMoq.Models;
using SampleEFCoreXUnitMoq.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleEFCoreXUnitMoq.Repositories.Concretes
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private DataMasterContext _context;

        public EmployeeRepo(DataMasterContext context)
        {
            _context = context;
        }

        public async Task<Employee> Add(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();

            return newEmployee;
        }

        public async Task Remove(Employee deletingEmployee)
        {
            _context.Employees.Remove(deletingEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> Update(Employee updatingEmployee)
        {
            if (await EmployeeExists(updatingEmployee.Id))
            {
                _context.Entry(updatingEmployee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return updatingEmployee;
            }

            return null;
        }

        public async Task<bool> EmployeeExists(Guid id)
        {
            return await _context.Employees.AnyAsync(_ => _.Id == id);
        }
    }
}
