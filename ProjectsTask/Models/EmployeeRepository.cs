using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ProjectsTask.Models
{
    public class EmployeeRepository
    {
        private ProjectManagmentContext _context;

        public EmployeeRepository(ProjectManagmentContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.FindAsync(id); ;
        }
        public async Task CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEmployee(int id, Project updatedEmployee)
        {
            _context.Entry(updatedEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        public async Task AddEmployeeToProject(Project project, Employee employee)
        {
            project.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveEmployeeFromProject(Project project, Employee employee)
        {
            project.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
