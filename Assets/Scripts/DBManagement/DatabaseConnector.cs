using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;

public class DatabaseConnector
{
    string connector;

    public DatabaseConnector() => connector = $"URI=file:{ Application.dataPath }/MethodicaDB.db";
    ~DatabaseConnector() { }

    //public void UpdateTopicData(int studentID)
    //{
    //    using (IDbConnection connection = new SqliteConnection(connector))
    //    {
    //        connection.Open();

    //        using (IDbCommand command = connection.CreateCommand())
    //        {
    //            string query = $"UPDATE Topic SET Student_ID = {studentID}";
    //            command.CommandText = query;
    //            command.ExecuteScalar();
    //            connection.Close();
    //        }
    //    }
    //}

    //public List<StudentData> GetStudents()
    //{
    //    using (IDbConnection connection = new SqliteConnection(connector))
    //    {
    //        connection.Open();

    //        using (IDbCommand command = connection.CreateCommand())
    //        {
    //            List<StudentData> students = new List<StudentData>();
    //            string query = "SELECT Name FROM Student";
    //            command.CommandText = query;
    //            IDataReader reader = command.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                StudentData data = new StudentData(int.Parse(reader["ID"].ToString()),
    //                                                    (string)reader["Name"],
    //                                                    (string)reader["University"],
    //                                                    (string)reader["Email"],
    //                                                    (string)reader["Phone"],
    //                                                    (string)reader["Comments"]);
    //                students.Add(data);
    //            }
    //            reader.Close();
    //            return students;
    //        }
    //    }
    //}

    //public List<SupervisorData> GetSupervisors()
    //{
    //    List<SupervisorData> supervisor = new List<SupervisorData>();

    //    using (IDbConnection connection = new SqliteConnection(connector))
    //    {
    //        connection.Open();

    //        using (IDbCommand command = new SqliteCommand("SELECT * FROM Supervisor", connection as SqliteConnection))
    //        {
    //            using (IDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    SupervisorData data = new SupervisorData((int)reader["ID"],
    //                                                        (string)reader["Name"],
    //                                                        (string)reader["Organization"],
    //                                                        (string)reader["Email"],
    //                                                        (string)reader["Phone"],
    //                                                        (string)reader["Comments"]);
    //                    supervisor.Add(data);
    //                }
    //            }
    //            return supervisor;
    //        }
    //    }
    //}

    //public List<TopicData> GetTopics()
    //{
    //    List<TopicData> topics = new List<TopicData>();

    //    using (IDbConnection connection = new SqliteConnection(connector))
    //    {
    //        connection.Open();

    //        using (IDbCommand command = new SqliteCommand("SELECT * FROM Topic", connection as SqliteConnection))
    //        {
    //            using (IDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    TopicData data = new TopicData((int)reader["ID"],
    //                                                        (int)reader["Student_ID"],
    //                                                        (int)reader["Supervisor_ID"],
    //                                                        (int)reader["Cosupervisor_ID"],
    //                                                        (int)reader["ResearchType"],
    //                                                        (int)reader["StudyLevel"],
    //                                                        (int)reader["GoalType"],
    //                                                        (string)reader["Title"],
    //                                                        (string)reader["ExpectedOutcome"],
    //                                                        (string)reader["RegistrationDate"],
    //                                                        (string)reader["ExpringDate"],
    //                                                        (string)reader["Comments"],
    //                                                        (string)reader["Keywords"]);
    //                    topics.Add(data);
    //                }
    //            }
    //            return topics;
    //        }
    //    }
    //}
}