using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectsTask.Models
{
    public class ProjectRepository
    {
        private ProjectManagmentContext _context;

        public ProjectRepository(ProjectManagmentContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }
        public async Task<Project> GetProject(int id)
        {
            return await _context.Projects.FindAsync(id);
        }
        public async Task CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProject(int id, Project updatedProject)
        {
            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Project>> GetProjectsBySearchRequest(String sortBy, bool? sortAsc, DateTime? startDateFrom, DateTime? startDateTo, DateTime? endDateFrom, DateTime? endDateTo, int? priorityStart, int? priorityEnd)
        {
            var projects = _context.Projects.AsQueryable();

            //sort
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Name":
                        projects = sortAsc != false ? projects.OrderBy(p => p.Name) : projects.OrderByDescending(p => p.Name);
                        break;
                    case "Client":
                        projects = sortAsc != false ? projects.OrderBy(p => p.Client) : projects.OrderByDescending(p => p.Client);
                        break;
                    case "Executor":
                        projects = sortAsc != false ? projects.OrderBy(p => p.Executor) : projects.OrderByDescending(p => p.Executor);
                        break;
                    case "Manager":
                        projects = sortAsc != false ? projects.OrderBy(p => p.ManagerId) : projects.OrderByDescending(p => p.ManagerId);
                        break;
                    case "StartDate":
                        projects = sortAsc != false ? projects.OrderBy(p => p.StartDate) : projects.OrderByDescending(p => p.StartDate);
                        break;
                    case "EndDate":
                        projects = sortAsc != false ? projects.OrderBy(p => p.EndDate) : projects.OrderByDescending(p => p.EndDate);
                        break;
                    case "Priority":
                        projects = sortAsc != false ? projects.OrderBy(p => p.Priority) : projects.OrderByDescending(p => p.Priority);
                        break;
                    default:
                        projects = projects.OrderBy(p => p.Name);
                        break;
                }
            }

            //filter
            if (startDateFrom != null)
            {
                projects = projects.Where(p => p.StartDate >= startDateFrom);
            }
            if (startDateTo != null)
            {
                projects = projects.Where(p => p.StartDate <= startDateTo);
            }

            if (endDateFrom != null)
            {
                projects = projects.Where(p => p.EndDate >= endDateFrom);
            }
            if (endDateTo != null)
            {
                projects = projects.Where(p => p.EndDate <= endDateTo);
            }

            if (priorityStart != null)
            {
                projects = projects.Where(p => p.Priority >= priorityStart);
            }
            if (priorityEnd != null)
            {
                projects = projects.Where(p => p.Priority <= priorityEnd);
            }

            return await projects.ToListAsync();
        }
    }
}
