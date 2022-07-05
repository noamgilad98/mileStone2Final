using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using IntroSE.Kanban.Backend.ServiceLayer;


namespace IntroSE.Kanban.BackendTests
{
    internal class TestTask
    {     
        private TaskService ts;

        public TestTask(TaskService ts)
        {
            this.ts=ts;
        }

        public void RunTests()

        {
            //testUpdateTaskDueDate(ts);
            //testUpdateTaskDueDate_1(ts);
            //testUpdateTaskDueDate_2(ts);
            //testUpdateTaskTitle(ts);
            //testUpdateTaskDescription(ts);
            //testRemoveAssignee(ts);

            Console.WriteLine("testDueDate:");
            testUpdateTaskDueDate(ts);

            Console.WriteLine("testChangeAssignee:");
            testChangeAssignee(ts);


            Console.WriteLine("testDueDate1:");
            testUpdateTaskDueDate_1(ts);

            Console.WriteLine("testDueDate2:");
            testUpdateTaskDueDate_2(ts);

            Console.WriteLine("testUpdateTitle:");
            testUpdateTaskTitle(ts);

            Console.WriteLine("testDesc:");
            testUpdateTaskDescription(ts);

            Console.WriteLine("testRemoveAssignee:");
            //testRemoveAssignee(ts);


            Console.WriteLine("testLimitColumn1:");
            testLimitColumn1();

            Console.WriteLine("testLimitColumn2:");
            testLimitColumn2();

            Console.WriteLine("testGetColName:");
            testGetColName();

            Console.WriteLine("testAdvanceTask_1:");
            testAdvanceTask_1();

            Console.WriteLine("testAdvanceTask_2:");
            testAdvanceTask_2();

            Console.WriteLine("testGetColumnLimit:");
            testGetColumnLimit();

            Console.WriteLine("testGetColumn:");
            testGetColumn();


            this.SpeicialTest();

            //testAddTask(ts);
        }

        public void SpeicialTest()
        {
            ts.AddTask("noy1@gmail.com", "noyboard", "Task", "lala", new DateTime(24, 12, 23));
            ts.ChangeAssignee("noy1@gmail.com", "noy1@gmail.com", "noyboard", 0, 0);
            ts.AdvanceTask("noy1@gmail.com", "noyboard", 0, 0);
            ts.ChangeAssignee("noy1@gmail.com", "noam@gmail.com", "noyboard", 0, 0);

        }

       

