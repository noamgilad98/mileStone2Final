

using IntroSE.Kanban.Backend.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IntroSE.Kanban.Backend.BusinessLayer;

public class UserController
{
    [JsonProperty("_users")]
    private readonly Dictionary<string, User> _users = new Dictionary<string, User>();//
    private readonly Dictionary<int, Board> _boards = new Dictionary<int, Board>();


    private int task_counter;
    private int board_counter;
    private UserMapper userMapper = new UserMapper("User");
    private BoardMapper boardMapper = new BoardMapper("Board");
    private TaskMapper taskMapper = new TaskMapper("Task");
    private UsersToBoardsMapper usersToBoardsMapper = new UsersToBoardsMapper("UsersToBoards");
    public UserController()
    {
        int task_counter = -1;
        int board_counter = -1;

    }
    public bool IsUserExists(string email)
    {
        if (_users.ContainsKey(email))
            return true;
        else return false;
    }
    public void AddNewUser(String email, String password)
    {
        if (!IsUserExists(email))
        {
            if (!string.IsNullOrEmpty(email) && CheckEmail(email))
            {
                if (!string.IsNullOrEmpty(password) && CheckPassword(password))
                {
                    User user = new User(email, password);
                    _users.Add(email, user);
                    try { userMapper.AddNewUser(email, password); }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                }
                else { throw new Exception("the password is ilegal"); }
            }
            else { throw new Exception("the email is ilegal"); }
        }
        else { throw new Exception("the user is already exist"); }
    }

    internal bool CheckPassword(String password)
    {
        bool upperCase = false;
        bool lowerCase = false;
        bool number = false;
        if (password != null && password.Length >= 6 && password.Length <= 20)
        {
            for (int i = 0; i < password.Length; i++) {
                if (Char.IsUpper(password[i]))
                    upperCase = true;
                if (Char.IsLetter(password[i]))
                    lowerCase = true;
                if (Char.IsDigit(password[i]))
                    number = true;
            }
            if (upperCase & lowerCase & number)
                return true;
            else return false;
        }
        return false;

    }

    internal bool CheckEmail(String email)
    {
        var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        bool isValid = Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        return isValid;

    }

    public User GetUser(String email)
    {
        if (IsUserExists(email)) {
            return _users[email];
        }
        throw new KeyNotFoundException();
    }

    public int GetTaskCounter() {
        SetTaskCounter();
        return this.task_counter;
    }

    public void SetTaskCounter()
    {
        this.task_counter++;
    }
    public int GetboardCounter()
    {
        SetboardCounter();
        return this.board_counter;
    }

    public void SetboardCounter()
    {
        this.board_counter++;
    }

    public void JoinBoard(string email, int boardID)
    {
        bool isFound = false;
        foreach (KeyValuePair<string, User> pair1 in _users)
        {
            BoardController boards = pair1.Value.GetBoardController();
            foreach (KeyValuePair<int, Board> pair2 in boards.GetBoards())

            {
                if (pair2.Value.GetID() == boardID && !isFound)
                {
                    User user = GetUser(email);
                    user.boardController.AddBoard(boardID, pair2.Value);
                    try { usersToBoardsMapper.JoinBoard(email, boardID); }
                    catch (Exception ex) { throw new Exception(ex.Message); }//DAL
                    isFound = true;
                }
            }
        }
        if (!isFound)
        {
            throw new Exception("Board not found");
        }
    }
    public void LeaveBoard(string email, int boardID)
    {
        bool isFound = false;
        foreach (KeyValuePair<string, User> pair1 in _users)
        {
            BoardController boards = pair1.Value.GetBoardController();
            foreach (KeyValuePair<int, Board> pair2 in boards.GetBoards())
            {
                if (pair2.Value.GetID() == boardID && !isFound)
                {

                    User user = GetUser(email);
                    Board board = user.boardController.GetBoard(boardID);
                    if (!board.GetBoardOwner().Equals(email))
                    {
                        isFound = true;
                        user.boardController.RemoveBoard(boardID);
                        try { usersToBoardsMapper.LeaveBoard(email, boardID); }//DAL
                        catch (Exception ex) { throw new Exception(ex.Message); }
                        try { taskMapper.LeaveBoard(email, boardID, "Assignee", "Null"); }//DAL
                        catch (Exception ex) { throw new Exception(ex.Message); }
                    }
                    else
                    {
                        throw new Exception("board owner cant leave his own board");
                    }

                }
            }
        }
        if (!isFound)
        {
            throw new Exception("board not found");
        }
    }

