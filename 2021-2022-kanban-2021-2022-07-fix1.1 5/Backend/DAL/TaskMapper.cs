using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DAL
{
    class TaskMapper : DALMapper
    {
        private readonly string tableName;

        public TaskMapper(String tableName) : base(tableName)
        {
            this.tableName = tableName;
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            TaskDTO result = new TaskDTO((int)reader.GetInt32(0), DateTime.Parse(reader.GetString(1)), reader.GetString(2), reader.GetString(3), DateTime.Parse(reader.GetString(4)), reader.GetString(5), (int)reader.GetInt32(6), (int)reader.GetInt32(7));
            return result;
        }

        public void AdvanceTask(int id, int newColumn, int boardID)
        {
            this.Update(id, boardID, "columnOrdinal", newColumn);
        }

        public void CreateTask(string title, string description, DateTime dueDate, int taskID, int boardId, DateTime creationTime, string assignee, int columnOrdinal)
        {
            if (!this.Insert(taskID, title, description, dueDate, boardId, creationTime, assignee, columnOrdinal))
            {
                throw new Exception("task insertion failed");
            }
        }


        public void LimitColumn(int columnOrdinal, int limit, int boardID)
        {
            this.Update(columnOrdinal, boardID, "Limit", limit);
        }
        public bool Insert(int taskID, string title, string description, DateTime dueDate, int boardId, DateTime creationTime, string ass, int columnOrdinal)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO {tableName} (id,creationTime,Title,Description,DueDate,Assignee,BoardId,columnOrdinal) VALUES (@id,@creationTime,@title,@desc,@dd,@ass,@boardId,@columnOrdinal)"
                };
                try
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@id", taskID);
                    command.Parameters.AddWithValue("@dd", dueDate.ToString());
                    command.Parameters.AddWithValue("@desc", description);
                    command.Parameters.AddWithValue("@boardId", boardId);
                    command.Parameters.AddWithValue("@ass", ass);
                    command.Parameters.AddWithValue("@creationTime", creationTime.ToString());
                    command.Parameters.AddWithValue("@columnOrdinal", columnOrdinal);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
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

        public bool Update(long id, int BoardID, string attributeName, int attributeValue)//update int by 2 int 
        {

            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update Task set [{attributeName}]=@{attributeName} where id={id} AND BoardID={BoardID}"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
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



        public bool Update(long id, int BoardID, string attributeName, string attributeValue)//update string by 2 int 
        {

            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update Task set [{attributeName}]=@{attributeName} where id={id} AND BoardID={BoardID}"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
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
        public bool LeaveBoard(string email, int BoardID, string attributeName, string attributeValue)//update assignee by boardID and email  
        {

            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update Task set Assignee='Null' where BoardID={BoardID} AND Assignee='{email}'"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
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
            return res >= 0;
        }




        public List<DTO> SelectTasks(int columnOrdinal,int boardID)
        {
            List<DTO> results = new List<DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from Task where BoardID={boardID} AND columnOrdinal={columnOrdinal};";
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
        public List<DTO> GetAlltasks(int columnOrdinal, int boardID)
        {
            return this.SelectTasks(columnOrdinal, boardID);
        }
        public bool DeleteTasksByBoardID(int boardID)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from Task where BoardID={boardID}"
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
        //public bool Update(long id, string attributeName, string attributeValue, int BoardID)
        //{
        //    int res = -1;
        //    using (var connection = new SQLiteConnection(_connectionString))
        //    {
        //        SQLiteCommand command = new SQLiteCommand
        //        {
        //            Connection = connection,
        //            CommandText = $"update Task set [{attributeName}]=@{attributeName} where id={id} AND BoardID={BoardID}"
        //        };
        //        try
        //        {
        //            command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //        finally
        //        {
        //            command.Dispose();
        //            connection.Close();

        //        }

        //    }
        //    return res > 0;
        //}
    }
}
