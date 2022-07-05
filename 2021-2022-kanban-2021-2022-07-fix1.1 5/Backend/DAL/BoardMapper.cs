using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DAL
{
    class BoardMapper : DALMapper
    { 
        const int columLimitStarter = -1;
        private readonly string tableName;

        public BoardMapper(String tableName) : base(tableName)
        {
            this.tableName = tableName;
        }


        public List<DTO> LoadBoards()
        {
            return this.Select();
        }


        public void SetNewOwner(int boardId, string emailnew ,string emailold ) {

            this.Update(boardId,"Owner", emailnew);
        }
        public void LimitColumn(int boardID,int columnOrdinal, int limit)
        {
            this.Update(boardID, "limitColumn"+columnOrdinal, limit);
        }

        public void CreateBoard(BoardDTO bDTO)
        {

            if (!Insert(bDTO.GetName(), bDTO.GetId(), bDTO.GetEmailOwner(),-1))
            {
                throw new Exception("the insertion failed");
            }
           
        }


        public void DeleteBoard(BoardDTO bDTO)
        {
            if (!this.Delete(bDTO))
            {
                throw new Exception("the delete failed");
            }
        }


        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            BoardDTO result = new BoardDTO( (int)reader.GetInt32(0),reader.GetString(1), reader.GetString(2), (int)reader.GetInt32(3), (int)reader.GetInt32(4), (int)reader.GetInt32(5), (int)reader.GetInt32(6));
            return result;
        }


        public bool Insert(string name, int id,string owner,int lastTaskID)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO {tableName} (id,name,Owner,limitColumn0,limitColumn1,limitColumn2,lastTaskID) VALUES (@id,@name,@owner,@limitColumn0,@limitColumn1,@limitColumn2,@lastTaskID)"
                };
                try
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@owner", owner);
                    command.Parameters.AddWithValue("@limitColumn0", columLimitStarter);
                    command.Parameters.AddWithValue("@limitColumn1", columLimitStarter);
                    command.Parameters.AddWithValue("@limitColumn2", columLimitStarter);
                    command.Parameters.AddWithValue("@lastTaskID", lastTaskID);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }

        public List<DTO> GetAllBoards()
        {
            return this.Select();
        }


        public void RemoveBoard(int boardID)
        {
            if (!Delete(boardID))
            {
                throw new Exception("the delete failed");
            }
        }
        public bool Delete(int boardID)//more specific function
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from Board where id={boardID}"
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


    }
}