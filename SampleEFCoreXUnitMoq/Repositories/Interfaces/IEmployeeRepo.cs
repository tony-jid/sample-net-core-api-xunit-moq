using SampleEFCoreXUnitMoq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleEFCoreXUnitMoq.Repositories.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(Guid id);
        Task<Employee> Add(Employee newEmployee);
        Task<Employee> Update(Employee updatingEmployee);
        Task Remove(Employee deletingEmployee);
        Task<bool> EmployeeExists(Guid id);
    }
}
