using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.BackendTests
{
     class Like_Grading
    {
        FactoryService init;

        public Like_Grading()
        {
            init = new FactoryService();
        }
        public void RunTests()
        {

            init.userService.LoadData();
            



            //Console.WriteLine(Login("123@gmail.com", "123123Aaa"));
            //Console.WriteLine(AddBoard("123@gmail.com", "TODO1"));
            //Console.WriteLine(RemoveBoard("01@gmail.com", "TODO"));

            //Console.WriteLine(Register("noam1W@gmail.com", "123123Aaa"));
            //Console.WriteLine(Register("123@gmail.com", "123123Aaa"));
            //Console.WriteLine(Register("is6raelW@gmail.com", "123123Aaa"));
            //Console.WriteLine(Logout("noam1W@gmail.com"));

            //Console.WriteLine(Login("is6raelW@gmail.com", "123123Aaa"));

            //Console.WriteLine("limit");
            ////Console.WriteLine(LimitColumn("is6raelW@gmail.com", "TODO",1,10));
            ////Console.WriteLine(GetColumnName("is6raelW@gmail.com", "TODO", 0));
            ////Console.WriteLine(GetColumnLimit("is6raelW@gmail.com", "TODO", 1));
            //DateTime dt = new DateTime(2023 / 11 / 12);
            //Console.WriteLine(AddTask("is6raelW@gmail.com", "TODO","noam1","reut",dt));
            //Console.WriteLine(AddTask("is6raelW@gmail.com", "TODO", "noam2", "reut2", dt));
            //Console.WriteLine(AddTask("is6raelW@gmail.com", "TODO", "noam3", "reut2", dt));
            //Console.WriteLine(AdvanceTask("is6raelW@gmail.com", "TODO",0,1 ));

            //Console.WriteLine(UpdateTaskDescription("is6raelW@gmail.com", "TODO",0,3,"dddddd"));
            //Console.WriteLine(GetTask("is6raelW@gmail.com", "TODO", 1, 1));


            ////Console.WriteLine(RemoveBoard("is6raelW@gmail.com", "TODO"));
            ///


        }



        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Register(string email, string password)
            {
                try
                {
                    return init.userService.AddNewUser(email, password);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            ///  This method logs in an existing user.
            /// </summary>
            /// <param name="email">The email address of the user to login</param>
            /// <param name="password">The password of the user to login</param>
            /// <returns>A response with the user's email, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string Login(string email, string password)
            {
                try
                {
                    return init.userService.Login(email, password);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method logs out a logged in user. 
            /// </summary>
            /// <param name="email">The email of the user to log out</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string Logout(string email)
            {
                try
                {
                    return init.userService.Logout(email);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }

            /// <summary>
            /// This method limits the number of tasks in a specific column.
            /// </summary>
            /// <param name="email">The email address of the user, must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
            /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
            {
                try
                {
                    return init.taskService.LimitColumn(email, boardName, columnOrdinal, limit);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }

            /// <summary>
            /// This method gets the limit of a specific column.
            /// </summary>
            /// <param name="email">The email address of the user, must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
            /// <returns>A response with the column's limit, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string GetColumnLimit(string email, string boardName, int columnOrdinal)
            {
                try
                {
                    return init.taskService.GetColumnLimit(email, boardName, columnOrdinal);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method gets the name of a specific column
            /// </summary>
            /// <param name="email">The email address of the user, must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
            /// <returns>A response with the column's name, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string GetColumnName(string email, string boardName, int columnOrdinal)
            {
                try
                {
                    return init.taskService.GetColumnName(email, boardName, columnOrdinal);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method adds a new task.
            /// </summary>
            /// <param name="email">Email of the user. The user must be logged in.</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="title">Title of the new task</param>
            /// <param name="description">Description of the new task</param>
            /// <param name="dueDate">The due date if the new task</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
            {
                // Board board = new Board(boardName);

                try
                {
                    return init.taskService.AddTask(email, boardName, title, description, dueDate);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method updates the due date of a task
            /// </summary>
            /// <param name="email">Email of the user. Must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
            /// <param name="taskId">The task to be updated identified task ID</param>
            /// <param name="dueDate">The new due date of the column</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
            {
                //Board board = new Board(boardName);

                try
                {
                    return init.taskService.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method updates task title.
            /// </summary>
            /// <param name="email">Email of user. Must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
            /// <param name="taskId">The task to be updated identified task ID</param>
            /// <param name="title">New title for the task</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
            {
                // Board board = new Board(boardName);
                try
                {
                    return init.taskService.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method updates the description of a task.
            /// </summary>
            /// <param name="email">Email of user. Must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
            /// <param name="taskId">The task to be updated identified task ID</param>
            /// <param name="description">New description for the task</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
            {

                // Board board = new Board(boardName);
                try
                {
                    return init.taskService.UpdateTaskDescription(email, boardName, columnOrdinal, taskId, description);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method advances a task to the next column
            /// </summary>
            /// <param name="email">Email of user. Must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
            /// <param name="taskId">The task to be updated identified task ID</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
            {
                try
                {
                    return init.taskService.AdvanceTask(email, boardName, columnOrdinal, taskId);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method returns a column given it's name
            /// </summary>
            /// <param name="email">Email of the user, must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
            /// <returns>A response with a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string GetColumn(string email, string boardName, int columnOrdinal)
            {
                try
                {
                    return init.taskService.GetColumn(email, boardName, columnOrdinal);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method adds a board to the specific user.
            /// </summary>
            /// <param name="email">Email of the user, must be logged in</param>
            /// <param name="name">The name of the new board</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string AddBoard(string email, string name)
            {
                try
                {
                    return init.boardService.CreateBoard(email, name);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method deletes a board.
            /// </summary>
            /// <param name="email">Email of the user, must be logged in and an owner of the board.</param>
            /// <param name="name">The name of the board</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string RemoveBoard(string email, string name)
            {
                try
                {
                    return init.boardService.DeleteBoard(email, name);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }


            /// <summary>
            /// This method returns all in-progress tasks of a user.
            /// </summary>
            /// <param name="email">Email of the user. Must be logged in</param>
            /// <returns>A response with a list of the in-progress tasks of the user, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string InProgressTasks(string email)
            {
                try
                {
                    return init.userService.InProgresTasks(email);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }

            /// <summary>
            /// This method returns a list of IDs of all user's boards.
            /// </summary>
            /// <param name="email"></param>
            /// <returns>A response with a list of IDs of all user's boards, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string GetUserBoards(string email)
            {
                try
                {
                    return init.userService.GetAllBoards(email);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }

            /// <summary>
            /// This method returns a board's name
            /// </summary>
            /// <param name="boardId">The board's ID</param>
            /// <returns>A response with the board's name, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string GetBoardName(int boardId)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// This method adds a user as member to an existing board.
            /// </summary>
            /// <param name="email">The email of the user that joins the board. Must be logged in</param>
            /// <param name="boardID">The board's ID</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string JoinBoard(string email, int boardID)
            {
                try
                {
                    return init.userService.JoinBoard(email, boardID);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }

            /// <summary>
            /// This method removes a user from the members list of a board.
            /// </summary>
            /// <param name="email">The email of the user. Must be logged in</param>
            /// <param name="boardID">The board's ID</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string LeaveBoard(string email, int boardID)
            {
                try
                {
                    return init.userService.LeaveBoard(email, boardID);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }

            /// <summary>
            /// This method assigns a task to a user
            /// </summary>
            /// <param name="email">Email of the user. Must be logged in</param>
            /// <param name="boardName">The name of the board</param>
            /// <param name="columnOrdinal">The column number. The first column is 0, the number increases by 1 for each column</param>
            /// <param name="taskID">The task to be updated identified a task ID</param>        
            /// <param name="emailAssignee">Email of the asignee user</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
            {
                try
                {
                    return init.taskService.ChangeAssignee(email, emailAssignee, boardName, taskID, columnOrdinal);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }

            ///<summary>This method loads all persisted data.
            ///<para>
            ///<b>IMPORTANT:</b> When starting the system via the GradingService - do not load the data automatically, only through this method. 
            ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
            ///</para>
            /// </summary>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string LoadData()
            {
                throw new NotImplementedException();
            }

            ///<summary>This method deletes all persisted data.
            ///<para>
            ///<b>IMPORTANT:</b> 
            ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
            ///</para>
            /// </summary>
            ///<returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string DeleteData()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// This method transfers a board ownership.
            /// </summary>
            /// <param name="currentOwnerEmail">Email of the current owner. Must be logged in</param>
            /// <param name="newOwnerEmail">Email of the new owner</param>
            /// <param name="boardName">The name of the board</param>
            /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
            public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
            {
                try
                {
                    return init.userService.TransferOwner(currentOwnerEmail, newOwnerEmail, boardName);
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, null);
                    return JsonConvert.SerializeObject(response);
                }
            }















        

        //public string Register(string email, string password)
        //{
        //    try
        //    {
                
        //        return init.userService.AddNewUser(email, password);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string Login(string email, string password)
        //{
        //    try
        //    {
        //        return init.userService.Login(email, password);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string Logout(string email)
        //{
        //    try
        //    {
        //        return init.userService.Logout(email);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string AddBoard(string email, string name)
        //{
        //    try
        //    {
        //        return init.boardService.CreateBoard(email, name);//100
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string RemoveBoard(string email, string name)
        //{
        //    try
        //    {
        //        return init.boardService.DeleteBoard(email, name);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        //{
        //    try
        //    {
        //        return init.taskService.LimitColumn(email, boardName, columnOrdinal, limit);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        //{
        //    try
        //    {
        //        return init.taskService.GetColumnLimit(email, boardName, columnOrdinal);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string GetColumnName(string email, string boardName, int columnOrdinal)
        //{
        //    try
        //    {
        //        return init.taskService.GetColumnName(email, boardName, columnOrdinal);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        //{

        //    try
        //    {
        //        return init.taskService.AddTask(email, boardName, title, description, dueDate);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string GetColumn(string email, string boardName, int columnOrdinal)
        //{
        //    try
        //    {
        //        return init.taskService.GetColumn(email, boardName, columnOrdinal);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string GetTask(string email, string boardName,int taskid, int columnOrdinal)
        //{
        //    try
        //    {
        //        return init.taskService.GetTask(email, boardName,taskid, columnOrdinal);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        //{

        //    try
        //    {
        //        return init.taskService.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        //{

            
        //    try
        //    {
        //        return init.taskService.UpdateTaskDescription(email, boardName, columnOrdinal, taskId, description);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}
        //public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        //{
           

        //    try
        //    {
        //        return init.taskService.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}

        //public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        //{
        //    try
        //    {
        //        return init.taskService.AdvanceTask(email, boardName, columnOrdinal, taskId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response response = new Response(ex.Message, null);
        //        return JsonConvert.SerializeObject(response);
        //    }
        //}

    }
}
