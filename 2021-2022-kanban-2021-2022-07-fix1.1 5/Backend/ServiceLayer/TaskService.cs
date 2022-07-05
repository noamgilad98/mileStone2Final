using System;
using System.Collections.Generic;
using System.Reflection;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using log4net;
using Newtonsoft.Json;

public class TaskService
{
    private UserController uc;
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    public TaskService(UserController uc)
    {
        this.uc = uc;
    }

    public TaskService()
    {
    }

    /// <summary>
    /// This method updates the due date of a task.
    /// </summary>
    /// <param name="email">The user email address, used as the username for logging the system.</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
    /// <param name="taskId">The task id number, for identication and future changes.</param>
    /// <param name="dueDate">the task due date.</param>
    /// <returns> a string of the updated date, unless an exception if an error occurs </returns>
    public String UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    TC.GetTask(taskId, columnOrdinal).UpdateTaskDueDate(dueDate, columnOrdinal);
                    log.Debug("Task due date has changed");
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                    log.Info("can't change the tasks due date while the user is not logged in");
                }
            }
            else
            {
                res = new Response("user does not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
            log.Error(ex.Message);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res,Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            return ex.Message;
            log.Error(ex.Message);
        }
    }
    /// <summary>
    /// This method updates the title of a task.
    /// </summary>
    /// <param name="email">The user email address, used as the username for logging the system.</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
    /// <param name="taskId">The task id number, for identication and future changes.</param>
    /// <param name="title">the task title.</param>
    /// <returns> a string of the updated title, unless an exception if an error occurs </returns>
    public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    try
                    {
                        TC.GetTask(taskId, columnOrdinal).UpdateTaskTitle(title, columnOrdinal);
                        log.Debug("Task title has changed successfully");
                    }
                    catch (Exception ex)
                    {
                        res = new Response(ex.Message, null);

                    }
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user does not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return ex.Message;
        }
    }

    /// <summary>
    /// This method updates the description of a task.
    /// </summary>
    /// <param name="email">The user email address, used as the username for logging the system.</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
    /// <param name="taskId">The task id number, for identication and future changes.</param>
    /// <param name="description">the task description.</param>
    /// <returns> a string of the updated description, unless an exception if an error occurs </returns>
    /// 
    public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    try
                    {
                        TC.GetTask(taskId, columnOrdinal).UpdateTaskDescription(description, columnOrdinal);
                        log.Debug("Task description has changed");
                    }catch (Exception ex)
                    {
                        res = new Response(ex.Message,null);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
            log.Error(ex.Message);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            log.Debug("json created");
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return ex.Message;
        }
    }

    /// <summary>
    /// This method adds a task to the board.
    /// </summary>
    /// <param name="email">The user email address, used as the username for logging the system.</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="description">the task description.</param>
    /// <param name="title">the task title.</param>
    /// <param name="dueDate">the task due date.</param>
    /// <returns> a string with user-email, unless an error occurs </returns>
    public String AddTask(String email, String boardName, String title, String description, DateTime dueDate)
    {//
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    TC.CreateTask(title, description, dueDate, board.GetNewTaskID());
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user does not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return ex.Message;
        }
    }

    public string GetTask(string email,string boardName, int taskID, int columnOrdinal)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    Task task = TC.GetTask(taskID, columnOrdinal);
                    res = new Response(null,task);
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user does not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return ex.Message;
        }
    }

    //public String Assign(string email, string boardName, int taskId, int columnOrdinal)
    //{
    //    Response res = new Response();
    //    try
    //    {
    //        User user = uc.GetUser(email);
    //        if (uc.IsUserExists(email))
    //        {
    //            if (user.isLogin)
    //            {
    //                Board board = user.GetBoard(boardName);
    //                TaskController TC = board.GetTaskController();
    //                Task task = TC.GetTask(taskId, columnOrdinal);
    //                task.Assign(email);
    //                log.Debug("task assignee changed successfully");
    //            }
    //            else
    //            {
    //                res = new Response("user is not logged in ", null);
    //            }
    //        }
    //        else
    //        {
    //            res = new Response("user does not exist in the system ", null);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(ex.Message);
    //        res = new Response(ex.Message, null);
    //    }
    //    try
    //    {
    //        String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
    //        return jsonTOSend;
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(ex.Message);
    //        return ex.Message;
    //    }

    //}

    //public String ChangeAssignee(string email, string newAssignee, string boardName, int taskId, int columnOrdinal)
    //{
    //    Response res = new Response();
    //    try
    //    {
    //        User user = uc.GetUser(email);
    //        if (uc.IsUserExists(email))
    //        {
    //            if (user.isLogin)
    //            {
    //                Board board = user.GetBoard(boardName);
    //                TaskController TC = board.GetTaskController();
    //                Task task = TC.GetTask(taskId, columnOrdinal);
    //                task.ChangeAssignee(email, newAssignee);
    //                log.Debug("task assignee changed successfully");
    //            }
    //            else
    //            {
    //                res = new Response("user is not logged in ", null);
    //            }
    //        }
    //        else
    //        {
    //            res = new Response("user does not exist in the system ", null);
    //        }
    //    }
    //    catch(Exception ex)
    //    {
    //        log.Error(ex.Message);
    //        res = new Response(ex.Message, null);
    //    }
    //    try
    //    {
    //        String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
    //        return jsonTOSend;
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(ex.Message);
    //        return ex.Message;
    //    }

    //}
    public String ChangeAssignee(string email, string newAssignee, string boardName, int taskId, int columnOrdinal)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            User user2 = uc.GetUser(newAssignee);
            if (uc.IsUserExists(email) && uc.IsUserExists(newAssignee))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    try { Board board2 = user2.GetBoard(boardName); }
                    catch { throw new Exception("the board not exsist for the new assignee"); }
                    
                    TaskController TC = board.GetTaskController();
                    Task task = TC.GetTask(taskId, columnOrdinal);
                    task.ChangeAssignee(email, newAssignee);
                    log.Debug("task assignee changed successfully");
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user does not exist in the system ", null);
            }
        }
        catch(Exception ex)
        {
            log.Error(ex.Message);
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return ex.Message;
        }

    }
    public String RemoveAssignee(string email, string boardName, int taskId, int columnOrdinal)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    Task task = TC.GetTask(taskId, columnOrdinal);
                    task.RemoveAssignee(email);
                    log.Debug("task assignee removed successfully");
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user does not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            return ex.Message;
        }
    }

    public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    TC.AdvanceTask(email,columnOrdinal,taskId);
                    log.Debug("task is succsesfully advanced");
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user is not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res,Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// This method limits the number of tasks in a specific column.
    /// </summary>
    /// <param name="email">The email address of the user, must be logged in</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
    /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
    /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
    public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    TC.LimitColumn(board.GetID(),columnOrdinal, limit);
                    log.Debug("column is succsesfully limited");
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user is not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// This method gets the name of a specific column
    /// </summary>
    /// <param name="email">The email address of the user, must be logged in</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
    /// <returns>Response with column name value, unless an error occurs (see <see cref="GradingService"/>)</returns>
    public string GetColumnName(string email, string boardName, int columnOrdinal)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            Board board = user.GetBoard(boardName);
            TaskController TC = board.GetTaskController();
            String ans = TC.GetColumnName(columnOrdinal);
            {
                res = new Response(null, ans);
                log.Debug("columns name is succsesfully returnd");
            }
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    /// <summary>
    /// This method gets the limit of a specific column.
    /// </summary>
    /// <param name="email">The email address of the user, must be logged in</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
    /// <returns>Response with column limit value, unless an error occurs (see <see cref="GradingService"/>)</returns>
    public string GetColumnLimit(string email, string boardName, int columnOrdinal)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    int ans = TC.GetColumnLimit(columnOrdinal);
                    res = new Response(null, ans);
                    log.Debug("columns limit is succsesfully returnd");
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user is not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// This method returns a column given it's name
    /// </summary>
    /// <param name="email">Email of the user. Must be logged in</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
    /// <returns>Response with  a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
    public string GetColumn(string email, string boardName, int columnOrdinal)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    Board board = user.GetBoard(boardName);
                    TaskController TC = board.GetTaskController();
                    List<object> list = TC.GetColumn(columnOrdinal);
                    res = new Response(null, list);
                    log.Debug("columns is succsesfully returnd");
                }
                else
                {
                    res = new Response("user is not logged in ", null);
                }
            }
            else
            {
                res = new Response("user is not exist in the system ", null);
            }
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}