            /// <summary>
            /// This method tests the update of the due date of a task.
            /// </summary>
            /// <param name="ts">The task service that's suppose to return the json below.</param>
            /// <returns> a string that indicates whether the test has failed or not. </returns>
            public void testUpdateTaskDueDate(TaskService ts)
        {
            //DateTime date = new DateTime(03 / 10 / 1999);
            //string json_testUpdateTaskDueDate = ts.UpdateTaskDueDate("reutarad103@gmail.com", "ToDo", 2, 319, date);

            Response res = new Response();
            String goodResponse = JsonConvert.SerializeObject(res,Formatting.Indented);
            DateTime date = new DateTime(2023, 3, 3);
            String output_json = ts.UpdateTaskDueDate("reutarad103@gmail.com", "HW",0,0, date);
 
            try
            {
                if (goodResponse.Equals(output_json))
                {
                    Console.WriteLine("successful");

                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// This method tests the assignee removal of a task.
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testRemoveAssignee(TaskService ts)
        {
            Response res = new Response();
            String goodResponse = JsonConvert.SerializeObject(res, Formatting.Indented);
            String output_json = ts.RemoveAssignee("reutarad103@gmail.com", "HW", 0, 0);
            try
            {
                if (!goodResponse.Equals(output_json))
                {
                    Console.WriteLine("successful");
                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This method tests the assignee change of a task.
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testChangeAssignee(TaskService ts)
        {
            Response res = new Response();
            String goodResponse = JsonConvert.SerializeObject(res, Formatting.Indented);
            String output_json = ts.ChangeAssignee("reutarad103@gmail.com", "noamgilad98@gmail.com", "HW", 0,0);
            try
            {
                if (!goodResponse.Equals(output_json))
                {
                    Console.WriteLine("unsuccessful");
                }
                else
                {
                    Console.WriteLine("successful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// This method tests the update of the due date of a task.
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testUpdateTaskDueDate_1(TaskService ts)
        {
            Response res = new Response();
            String goodResponse = JsonConvert.SerializeObject(res, Formatting.Indented);
            String output_json = ts.UpdateTaskDueDate("reutarad103@gmail.com", "HW", 0, 0, new DateTime(03 / 10 / 2023));
            try
            {
                if (!goodResponse.Equals(output_json))
                {
                    Console.WriteLine("successful");//supposed to fail because dates are  different.
                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        /// <summary>
        /// This method tests the update of the due date of a task.
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testUpdateTaskDueDate_2(TaskService ts)
        {
            Response res = new Response();
            String goodResponse = JsonConvert.SerializeObject(res, Formatting.Indented);
            String output_json = ts.UpdateTaskDueDate("reutarad103@gmail.com", "HW", 0, 0, new DateTime(03 / 10 / 1999));
            try
            {
                if (!goodResponse.Equals(output_json))
                {
                    Console.WriteLine("successful");//supposed to fail because date is in the past
                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }




        /// <summary>
        /// This method tests the update of the title of a task.
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testUpdateTaskTitle(TaskService ts)
        {
            Response res = new Response();
            String goodResponse = JsonConvert.SerializeObject(res, Formatting.Indented);
            String output_json = ts.UpdateTaskTitle("reutarad103@gmail.com", "HW", 0, 0, "new title");
            try
            {
                if (goodResponse.Equals(output_json))
                {
                    Console.WriteLine("successful");
                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        



        /// <summary>
        /// This method tests the update of the description of a task.
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testUpdateTaskDescription(TaskService ts)
        {
            Response res = new Response();
            String goodResponse = JsonConvert.SerializeObject(res, Formatting.Indented);
            String output_json = ts.UpdateTaskTitle("reutarad103@gmail.com", "HW", 0, 0, "new desc");
            try
            {
                if (goodResponse.Equals(output_json))
                {
                    Console.WriteLine("successful");
                }
                else
                {
                    Console.WriteLine("unsuccessful");


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        /// <summary>
        /// This method tests the addition of a task to the board.
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        //    public void testAddTask(TaskService ts)
        //    {
        //        Response res = new Response();
        //        String goodResponse = JsonConvert.SerializeObject(res);
        //        String output_json = ts.addTask("reutarad103@gmail.com", "HW", "title", "desc", new DateTime(02/02/2023));
        //        try
        //        {
        //            if (goodResponse.Equals(output_json))
        //            {
        //                Console.WriteLine("successful");
        //            }
        //            else
        //            {
        //                Console.WriteLine("unsuccessful");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //        }
        //    }




        //noam-------------noam-------noam-------------noam-------noam-------------noam-------noam

        //noam-------------noam-------noam-------------noam-------noam-------------noam-------noam

        //noam-------------noam-------noam-------------noam-------noam-------------noam-------noam


        public void testLimitColumn1()
        {
            
            Response json_res = new Response();
            String json_test = JsonConvert.SerializeObject(json_res, Formatting.Indented);

            String lim = ts.LimitColumn("noamgilad98@gmail.com", "HW", 0, 10);

            try
            {
                if (lim.Equals(json_test))
                {
                    Console.WriteLine("successful");
                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This method tests if the column's limit change.
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testLimitColumn2()
        {
            Response json_res = new Response();
            String json_test = JsonConvert.SerializeObject(json_res, Formatting.Indented); ;

            try
            {
                String res = ts.LimitColumn("noamgilad98@gmail.com", "HW", 0, -1);

                Console.WriteLine(json_test);
                Console.WriteLine(res);
                if (res.Equals(json_test))
                    Console.WriteLine("unsuccessful");
                else
                    Console.WriteLine("successful");




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This method tests if the String that the function GetColName return is the correct name .
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testGetColName()
        {
            Response json_res = new Response(null, "backlog");
            String json_test = JsonConvert.SerializeObject(json_res, Formatting.Indented);
            String name = ts.GetColumnName("noamgilad98@gmail.com", "HW", 0);

            try
            {
                if (name.Equals(json_test))
                {
                    Console.WriteLine("successful");
                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }






        /// <summary>
        /// This method tests if the correct task is advance .
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testAdvanceTask_1()
        {
            Response json_res = new Response();
            String json_test = JsonConvert.SerializeObject(json_res, Formatting.Indented);
            String ans = ts.AdvanceTask("noamgilad98@gmail.com", "HW", 0, 0);
            try
            {
                if (ans.Equals(json_test))
                {
                    Console.WriteLine("successful adv");
                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }




        /// <summary>
        /// This method tests if the correct task is advance .
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testAdvanceTask_2()
        {
            Response json_res = new Response();
            String json_test = JsonConvert.SerializeObject(json_res, Formatting.Indented);
            try
            {
                String ans = ts.AdvanceTask("noamgilad98@gmail.com", "HW", 2, 23);

            }
            catch (Exception ex)
            {
                Console.WriteLine("successful");
                Console.WriteLine(ex.ToString());
            }
        }








        /// <summary>
        /// This method tests if the String that the function GetColumnLimit return is the correct limit .
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testGetColumnLimit()
        {
            Response json_res = new Response(null, 10);
            String json_test = JsonConvert.SerializeObject(json_res, Formatting.Indented);
            try
            {
                String ans = ts.GetColumnLimit("noamgilad98@gmail.com", "HW", 0);
                if (ans.Equals(json_test))
                    Console.WriteLine("successful");
                else
                    Console.WriteLine("unsuccessful11");


            }
            catch (Exception ex)
            {
                Console.WriteLine("unsuccessful12");
                Console.WriteLine(ex.ToString());
            }
        }









        /// <summary>
        /// This method tests if the String that the function GetColumn return is the correct column .
        /// </summary>
        /// <param name="ts">The task service that's suppose to return the json below.</param>
        /// <returns> a string that indicates whether the test has failed or not. </returns>
        public void testGetColumn()
        {
            List<object> l = new List<object>();

            Response json_res = new Response(null, l);
            String json_test = JsonConvert.SerializeObject(json_res, Formatting.Indented);
            String ans = ts.GetColumn("noamgilad98@gmail.com", "HW", 0);
            try
            {
                if (ans.Equals(json_test))
                {
                    Console.WriteLine("successful");
                }
                else
                {
                    Console.WriteLine("unsuccessful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }





    }
}
