using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DAL
{
     class UserMapper : DALMapper
    {
        private readonly string _tableName;
        public UserMapper(string _tableName) : base(_tableName) {
            this._tableName = _tableName;
        }
        public List<DTO> GetAllUsers()
        {
            return this.Select();
        }

        public void AddNewUser(string email, string password)
        {
            if (!Insert(email, password)) {
                throw new Exception("the insertion failed");
            }
        }

       

      

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            UserDTO result = new UserDTO(reader.GetString(0), reader.GetString(1));
            return result;
        }

        private bool BoolToString(string s) {

            if (s.Equals("true"))
            {
                return true;
            }
            else if (s.Equals("false")) {
                return false;
            }
            throw new Exception(s);
        }

        public bool Insert(string email,string password)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO {_tableName} (email,password) VALUES (@email,@password)"
                };
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    //log
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;

        }
    }
}
