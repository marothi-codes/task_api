using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models
{
	public class TaskModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter the task's description text.")]
		public string Text { get; set; }

		[Required(ErrorMessage = "Please specify the task's due date.")]
		public string Date { get; set; }

		public bool Reminder { get; set; }
	}
}
