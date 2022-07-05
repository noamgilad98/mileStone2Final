using System;//
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DAL
{
    public class BoardDTO: DTO
    {
        private String name;
        private int boardID;
        private string emailOwner;
        private int limit0;
        private int limit1;
        private int limit2;
        private int lastTaskID;
        public BoardDTO(int boardID, string name, string emailOwner,int columnLimit0, int columnLimit1, int columnLimit2,int lastTaskID) : base(new TaskMapper("Task"))
        {
            this.name = name;
            this.boardID = boardID;
            this.emailOwner = emailOwner;
            this.limit0 = columnLimit0;
            this.limit1 = columnLimit1;
            this.limit2 = columnLimit2;
            this.lastTaskID = lastTaskID;


        }
        public String GetName() { return this.name; }
        public int GetId() { return this.boardID; }
        public String GetEmailOwner() { return this.emailOwner; }
        public int GetLimit0() { return this.limit0; }
        public int GetLimit1() { return this.limit1; }
        public int GetLimit2() { return this.limit2; }
        public int GetLastTaskID() { return this.lastTaskID; }
    }

}
