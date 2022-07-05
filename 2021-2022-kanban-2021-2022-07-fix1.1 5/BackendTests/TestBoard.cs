using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Reflection;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.BackendTests
{
    internal class TestBoard//
    {

        private BoardService bs;
        private UserService us;
        private TaskService ts;
        public TestBoard(BoardService bs,UserService us, TaskService ts)
        {
            this.bs = bs;
            this.us = us;
            this.ts = ts;
        }


        public void RunTests()
        {
            Console.WriteLine(us.DeleteData());
            before();
            Console.WriteLine("all test should pass:");

            Console.WriteLine("Test creat new board with proper input");
            Test1();

            Console.WriteLine("Test creat new board with improper input (taken board name)");
            Test2();

            Console.WriteLine("Test creat new board with improper input (empty board name)");
            Test3();

            Console.WriteLine("Test delete board with proper input");
            Test4();

            Console.WriteLine("Test delete board with improper input (not nxsist board name)");
            Test5();

            Console.WriteLine(us.DeleteData());
        }


        public void before()
        {
            us.AddNewUser("noam@gmail.com", "aA12345");
            us.AddNewUser("israel@gmail.com", "aA12345");
        }

        /// <summary>
        /// This method test if board added.
        /// </summary>
        public void Test1()
        {
            
            try
            {

                Response json_t = new Response();
                String json_test = JsonConvert.SerializeObject(json_t, Formatting.Indented);

                String ans = bs.CreateBoard("noam@gmail.com", "todo");
                if (ans.Equals(json_test))
                {
                    Console.WriteLine("pass");
                }
                else Console.WriteLine("fail");
            }
            catch (Exception ex)
            {
                Console.WriteLine("fail");
            }
        }

        /// <summary>
        /// This method test if board added.
        /// </summary>
        public void Test2()
        {
            try
            {
                Response json_t = new Response("board name is taken!",null);
                String json_test = JsonConvert.SerializeObject(json_t, Formatting.Indented);
                String ans = bs.CreateBoard("noam@gmail.com", "todo");
                if (ans.Equals(json_test))
                {
                    Console.WriteLine("pass");
                }
                else
                {
                    Console.WriteLine("fail");
                }
                


            }
            catch (Exception ex)
            {

                Console.WriteLine("fail");
            }
        }

        /// <summary>
        /// This method test if board added.
        /// </summary>
        public void Test3()
        {
            try
            {
                Response json_t = new Response("board name cant be null or empty!", null);
                String json_test = JsonConvert.SerializeObject(json_t, Formatting.Indented);
                String ans = bs.CreateBoard("noam@gmail.com", "");
                if (ans.Equals(json_test))
                {
                    Console.WriteLine("pass");
                }
                else
                {
                    Console.WriteLine("fail");
                }



            }
            catch (Exception ex)
            {

                Console.WriteLine("fail");
            }
        }

        /// <summary>
        /// This method test if board deleted.
        /// </summary>
        public void Test4()
        {
            try
            {
                Response json_t = new Response();
                String json_test = JsonConvert.SerializeObject(json_t, Formatting.Indented);
                String ans = bs.DeleteBoard("noam@gmail.com","todo");
                if (ans.Equals(json_test))
                {
                    Console.WriteLine("pass");
                }
                else
                {
                    Console.WriteLine("fail");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("fail");
            }
        }

        /// <summary>
        /// This method test if board deleted.
        /// </summary>
        public void Test5()
        {
            try
            {
                Response json_t = new Response("the board is not exsist", null);
                String json_test = JsonConvert.SerializeObject(json_t, Formatting.Indented);
                String ans =bs.DeleteBoard("noam@gmail.com", "notExsistName");
                if (ans.Equals(json_test))
                {
                    Console.WriteLine("pass");
                }
                else
                {
                    Console.WriteLine("fail");
                }



            }
            catch (Exception ex)
            {

                Console.WriteLine("fail");
            }
        }


        
       




    }
}