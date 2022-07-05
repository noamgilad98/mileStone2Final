
using IntroSE.Kanban.Backend.ServiceLayer;
namespace IntroSE.Kanban.BackendTests
{
    class Program
    {
        static void Main(string[] args)
        {
            FactoryService fc = new FactoryService();

            //Console.WriteLine("Like_Grading tests:");
            //new Kanban.BackendTests.Like_Grading().RunTests();

           // Console.WriteLine("User tests:");

           //// new Kanban.BackendTests.TestUser(fc.userService,fc.boardService,fc.taskService).RunTests();
           // Console.WriteLine("TaskTests");
           // fc.userService.DeleteData();
           // fc.userService.AddNewUser("reutarad103@gmail.com", "A123123");
           // fc.userService.AddNewUser("noamgilad98@gmail.com", "A123123");
           // fc.boardService.CreateBoard("reutarad103@gmail.com", "HW");
           // fc.userService.JoinBoard("noamgilad98@gmail.com", 0);
           // fc.taskService.AddTask("reutarad103@gmail.com", "HW", "title", "desc", new DateTime(2023,3,10));
           // fc.taskService.ChangeAssignee("reutarad103@gmail.com", "reutarad103@gmail.com", "HW", 0, 0);
           // new Kanban.BackendTests.TestTask(fc.taskService).RunTests();

           new Kanban.BackendTests.TestUser(fc.userService,fc.boardService,fc.taskService).RunTests();

            Console.WriteLine("Board tests");
            new Kanban.BackendTests.TestBoard(fc.boardService, fc.userService, fc.taskService).RunTests();

            Console.WriteLine("Task tests:");
            new Kanban.BackendTests.TestTask(fc.taskService).RunTests();

        }
    }
}
