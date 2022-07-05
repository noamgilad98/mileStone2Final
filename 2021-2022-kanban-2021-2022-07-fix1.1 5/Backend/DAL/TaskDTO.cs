using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.DAL
{
	public class TaskDTO : DTO
	{
		[JsonProperty("Id")]
		private int Id;
		public int ID { get => Id; set => Id = value; }
		[JsonProperty("CreationTime")]
		private DateTime CreationTime;
		public DateTime CT { get => CreationTime; set => CreationTime = value; }
		[JsonProperty("Title")]
		private string Title;
		public string TitleG { get => Title; set => Title = value; }
		[JsonProperty("Description")]
		private string Description;
		public string Desc { get => Description; set => Description = value; }
		[JsonProperty("DueDate")]
		private DateTime DueDate;
		public DateTime DD { get => DueDate; set => DueDate = value; }
		[JsonProperty("assignee")]
		private string assignee;
		public string Assignee { get => assignee; set => assignee = value; }
		private int columnOrdinal;
        public int ColumnOrdinal { get => columnOrdinal; set => columnOrdinal = value; }
		TaskMapper taskMapper = new TaskMapper("Task");
		private int boardID;
		public int BoardID { get => boardID;}


        public TaskDTO(int ID, DateTime CreationTime, string title, string description, DateTime dueDate, string assignee,int boardID,int columnOrdinal) : base(new TaskMapper("Task"))
		{
			this.Id = ID;
			this.CreationTime = CreationTime;
			this.DueDate = dueDate;
			this.Assignee = assignee;
			this.Description = description;
			this.Title = title;
			this.columnOrdinal = columnOrdinal;
			this.boardID = boardID;
		}
		public void UpdateTaskDueDate(int taskId,int boardID, DateTime dueDate) {
			taskMapper.Update(taskId, boardID, "DueDate", ""+dueDate);
		}

		public void UpdateTaskTitle(int taskId, int boardID, string title) {
            taskMapper.Update(taskId, boardID, "Title", title);
		}

		public void UpdateTaskDescription(int taskId, int boardID, string description) {
            taskMapper.Update(taskId, boardID, "Description", description);
		}
        public void Assign(int taskId, int boardID, string newAsignee)
        {
            taskMapper.Update(taskId, boardID, "Assignee", newAsignee);
        }
		public void ChangeAssignee(int taskId, int boardID, string newAsignee)
		{
            taskMapper.Update(taskId, boardID, "Assignee", newAsignee);

		}
		public void RemoveAssignee(int taskId, int boardID)
        {
            taskMapper.Update(taskId,boardID, "Assignee", "Null");
        }




	}

	
}
