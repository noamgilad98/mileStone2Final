using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class FactoryService
    {
        private UserController userController;
        public UserService userService;
        public BoardService boardService;
        public BoardController boardController;
        public TaskService taskService;
        public TaskController taskController;

        public FactoryService()
        {
            this.userController = new UserController();
            this.userService = new UserService(userController);
            this.boardService = new BoardService(userController);
            this.taskService = new TaskService(userController);
        }
        


    }
}
