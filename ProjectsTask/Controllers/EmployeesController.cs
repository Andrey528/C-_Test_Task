using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsTask.Models;

namespace ProjectsTask.Controllers
{
    public class EmployeesController : Controller
    {
        private ProjectRepository _projectRepository;
        private EmployeeRepository _employeeRepository;

        public EmployeesController(ProjectRepository projectRepository, EmployeeRepository employeeRepository)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult EmployeesView()
        {
            var employees = _employeeRepository.GetAllEmployees().GetAwaiter().GetResult();
            return View(employees);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);

        //    if (employee == null)
        //        return NotFound();

        //    return employee;
        //}

        [HttpGet]
        public ActionResult AddEmployee()
        {
            var projects = _projectRepository.GetAllProjects().GetAwaiter().GetResult();
            ViewData["Projects"] = new MultiSelectList(projects, "Id", "Name");
            return View("AddEmployee");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee([Bind("FirstName, MiddleName, LastName, Email, Projects")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.CreateEmployee(employee);
                return RedirectToAction("EmployeesView");
            }

            var projects = _projectRepository.GetAllProjects().GetAwaiter().GetResult();

            ViewData["Projects"] = new MultiSelectList(projects, "Id", "Name");

            return View();
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateEmployee(int id, Project updatedEmployee)
        //{
        //    if (id != updatedEmployee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(updatedEmployee).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteEmployee(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpPost ("{projectId}/addEmployee/{employeeId}")]
        //public async Task<ActionResult> AddEmployeeToProject(int projectId, int employeeId)
        //{
        //    var project = await _context.Projects.FindAsync(projectId);
        //    var employee = await _context.Employees.FindAsync(employeeId); 

        //    if (employee == null || project == null) 
        //    { 
        //        return NotFound();
        //    }

        //    project.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}

        //[HttpPost("{projectId}/removeEmployee/{employeeId}")]
        //public async Task<ActionResult> RemoveEmployeeToProject(int projectId, int employeeId)
        //{
        //    var project = await _context.Projects.FindAsync(projectId);
        //    var employee = await _context.Employees.FindAsync(employeeId);

        //    if (employee == null || project == null)
        //    {
        //        return NotFound();
        //    }

        //    project.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
    }
}
