using System;
using System.Collections.Generic;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DAL;
using Newtonsoft.Json;
public class Board
{
	[JsonProperty("name")]
	private String name;
	[JsonProperty("boardID")]
	private int boardID;
	[JsonProperty("emailOwner")]
	private string emailOwner;
	[JsonProperty("viewers")]
	private BoardDTO boardDTO;
	private TaskController taskController;
	const int columLimitStarter = -1;
	private int taskID = -1;

	public Board(String name, int ID, string emailOwner)
	{
		this.name = name;
		this.boardID = ID;
		this.emailOwner = emailOwner;
		this.boardDTO = new BoardDTO( boardID, name, emailOwner, columLimitStarter, columLimitStarter, columLimitStarter,taskID);
		taskController = new TaskController(ID);
	}


	public Board(BoardDTO boardDTO,List<object> list0, List<object> list1, List<object> list2)
	{
		this.name = boardDTO.GetName();
		this.boardID = boardDTO.GetId();
		this.emailOwner = boardDTO.GetEmailOwner();
		taskController = new TaskController(boardDTO,list0,list1,list2);
	}


	public void SetNewOwner(string email)
	{

		this.emailOwner = email;

	}
	public int GetNewTaskID()
	{
		return SetNewTaskID();
	}
	private int SetNewTaskID()
    {
		taskID++;
		return taskID;
    }
        
	


	public TaskController GetTaskController()
    {
		return this.taskController;
    }


	public string GetName()
    {
		return this.name;
    }


    public string GetBoardOwner()
    {
        return this.emailOwner;
    }


    public int GetID()
    {
		return this.boardID;
    }

	public object GetBoardDTO()
    {
		return this.boardDTO;
    }

}
