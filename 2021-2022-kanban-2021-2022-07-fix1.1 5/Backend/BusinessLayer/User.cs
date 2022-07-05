

using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;

namespace IntroSE.Kanban.Backend.BusinessLayer;
public class User  
{
    [JsonProperty("Email")]
    private string Email;
    [JsonProperty("Password")]
    private string Password;
    [JsonProperty("isLogin")]
    public Boolean isLogin;
    [JsonProperty("boardcontroller")]
    public BoardController boardController;
    [JsonProperty("userDTO")]
    private UserDTO userDTO;

  public User(string email, string password)
    {
        this.Password = password;
        this.Email = email;
        this.isLogin = true;
        this.boardController = new BoardController(this.Email); 
        this.userDTO = new UserDTO(email,password);
    }
    public User(UserDTO userDTO, Dictionary<int, Board> boardsDic)
    {
        this.Email = userDTO.GetEmail();
        this.isLogin = false;// all users must me logut when program falls!
        this.Password=userDTO.GetPassword();
        this.boardController = new BoardController(boardsDic,Email);
    }

    public void Login(string password)
    {
        if (Password.Equals(this.Password))
        {
            if (!isLogin)
            {
                isLogin = true;
              
            }
            else { throw new Exception("the user is already login"); }
        }
        else
        {
            throw new Exception("password is incorrect");
        }
    }
    public void Logout()
    {
        if (isLogin)
        {
            isLogin = false;
        }
        else { throw new Exception("the user is not login"); }
    }
    public List<object> InProgresTasks(string email)
    {
        List<object> inProtasks = new List<object>();
        Dictionary<int, Board> Boards = boardController.GetBoards();
        foreach (KeyValuePair <int,Board> entry in Boards)
        {
            inProtasks = JoinLists(inProtasks, entry.Value.GetTaskController().InProgressTasks());
        }
        return inProtasks;
    }

    public static List<object> JoinLists(List<object> first, List<object> second)
    {
        if (first == null)
        {
            return second;
        }
        if (second == null)
        {
            return first;
        }
        return first.Concat(second).ToList();
    }
   
    public Board GetBoard(int boardId) {

        return this.boardController.GetBoard(boardId);
    }

    public Board GetBoard(string boardName)
    {
        try
        {
            return this.boardController.GetBoard(boardName);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
                
        }
        
    }

    public string GetEmail() { return this.Email; }
    public void TransferOwner(User user, int boardID)
    {
        if (boardController.IsBoardExsist(boardID))
        {
            Board board = boardController.GetBoard(boardID);
            this.boardController.SetNewOwner(this.Email,user.GetEmail(), boardID);
            try { userDTO.TransferOwner(this.userDTO ,(BoardDTO)board.GetBoardDTO()); }//DAL
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        else throw new Exception("board is not exsist in this user");
    }
   public BoardController GetBoardController()
    {
        return this.boardController;
    }
    public List<int> GetAllBoards()
    {
        return this.boardController.GetBoardsList();
      
    }
    public List<string> GetAllBoardsNames()
    {
        return this.boardController.GetBoardsListNames();

    }
    

}
