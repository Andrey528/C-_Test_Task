using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsTask.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
        public string Executor { get; set; }
        [NotMapped]
        public List<Employee> Employees { get; set; }
        public int ManagerId { get; set; }
        public Employee Manager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
    }
}
