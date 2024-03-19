using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ProjectsTask.Models;
using System.Diagnostics;
using System.Net;

namespace ProjectsTask.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectRepository _projectRepository;
        private EmployeeRepository _employeeRepository;

        public ProjectsController(ProjectRepository projectRepository, EmployeeRepository employeeRepository)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet(Name = "GetAllProjects")]
        public ActionResult ProjectsView()
        {
            var projects = _projectRepository.GetAllProjects().GetAwaiter().GetResult();
            
            if (projects == null)
                return NotFound();

            return View(projects);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Project>> GetProject(int id)
        //{
        //    var project = await _context.Projects.FindAsync(id);

        //    if (project == null)
        //        return NotFound();

        //    return View(project);
        //}

        [HttpGet]
        public ActionResult CreateProject()
        {
            var employees = _employeeRepository.GetAllEmployees().GetAwaiter().GetResult();

            ViewBag.ManagerId = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.FirstName} {e.MiddleName} {e.LastName}"
            }).ToList();
            ViewBag.EmployeesList = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.FirstName} {e.MiddleName} {e.LastName}"
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProject(Project project)
        {
            if (project != null)
            {
                List<Employee> selectedEmployees = new List<Employee>();

                string[] selectedValue = Request.Form["employeesSelect"];

                if (selectedValue.Length != 0) 
                {
                    foreach (string value in selectedValue)
                    {
                        Employee employee = await _employeeRepository.GetEmployee(Int32.Parse(value));
                        selectedEmployees.Add(employee);
                        await _employeeRepository.AddEmployeeToProject(project, employee);
                    }

                    project.Employees = selectedEmployees;
                }

                await _projectRepository.CreateProject(project);
                return RedirectToAction("ProjectsView");
            }

            var employees = _employeeRepository.GetAllEmployees().GetAwaiter().GetResult();

            ViewBag.ManagerId = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.FirstName} {e.MiddleName} {e.LastName}"
            }).ToList();
            ViewBag.Employees = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.FirstName} {e.MiddleName} {e.LastName}"
            }).ToList();

            return View();
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateProject(int id, Project updatedProject)
        //{
        //    if (id != updatedProject.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(updatedProject).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteProject(int id)
        //{
        //    var project = await _context.Projects.FindAsync(id);

        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Projects.Remove(project);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpGet]
        //public async Task<ActionResult<List<Project>>> GetProjects(String sortBy, bool? sortAsc, DateTime? startDateFrom, DateTime? startDateTo, DateTime? endDateFrom, DateTime? endDateTo, int? priorityStart, int? priorityEnd) {
        //    var projects = _context.Projects.AsQueryable();

        //    //sort
        //    if (!string.IsNullOrEmpty(sortBy))
        //    {
        //        switch (sortBy)
        //        {
        //            case "Name":
        //                projects = sortAsc != false ? projects.OrderBy(p => p.Name) : projects.OrderByDescending(p => p.Name);
        //                break;
        //            case "Client":
        //                projects = sortAsc != false ? projects.OrderBy(p => p.Client) : projects.OrderByDescending(p => p.Client);
        //                break;
        //            case "Executor":
        //                projects = sortAsc != false ? projects.OrderBy(p => p.Executor) : projects.OrderByDescending(p => p.Executor);
        //                break;
        //            case "Manager":
        //                projects = sortAsc != false ? projects.OrderBy(p => p.Manager) : projects.OrderByDescending(p => p.Manager);
        //                break;
        //            case "StartDate":
        //                projects = sortAsc != false ? projects.OrderBy(p => p.StartDate) : projects.OrderByDescending(p => p.StartDate);
        //                break;
        //            case "EndDate":
        //                projects = sortAsc != false ? projects.OrderBy(p => p.EndDate) : projects.OrderByDescending(p => p.EndDate);
        //                break;
        //            case "Priority":
        //                projects = sortAsc != false ? projects.OrderBy(p => p.Priority) : projects.OrderByDescending(p => p.Priority);
        //                break;
        //            default:
        //                projects = projects.OrderBy(p => p.Name);
        //                break;
        //        }
        //    }

        //    //filter
        //    if (startDateFrom != null)
        //    {
        //        projects = projects.Where(p => p.StartDate >= startDateFrom);
        //    }
        //    if (startDateTo != null)
        //    {
        //        projects = projects.Where(p => p.StartDate <= startDateTo);
        //    }

        //    if (endDateFrom != null)
        //    {
        //        projects = projects.Where(p => p.EndDate >= endDateFrom);
        //    }
        //    if (endDateTo != null)
        //    {
        //        projects = projects.Where(p => p.EndDate <= endDateTo);
        //    }

        //    if (priorityStart != null)
        //    {
        //        projects = projects.Where(p => p.Priority >= priorityStart);
        //    }
        //    if (priorityEnd != null)
        //    {
        //        projects = projects.Where(p => p.Priority <= priorityEnd);
        //    }

        //    return await projects.ToListAsync();
        //}
    }
}