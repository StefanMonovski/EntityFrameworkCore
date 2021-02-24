using _01.MiniORM;
using _02.MiniORMApp.Data.Entities;

namespace _02.MiniORMApp.Data
{
	public class SoftUniDbContext : DbContext
	{
		public SoftUniDbContext(string connectionString)
			: base(connectionString)
		{
		}

		public DbSet<Employee> Employees { get; }

		public DbSet<Department> Departments { get; }

		public DbSet<Project> Projects { get; }

		public DbSet<EmployeeProject> EmployeesProjects { get; }
	}
}
