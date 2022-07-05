using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;
using log4net;
using log4net.Config;//
using System;
using System.IO;
using System.Reflection;//b7

public class BoardService//Boardservice
{
    private String name;
    private List<Task>[] columns;
    private UserController uc;
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    public BoardService(UserController uc)
    {
        this.name = name;
        columns = new List<Task>[3];
        this.uc = uc;
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        log.Info("Starting log!");
    }

    public BoardService()
    {
    }


   
    /// <summary>
    /// This method create column given it's name
    /// </summary>
    /// <param name="email">Email of the user. Must be logged in</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
    /// <returns>Response with  a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>

    public string CreateBoard(string email, string boardName)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    user.boardController.CreateBoard(boardName ,uc.GetboardCounter());
                    log.Debug("board is created");
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


    public string DeleteBoard(string email, string boardName)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(email);
            if (uc.IsUserExists(email))
            {
                if (user.isLogin)
                {
                    
                    uc.DeleteBoard(boardName, email);

                    log.Debug("board is deleted");
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

    public string SetNewOwner(string emailOld, string emailNew, int boardID)
    {
        Response res = new Response();
        try
        {
            User user = uc.GetUser(emailOld);
            if (uc.IsUserExists(emailOld) && uc.IsUserExists(emailNew))
            {
                if (user.isLogin)
                {
                    user.boardController.SetNewOwner(emailOld, emailNew, boardID);
                    log.Debug("new owner is set");
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



    public string GetBoardName(int boardID)
    {
        Response res = new Response();
        try
        {
            res = new Response(null, uc.GetBoardName(boardID));
            log.Debug("board name returned");
              
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

}


















































































































































































































































































































































































































































































































































































