using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _02.MiniORMApp.Data.Entities
{
	public class Project
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public ICollection<EmployeeProject> EmployeeProjects { get; }
	}
}