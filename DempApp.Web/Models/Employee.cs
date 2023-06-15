using System;
using System.ComponentModel.DataAnnotations;

namespace DempApp.Web.Models
{
	public class Employee
	{
		public Employee()
		{
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[Display(Name ="Employee Name")]
		public string Name { get; set; }

		public string Designation { get; set; }

		[DataType(DataType.MultilineText)]
		public string Address { get; set; }

		public DateTime? recordCreatedOn { get; set; }
	}
}

