using IntroSE.Kanban.Backend.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using static System.Linq.Enumerable;
namespace IntroSE.Kanban.Backend.BusinessLayer;

public class BoardController
{
    [JsonProperty("boards")]
    public Dictionary<int, Board> boards;
    private string emailOwner;
    private BoardMapper boardMapper = new BoardMapper("Board");
    private UsersToBoardsMapper usersToBoardsMapper = new UsersToBoardsMapper("UsersToBoards");
    public BoardController(string emailOwner)
    {
        int boardCounter = -1;
        this.boards = new Dictionary<int, Board>();
     
        this.emailOwner = emailOwner;
    }
    public BoardController(Dictionary<int, Board> boardsDic,string emailOwner)
    {
        int boardCounter = -1;
        this.boards = boardsDic;
        this.emailOwner = emailOwner;
        
    }

    public void CreateBoard(string name,int ID)
    {
        if (!string.IsNullOrEmpty(name))
        {
            if (!isBoardNameExsist(name))
            {
               
                Board new_board = new Board(name, ID, emailOwner);
                this.boards.Add(ID, new_board);
                try
                {
                    usersToBoardsMapper.JoinBoard(emailOwner, ID);
                    boardMapper.CreateBoard((BoardDTO)new_board.GetBoardDTO());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("board name is taken!");
            }
        }
        else
        {
            throw new Exception("board name cant be null or empty!");
        }
    }


    public void RemoveBoard(int boardID)
    {
        this.boards.Remove(boardID);
    }


    

    public Board GetBoard(int ID)
    {
        foreach (KeyValuePair<int, Board> pair in boards)
        {
            if (pair.Value.GetID() == ID)
            {
                return pair.Value;
            }
        }
       
        throw new Exception("the board is not exsist");
    }


    public Board GetBoard(string name)
    {
        foreach (KeyValuePair<int, Board> pair in boards)
        {
            if (pair.Value.GetName().Equals(name))
            {
                return pair.Value;
            }
        }
      
        throw new Exception("the board is not exsist");
    }


    public bool IsBoardExsist(int ID)
    {
        return (this.boards.ContainsKey(ID));
    }

    public Dictionary<int,Board> GetBoards()
    {
        return this.boards;
    }

   
    public void AddBoard(int boardID, Board board)
    {
        if (!isBoardNameExsist(board.GetName()))
        {
            this.boards.Add(boardID, board);
        }
        else
        {
            throw new Exception("board name is exsist in this board");
        }
            
    }


    public List<int> GetBoardsList()
    {    
        List<int> keyList1 = new List<int>(this.boards.Keys);
     
        return keyList1;
    }
    public List<string> GetBoardsListNames()
    {
        List<string> namesList = new List<string>();
        foreach (KeyValuePair<int, Board> pair in boards)
        {
            namesList.Add(pair.Value.GetName());
        }
        return namesList;
    }


    public bool isBoardNameExsist(string name)
    {

        foreach (KeyValuePair<int, Board> pair in boards)
        {
            if (pair.Value.GetName().Equals(name))
            {
                return true;
            }
        }
        return false;
    }


    

    public void SetNewOwner(string emailOld,string emailNew ,int boardID)
    {

        bool isBoardFound = false;
        foreach (KeyValuePair<int, Board> pair in boards)
        {
            if(pair.Value.GetID() == boardID)
            {
                this.GetBoard(boardID).SetNewOwner(emailNew);
                boardMapper.SetNewOwner(boardID, emailNew, emailOld);
                isBoardFound = true;
            }
        }
        if(!isBoardFound)
        {
            throw new Exception("this board not found in this user!");
        }
           
    }


    public string GetBoardName(int ID)
    {
        return (boards[ID].GetName());
    }


}


