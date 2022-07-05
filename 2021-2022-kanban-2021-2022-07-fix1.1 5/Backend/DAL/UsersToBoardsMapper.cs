using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IntroSE.Kanban.Backend.DAL
{
	public class UsersToBoardsMapper:DALMapper
	{
        private readonly string _tableName;
        public UsersToBoardsMapper(string _tableName) : base(_tableName)
        {
            this._tableName = _tableName;
        }


        public void JoinBoard(string email, int boardID)
        {
            if (!Insert(email, boardID))
            {
                throw new Exception("the insertion failed");
            }
        }

        public void LeaveBoard(string email, int boardID)
        {
            if(!Delete(email, boardID))
            {
                throw new Exception("the delete failed");
            }
        }

        public void RemoveBoard(int boardID)
        {
            if (!Delete( boardID))
            {
                throw new Exception("the delete failed");
            }
        }
        public bool Delete(String email, int boardID)//more specific function
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where email='{email}' AND boardID={boardID}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                   
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public bool Delete(int boardID)//more specific function
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from UsersToBoards where boardID={boardID}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }


        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            UsersToBoardsDTO result = new UsersToBoardsDTO(reader.GetString(0),(int)reader.GetInt32(1));
            return result;
        }

      

        public bool Insert(string email, int boardID)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO {_tableName} (email,boardID) VALUES (@email,@boardID)"
                };
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@boardID", boardID);
                    
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

        public List<DTO> GetBoardsByEmail(string email)
        {
            List<DTO> results = new List<DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from UsersToBoards where email='{email}';";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

                    }
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }
    }
}

