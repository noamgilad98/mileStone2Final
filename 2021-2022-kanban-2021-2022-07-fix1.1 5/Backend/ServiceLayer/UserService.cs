using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;
using log4net;

public class UserService
{
    
    private UserController uc;
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public UserService(UserController uc) {
        this.uc = uc;
    }
    public UserService() { }

    /// <summary>
    /// This method registers a new user to the system.
    /// </summary>
    /// <param name="email">The user email address, used as the username for logging the system.</param>
    /// <param name="password">The user password.</param>
    /// <returns>The string "'email':'israel@gmail.com','password':'123456','hasError':'false'", unless an error occurs than throw exception</returns>
    public string AddNewUser(string email, string password){
        Response res = new Response();
        try
        {
            uc.AddNewUser(email, password);
            log.Debug("user added");
        }
        catch (Exception ex){
            res = new Response(ex.Message,null);
            return JsonConvert.SerializeObject(res, Formatting.Indented);
        }
        try
        {
            string jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex){
            return ex.Message;
        }
    }
    /// <summary>
    ///  This method logs in an existing user.
    /// </summary>
    /// <param name="email">The email address of the user to login</param>
    /// <param name="password">The password of the user to login</param>
    /// <returns>The string "'email':'israel@gmail.com','password':'123456','hasError':'false'", unless an error occurs than throw exception</returns>
    public string Login(string email, string password)
    {
        Response res = new Response(null,email);
        try
        {
            User user = uc.GetUser(email);
            user.Login(password);
            log.Debug("user logged in");
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
            return JsonConvert.SerializeObject(res, Formatting.Indented);
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
    ///  This method logs out an existing user.
    /// </summary>
    /// <param name="email">The email address of the user to logout</param>
    /// <param name="password">The password of the user to logout</param>
    /// <returns>The string "'email':'israel@gmail.com','password':'123456','hasError':'false'", unless an error occurs than throw exception</returns>
    public String Logout(string email)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            user.Logout();
            log.Debug("user logged out");

        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
            return JsonConvert.SerializeObject(res, Formatting.Indented);
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
    /// This method returns all the In progress tasks of the user.
    /// </summary>
    /// <param name="email">Email of the user. Must be logged in</param>
    /// <returns>Response with  a list of the in progress tasks, unless an error occurs than throw exception </returns>
    public string InProgresTasks(string email)
    {

        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            List<object> inProgresTasks = user.InProgresTasks(email);
            res = new Response(null,inProgresTasks);
            log.Debug("user logged in");
        }
        catch (Exception ex) {
            res = new Response(ex.Message,null); 
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res,Formatting.Indented);
            return jsonTOSend;
        }
        catch (Exception ex) {
            return ex.Message;
        }
    }
    public object GetUser(string email) {
        Response res = null;
        try
        {
            return uc.GetUser(email);
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
            return res;
        }

    }
    public string TransferOwner(string currentOwnerEmail, string newOwnerEmail, string boardName)
    {
        Response res = new Response();
        try
        {
            User user1 = uc.GetUser(currentOwnerEmail);
            User user2 = uc.GetUser(newOwnerEmail);
            int boardId = user2.GetBoardController().GetBoard(boardName).GetID();
            user1.TransferOwner(user2, boardId);
            log.Debug("transfer owener ");
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
    public string JoinBoard(string email, int boardID)
    {
        Response res = new Response();
        try
        {
            uc.JoinBoard(email, boardID);
            log.Debug("joiend board ");
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
    public string LeaveBoard(string email, int boardID)
    {
        Response res = new Response();
        try
        {
            uc.LeaveBoard(email, boardID);
            log.Debug("leaved board ");
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
    public string GetAllBoards(string email)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            List<int> listId = user.GetAllBoards();
            res = new Response(null, listId);
            log.Debug("gets all the boards ");
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
    public string GetAllBoardsNames(string email)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            List<string> nameList = user.GetAllBoardsNames();
            res = new Response(null, nameList);
            log.Debug("gets all the boards names ");
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

    public string LoadData()
    {
        Response res = new Response();
        try
        {
            uc.LoadData();

            log.Debug("load data ");
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
    public string DeleteData()
    {
        Response res = new Response();
        try
        {
            uc.DeleteData();
            log.Debug("delete all the data");
        }
        catch (Exception ex)
        {
            res = new Response(ex.Message, null);
        }
        try
        {
            String jsonTOSend = JsonConvert.SerializeObject(res, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return jsonTOSend;

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

}


