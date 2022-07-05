using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IntroSE.Kanban.Backend.DAL;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class TaskController
    {
        [JsonProperty("columns")]
        private List<object>[] columns;
        [JsonProperty("limitArr ")]
        private int[] limitArr = new int[3];
		[JsonProperty("boardID")]
		private int boardID;
		[JsonProperty("TaskMapper")]
		private TaskMapper TM;
		[JsonProperty("ColumnMapper")]
		private BoardMapper BM;


		public TaskController(int boardID)
        {
            columns = new List<object>[3];
            columns[0] = new List<object>();
            columns[1] = new List<object>();
            columns[2] = new List<object>();
            limitArr[0] = -1;
            limitArr[1] = -1;
            limitArr[2] = -1;
			this.boardID = boardID;
			TM = new TaskMapper("Task");
			BM = new BoardMapper("Board");

        }
        public TaskController(BoardDTO bDTO,List<object> list0, List<object> list1, List<object> list2)
        {
            columns = new List<object>[3];
			columns[0] = list0;
            columns[1] = list1;
            columns[2] = list2;
            limitArr[0] = bDTO.GetLimit0();
            limitArr[1] = bDTO.GetLimit1();
            limitArr[2] = bDTO.GetLimit2();
            this.boardID = bDTO.GetId();

            TM = new TaskMapper("Task");
            BM = new BoardMapper("Board");
        }

        public void CreateTask(string title, string description, DateTime dueDate, int taskID)
		{
			if (columns[0].Count < limitArr[0] || limitArr[0] == -1)
			{
				if (!string.IsNullOrEmpty(title))
				{
					for (int i = 0; i < columns.Length; i++) // check if title already exist
					{
						foreach (Task t in columns[i])
						{
							if (title.Equals(t.GetTaskTitle()))
							{
								throw new Exception("there is allready task with the same name in this board");
							}
						}
					}
					if (dueDate.CompareTo(DateTime.Now) > 0)
					{
						if (description != null)
						{
							Task new_task = new Task(dueDate, title, description, taskID,boardID);
							this.columns[0].Add(new_task);
							try { TM.CreateTask(title, description, dueDate, taskID, this.boardID, new_task.GetCreationTime(), new_task.GetAssignee(), 0); }//DAL//TODO (!!!!!!!!column 0)
							catch (Exception ex) { throw new Exception(ex.Message); }
							try { BM.Update(this.boardID, "lastTaskID", taskID); }
							catch (Exception ex) { throw new Exception(ex.Message); }
                        }
                        else
                        {
							throw new Exception("desc can't be null");
                        }
					}
                    else
                    {
						throw new Exception("duedate must be later than today");
                    }
				}
                else
                {
					throw new Exception("title can't be empty");
                }
			}
			else
				throw new Exception("cant add more tasks");
		}

		public void AdvanceTask(string mail, int columnOrdinal, int taskID)
		{
			if (columnOrdinal == 0 || columnOrdinal == 1)
			{
				if (limitArr[columnOrdinal + 1] == -1 || columns[columnOrdinal + 1].Count < limitArr[columnOrdinal + 1])
				{
					bool isIdFound = false;
					foreach (Task t in this.columns[columnOrdinal])
					{
						if (t.GetTaskID() == taskID)
						{
							if (t.GetAssignee() != null && t.GetAssignee().Equals(mail))
							{
								this.columns[columnOrdinal + 1].Add(t);
								this.columns[columnOrdinal].Remove(t);
								isIdFound = true;
								TM.AdvanceTask(taskID, columnOrdinal +1, boardID);
								t.advanceColumnOrdinal();
							}
                            else
                            {
								throw new Exception("only the assignee can change the task");
                            }
                            break;
                        }
					}
					if (!isIdFound)
					{
						throw new Exception("no such id in this board");
					}

				}
				else
				{
					throw new Exception("column is full");
				}
			}
			else
			{
				throw new Exception("column ordinal must be 0 or 1");
			}
		}

		public void LimitColumn(int boardID ,int columnOrdinal, int limit)//
		{
			if (this.columns[columnOrdinal].Count <= limit && 0 <= limit)
			{
				this.limitArr[columnOrdinal] = limit;
				BM.LimitColumn(boardID,columnOrdinal, limit);
			}
			else
				throw new Exception("the limit must be positive number");
		}

		public int GetColumnLimit(int columnOrdinal)//
		{
			return this.limitArr[columnOrdinal];
		}


		public String GetColumnName(int columnOrdinal)//
		{
			if (columnOrdinal == 0)
				return ("backlog");
			else if (columnOrdinal == 1)
				return ("in progress");
			else if (columnOrdinal == 2)
				return ("done");
			else throw new Exception("the column ordinal must be 0,1,2 only!");
		}

		public List<object> GetColumn(int columnOrdinal)//
		{
			return this.columns[columnOrdinal];
		}


		public List<object> InProgressTasks()
		{
			return columns[1];
		}

		public Task GetTask(int taskID, int columnOrdinal)
		{
			List<object> curr = columns[columnOrdinal];
			for (int j = 0; j < curr.Count; j++)
			{

				if (((Task)curr[j]).GetTaskID() == taskID)
				{
					return (Task)curr[j];
				}
			}
			throw new Exception("no such Task");
		}

		public Task GetTask(string taskName, int columnOrdinal)
		{
			List<object> curr = columns[columnOrdinal];
			for (int j = 0; j < curr.Count; j++)
			{

				if (((Task)curr[j]).GetTaskTitle() == taskName)
				{
					return (Task)curr[j];
				}
			}
			return null;
		}
      
    }
}
