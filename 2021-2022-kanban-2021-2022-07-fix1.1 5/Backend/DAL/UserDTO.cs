using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DAL
{
    public class UserDTO : DTO
    {
        [JsonProperty("Email")]
        private string Email { get; set; }
        [JsonProperty("Password")]
        private string Password { get; set; }
       

        public const string IDColumnName = "ID";

        public UserDTO(string Email, string password) : base(new BoardMapper ("User"))
        {
            this.Email = Email;
            this.Password = password;           
            
        }
        
        public void TransferOwner(UserDTO uDTO,BoardDTO bDTO)
        {
            bDTO.GetMapper().Update(bDTO.GetId(),"Owner",uDTO.GetEmail());
        }

        public List<DTO> InProgresTasks()
        {
            List<DTO> inProtasks = this._mapper.Select();
            return inProtasks;
        }

        public List<DTO> GetAllBoards()
        {
            List<DTO> allBoards = this._mapper.Select();
            return allBoards  ;
        }

        public string GetEmail()
        {
            return this.Email;
        }

        
        public string GetPassword()
        {
            return this.Password;
        }

    }
}
       



