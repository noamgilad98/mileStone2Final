using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DAL;
public class Task
{
	[JsonProperty("Id")]//
	private int Id;
	[JsonProperty("CreationTime")]
	private DateTime CreationTime;
	[JsonProperty("Title")]
	private string Title;
	[JsonProperty("Description")]
	private string Description;
	[JsonProperty("DueDate")]
	private DateTime DueDate;
    [JsonProperty("assignee")]
    private string assignee;
	[JsonProperty("TaskDTO")]
	private TaskDTO taskDTO;
    [JsonProperty("columnOrdinal")]
    private int columnOrdinal;
	const int columnOrdinalStarter = 0;
	private int boardID;


    public Task(DateTime DueDate, string title, string description, int Id,int boardID)
	{
		this.Id = Id;
		CreationTime = DateTime.Now;
		Title = title;
	    Description = description;
		this.DueDate = DueDate;
		this.assignee = "Null";
		this.columnOrdinal = columnOrdinalStarter;
		this.boardID = boardID;
		taskDTO = new TaskDTO(Id,this.CreationTime,title,description,DueDate,assignee,boardID,columnOrdinal);
		
	}

	public Task(TaskDTO task_dto)
    {
		this.Id = task_dto.ID;
		CreationTime = task_dto.CT;
		Title = task_dto.TitleG;
		Description = task_dto.Desc;
		this.DueDate = task_dto.DD;
		this.assignee = task_dto.Assignee;
		this.columnOrdinal = task_dto.ColumnOrdinal;
		taskDTO = task_dto;
		this.boardID = task_dto.BoardID;
	}
	public int GetTaskID()
	{
		return Id;
	}

	public DateTime GetDueDate()
    {
		return DueDate;
    }

	public string GetAssignee()
	{
		return this.assignee;
	}
	public DTO GetTaskDTO()
    {
		return this.taskDTO;
    }


	public void UpdateTaskDueDate(DateTime dueDate, int columnOrdinal)
	{
		if (IsAssigned())
		{
			if (columnOrdinal == 0 || columnOrdinal == 1)
			{
				if (dueDate > CreationTime || dueDate > DateTime.Now)
				{
					DueDate = dueDate;
					taskDTO.UpdateTaskDueDate(this.Id, this.boardID, dueDate);
				}
				else
				{
					throw new Exception("invalid date");
				}
			}
			else
			{
				throw new Exception("can't change a task that is done");
			}
		}
        else
        {
			throw new Exception("only the assignee can change the task. there's no such one.");
		}
	}
	public void UpdateTaskTitle(string title, int columnOrdinal)
	{
		if (IsAssigned())
		{
			if (columnOrdinal == 0 || columnOrdinal == 1)
			{
				if (title.Length <= 50 && title.Length > 0)
				{
					Title = title;
					taskDTO.UpdateTaskTitle(this.Id, this.boardID, title);
				}
				else
				{
					throw new Exception("title can contain up to 50 chars");
				}
			}
			else
			{
				throw new Exception("can't change a task that is done");
			}
		}
        else
        {
			throw new Exception("only the assignee can change the task. there's no such one.");
		}
	}
	
	public void UpdateTaskDescription(string description, int columnOrdinal)
	{
		if (IsAssigned())
		{
			if (columnOrdinal == 0 || columnOrdinal == 1)
			{
				if (description.Length <= 300)
				{
					Description = description;
					taskDTO.UpdateTaskDescription(this.Id,this.boardID,description);
				}
				else
				{
					throw new Exception("description can contain up to 300 chars");
				}
			}
			else
			{
				throw new Exception("can't change a task that is done");
			}
		}
        else
        {
			throw new Exception("only the assignee can change the task. there's no such one.");
		}
	}
	public bool IsAssigned() {
		return !(assignee.Equals("Null"));
	}
   // public void Assign(string email)
   // {
   //     if (!IsAssigned())
   //     {
			//this.assignee = email;
   //         try { taskDTO.Assign(this.Id, this.boardID, email); }//DAL
   //         catch (Exception ex) { throw new Exception(ex.Message); }
   //     }
   //     else
   //     {
   //         throw new Exception("task assignee taken, please use ''ChangeAssignee'' function");
   //     }

   // }
    public void ChangeAssignee(string email, string newAsignee)
	{ 
		if (IsAssigned())
        {
            if (email.Equals(this.assignee))
            {
				this.assignee = newAsignee;
				taskDTO.ChangeAssignee(this.Id,this.boardID, newAsignee);
            }
            else
            {
				throw new Exception("only the current assignee can assign the task");
            }
        }
        else
        {
			this.assignee = newAsignee;
			taskDTO.ChangeAssignee(this.Id,this.boardID, newAsignee);
		}
	}
	public void RemoveAssignee(string email)
	{
		if (IsAssigned())
		{
			if (email.Equals(this.assignee))
			{
				this.assignee = null;
				taskDTO.RemoveAssignee(this.Id,this.boardID);
			}
			else
			{
				throw new Exception("only the current assignee can unassign the task");
			}
		}
		else
		{
			throw new Exception("Task is already unassigned");
		}
	}
	public string GetTaskTitle()
    {
		return this.Title;
    }
	public int GetBoardID()
    {
		return this.boardID;
    }
		
	public DateTime GetCreationTime()
    {
		return CreationTime;
    }
	public void advanceColumnOrdinal()
    {
		if(this.columnOrdinal==0 || this.columnOrdinal == 1)
        {
			this.columnOrdinal++;
		}
	
    }
    
}