    public void LoadData() {

        var boardDTOs = boardMapper.GetAllBoards();
        this.board_counter = -1;
        foreach (BoardDTO bDTO in boardDTOs)
        {
            this.board_counter = Math.Max(this.board_counter, bDTO.GetId());
            var column0 = taskMapper.GetAlltasks(0, bDTO.GetId());//creat column 0 of this board
            List<object> list0 = new List<object>();
            foreach (TaskDTO tDTO in column0)
            {
                Task t = new Task(tDTO);
                list0.Add(t);
            }

            var column1 = taskMapper.GetAlltasks(1, bDTO.GetId());// creat column 1 of this board
            List<object> list1 = new List<object>();
            foreach (TaskDTO tDTO in column1)
            {
                Task t = new Task(tDTO);
                list1.Add(t);
            }

            var column2 = taskMapper.GetAlltasks(2, bDTO.GetId());// creat column 2 of this board
            List<object> list2 = new List<object>();
            foreach (TaskDTO tDTO in column2)
            {
                Task t = new Task(tDTO);
                list2.Add(t);
            }
            Board board = new Board(bDTO, list0, list1, list2);//creat the board with the right columns
            _boards.Add(board.GetID(), board);

        }



        var userDTOs = userMapper.GetAllUsers();
        foreach (UserDTO uDTO in userDTOs)
        {
            Dictionary<int, Board> boardsDic = new Dictionary<int, Board>();//this is the board controller of the user
            var usersToBoardsDTO = usersToBoardsMapper.GetBoardsByEmail(uDTO.GetEmail());
            foreach (UsersToBoardsDTO utbDTO in usersToBoardsDTO)
            {
                boardsDic.Add(utbDTO.GetBoardID(), _boards[utbDTO.GetBoardID()]);
            }
            User user = new User(uDTO, boardsDic);
            _users.Add(user.GetEmail(), user);
        }
    }

    public void DeleteData()
    {
        _users.Clear();
        task_counter = -1;
        board_counter = -1;
        if (!userMapper.DeleteData("User"))
        {
            throw new Exception("failed to delete all the user data");
        }
        if (!boardMapper.DeleteData("Board"))
        {
            throw new Exception("failed to delete all the board data");
        }
        if (!taskMapper.DeleteData("task"))
        {
            throw new Exception("failed to delete all the tasks data");
        }
        if (!usersToBoardsMapper.DeleteData("UsersToBoards")) ;
    }

    public void DeleteBoard(string boardName, string email)
    {
        User user = GetUser(email);
        Board board = user.boardController.GetBoard(boardName);
        int boardID = board.GetID();
        string owner = user.boardController.GetBoard(boardID).GetBoardOwner();
        if (owner.Equals(email))
        {
            foreach (KeyValuePair<string, User> pair in _users)
            {
                if (pair.Value.boardController.IsBoardExsist(boardID))
                {
                    pair.Value.boardController.RemoveBoard(boardID);
                    boardMapper.Delete(boardID);
                    usersToBoardsMapper.Delete(boardID);
                    taskMapper.DeleteTasksByBoardID(boardID);
                }

            }
        }

    }
    public string GetBoardName(int ID)
    {
        foreach (KeyValuePair<string, User> pair in _users)
        {
            if(pair.Value.boardController.IsBoardExsist(ID))
            {
                return (pair.Value.boardController.GetBoardName(ID));
            }
        }
        throw new Exception("this board id not found!");
    }

    }


