
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace IntroSE.Kanban.BackendTests
{
    class TestUser
    {

        private readonly UserService us;
        private readonly BoardService bs;
        private readonly TaskService ts;

        public TestUser(UserService us ,BoardService bs, TaskService ts)
        {
            this.us = us;
            this.bs = bs;
            this.ts = ts;
        }

        public void RunTests()
        {
            Console.WriteLine("test if the function register a new user - return An empty response");
            Console.WriteLine(this.Register("Israel@gmail.com", "123123Aaa"));


            Console.WriteLine("test if the function register a new user with bad password - return error");
            Console.WriteLine(this.Register("Israel2@gmail.com", "123123aa"));

            Console.WriteLine(this.DeleteData());

            Console.WriteLine("test if the function register a new user - return An empty response");
            Console.WriteLine(this.Register("Israel@gmail.com", "123123Aaa"));

            Console.WriteLine("test if the function register a new user with bad password - return error");
            Console.WriteLine(this.Register("Israel2@gmail.com", "123123aa"));

            Console.WriteLine("test if the function register a new user with the same user email - return error");
            Console.WriteLine(this.Register("Israel@gmail.com", "123123Aaa")); ;

            Console.WriteLine("test if the function logout user - return An empty response");
            Console.WriteLine(this.Logout("Israel@gmail.com"));

            Console.WriteLine("test if the function logout a not login user - return error");
            Console.WriteLine(this.Logout("Israel@gmail.com"));

            Console.WriteLine("test if the function login user - return A response with the user's email");
            Console.WriteLine(this.Login("Israel@gmail.com", "123123Aaa"));

            Console.WriteLine("test if the function login allready logged in user - return error");
            Console.WriteLine(this.Login("Israel@gmail.com", "123123Aaa"));

            us.AddNewUser("noy@gmail.com", "Aa123123");
            bs.CreateBoard("noy@gmail.com", "noyboard");//boardid=0

            Console.WriteLine("test if the function join board to users board list - return An empty response ");
            Console.WriteLine(this.JoinBoard("Israel@gmail.com", 0));

            Console.WriteLine("test if the function remove board from users board list - return An empty response ");
            Console.WriteLine(this.LeaveBoard("Israel@gmail.com", 0));

            Console.WriteLine("test if the function get all users board - return A response with a list of IDs of all user's boards  ");
            Console.WriteLine(this.GetAllBoards("noy@gmail.com"));

            Console.WriteLine("test if the function get all users board - return error ");
            Console.WriteLine(this.GetAllBoards("noy5@gmail.com"));

            Console.WriteLine("test if the function join board to users board list - return An empty response ");
            Console.WriteLine(this.JoinBoard("Israel@gmail.com", 0));

            Console.WriteLine("test if the function transfer thr ownership for another user return An empty response");
            Console.WriteLine(this.TransferOwner("noy@gmail.com", "Israel@gmail.com", "noyboard"));

            Console.WriteLine(this.DeleteData());
        }



        public void SpeicialTest()
        {
            Console.WriteLine(this.DeleteData());
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
            try
            {
                return us.LoadData();
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, null);
                return JsonConvert.SerializeObject(response);
            }
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
            try
            {
                return us.DeleteData();
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, null);
                return JsonConvert.SerializeObject(response);
            }
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
                return us.AddNewUser(email, password);
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
                return us.Login(email, password);
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
                return us.Logout(email);
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
                return us.InProgresTasks(email);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, null);
                return JsonConvert.SerializeObject(response);
            }
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
                return us.JoinBoard(email, boardID);
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
                return us.LeaveBoard(email, boardID);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, null);
                return JsonConvert.SerializeObject(response);
            }
        }

        /// <summary>
        /// This method test if the function returns a list of IDs of all user's boards.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A response with a list of IDs of all user's boards, unless an error occurs </returns>
        public string GetAllBoards(string email)
        {
            try
            {
                return us.GetAllBoards(email);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, null);
               return JsonConvert.SerializeObject(response);
            } 
        }

        /// <summary>
        /// This method transfers a board ownership.
        /// </summary>
        /// <param name="currentOwnerEmail">Email of the current owner. Must be logged in</param>
        /// <param name="newOwnerEmail">Email of the new owner</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string TransferOwner(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            try
            {
                return us.TransferOwner(currentOwnerEmail, newOwnerEmail, boardName);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, null);
                return JsonConvert.SerializeObject(response);
            }
        }

        //public void CreateData()
        //{
        //    this.DeleteData();
        //    DateTime dateTime = new DateTime(2023, 12, 12);
        //    Console.WriteLine(us.AddNewUser("noam@gmail.com", "Aa123123"));
        //    Console.WriteLine(bs.CreateBoard("noam@gmail.com", "noamBoard1"));//boardID = 0
        //    Console.WriteLine(ts.AddTask("noam@gmail.com", "noamBoard1", "newTask1", "ahzaka zona1", dateTime));//id = 0
        //    Console.WriteLine(ts.AddTask("noam@gmail.com", "noamBoard1", "newTask2", "ahzaka zona2", dateTime));//id = 1
        //    Console.WriteLine(ts.AddTask("noam@gmail.com", "noamBoard1", "newTask3", "ahzaka zona3", dateTime));//id = 2
        //    Console.WriteLine(ts.LimitColumn("noam@gmail.com", "noamBoard1", 0, 10));
        //    Console.WriteLine(ts.ChangeAssignee("noam@gmail.com", "noam@gmail.com", "noamBoard1", 0, 0));
        //    Console.WriteLine(ts.ChangeAssignee("noam@gmail.com", "noam@gmail.com", "noamBoard1", 1, 0));   
        //    Console.WriteLine(us.AddNewUser("reut@gmail.com", "Aa123123"));
        //    Console.WriteLine(bs.CreateBoard("reut@gmail.com", "reutBoard1"));//boardID = 1
        //    Console.WriteLine(ts.AddTask("reut@gmail.com", "reutBoard1", "newTask1", "ahzaka zona1", dateTime));//id = 0
        //    Console.WriteLine(ts.AddTask("reut@gmail.com", "reutBoard1", "newTask2", "ahzaka zona1", dateTime));//id = 1
        //    Console.WriteLine(ts.AddTask("reut@gmail.com", "reutBoard1", "newTask3", "ahzaka zona1", dateTime));//id = 2
        //    Console.WriteLine(us.JoinBoard("reut@gmail.com", 0));
        //    Console.WriteLine(ts.ChangeAssignee("reut@gmail.com", "reut@gmail.com", "noamBoard1", 2, 0));
        //    Console.WriteLine(bs.CreateBoard("noam@gmail.com", "noamBoard2"));//boardID = 2
        //    //Console.WriteLine(bs.CreateBoard("reut@gmail.com", "reutBoard1"));//boardID = 1
        //}
        public void LoadDataTests()
        {
            //Console.WriteLine("loadDatd:");
            Console.WriteLine(this.LoadData());
            DateTime dateTime = new DateTime(2023, 10, 10);
            //us.AddNewUser("israel@gmail.com", "123123Aaa");
            //Console.WriteLine(us.JoinBoard("israel@gmail.com", 1));
            //Console.WriteLine(bs.CreateBoard("israel@gmail.com", "israelBoard1"));//boardID = 2
            //Console.WriteLine(ts.AddTask("israel@gmail.com", "israelBoard1", "newTask3", "ahzaka zona3", dateTime));//id = 0
            Console.WriteLine(this.Login("noam@gmail.com", "Aa123123"));
            Console.WriteLine(this.Login("reut@gmail.com", "Aa123123"));
            //Console.WriteLine(ts.UpdateTaskTitle("reut@gmail.com", "noamBoard1", 0, 2, "new!!!!!"));
            //Console.WriteLine(ts.UpdateTaskDescription("reut@gmail.com", "noamBoard1", 0, 2, "new!!!!!"));
            //Console.WriteLine(ts.UpdateTaskDueDate("reut@gmail.com", "noamBoard1", 0, 2, dateTime));
            //Console.WriteLine(ts.ChangeAssignee("reut@gmail.com", "reut@gmail.com", "reutBoard1", 0, 0));
            //Console.WriteLine(ts.ChangeAssignee("reut@gmail.com", "reut@gmail.com", "reutBoard1", 1, 0));
            Console.WriteLine(us.LeaveBoard("reut@gmail.com", 0));
            //Console.WriteLine(bs.DeleteBoard("reut@gmail.com", "reutBoard1"));
            //Console.WriteLine(ts.RemoveAssignee("noam@gmail.com", "noamBoard1", 0, 0));
            //Console.WriteLine(ts.AdvanceTask("noam@gmail.com", "noamBoard1", 0,0));
            //DateTime dateTime = new DateTime(2023, 12, 12);
            //Console.WriteLine(ts.AddTask("noam@gmail.com", "noamBoard1", "newTask4", "ahzaka zona3", dateTime));//id = 3
            //Console.WriteLine(us.Login("israel@gmail.com", "123123Aaa"));
            //Console.WriteLine(us.Logout("israel@gmail.com"));

        }

    }
}



