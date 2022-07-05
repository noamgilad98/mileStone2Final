using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DAL
{
    public class UsersToBoardsDTO : DTO
    {
        [JsonProperty("Email")]
        private string Email { get; set; }
        [JsonProperty("Board")]
        private int BoardID { get; set; }
        

        public const string IDColumnName = "ID";

        public UsersToBoardsDTO(string Email,int BoardID) : base(new BoardMapper("UsersToBoards"))
        {
            this.Email = Email;
            this.BoardID = BoardID;
        }
        public string GetEmail()
        {
            return this.Email;
        }
        public int GetBoardID()
        {
            return this.BoardID;
        }




    }
}

