using UnityEngine;
using System.Data.SQLite;

public class DatabaseConnector
{
    string connector = $"URI=file:{ Application.dataPath }/MethodicaDB.sqlite";
    ~DatabaseConnector() { }
    public void UpdateTopicData(int studentID)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connector))
        {
            connection.Open();

            using (SQLiteCommand command = connection.CreateCommand())
            {
                string query = $"UPDATE Topic SET Student_ID = {studentID}";
                command.CommandText = query;
                command.ExecuteScalar();
                connection.Close();
            }
        }
    }

    public SQLiteDataReader GetTableReader(string tableName)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connector))
        {
            connection.Open();

            using (SQLiteCommand command = connection.CreateCommand())
            {
                string query = $"SELECT * FROM {tableName}";
                command.CommandText = query;
                return command.ExecuteReader();
            }
        }
    }
}