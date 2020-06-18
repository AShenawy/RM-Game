using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public static class DataAccess
{
    public static List<StudentData> GetStudents()
    {
        using (IDbConnection connection = new SqliteConnection($"URI=file:{ Application.dataPath }/MethodicaDB.db"))
        {
            connection.Open();
            using (IDbCommand command = new SqliteCommand("SELECT * FROM Student", connection as SqliteConnection))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    List<StudentData> students = new List<StudentData>();
                    while (reader.Read())
                    {
                        StudentData data = new StudentData(int.Parse(reader["ID"].ToString()),
                                                        (string)reader["Name"],
                                                        (string)reader["University"],
                                                        (string)reader["Email"],
                                                        (string)reader["Phone"],
                                                        (string)reader["Comments"]);
                        students.Add(data);
                    }
                    return students;
                }
            }
        }
    }
    public static List<SupervisorData> GetSupervisors()
    {
        using (IDbConnection connection = new SqliteConnection($"URI=file:{ Application.dataPath }/MethodicaDB.db"))
        {
            connection.Open();
            using (IDbCommand command = new SqliteCommand("SELECT * FROM Supervisor", connection as SqliteConnection))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    List<SupervisorData> supervisor = new List<SupervisorData>();
                    while (reader.Read())
                    {
                        SupervisorData data = new SupervisorData(int.Parse(reader["ID"].ToString()),
                                                            (string)reader["Name"],
                                                            (string)reader["Organization"],
                                                            (string)reader["Email"],
                                                            (string)reader["Phone"],
                                                            (string)reader["Comments"]);
                        supervisor.Add(data);
                    }
                    return supervisor;
                }
            }
        }
    }
    public static List<TopicData> GetTopics()
    {
        using (IDbConnection connection = new SqliteConnection($"URI=file:{ Application.dataPath }/MethodicaDB.db"))
        {
            connection.Open();
            using (IDbCommand command = new SqliteCommand("SELECT * FROM Topic", connection as SqliteConnection))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    List<TopicData> topics = new List<TopicData>();
                    while (reader.Read())
                    {
                        TopicData data = new TopicData(
                            int.Parse(reader["ID"].ToString()),
                            int.Parse(reader["Student_ID"].ToString()),
                            int.Parse(reader["Supervisor_ID"].ToString()),
                            int.Parse(reader["Cosupervisor_ID"].ToString()),
                            int.Parse(reader["ResearchType"].ToString()),
                            int.Parse(reader["StudyLevel"].ToString()),
                            int.Parse(reader["GoalType"].ToString()),
                            (string)reader["Title"],
                            (string)reader["ExpectedOutcome"],
                            (string)reader["RegistrationDate"],
                            (string)reader["ExpringDate"],
                            (string)reader["Comments"],
                            (string)reader["Keywords"]);
                        topics.Add(data);
                    }
                    return topics;
                }
            }
        }
    }

    public static void UpdateTopicData(int studentID)
    {
        using (IDbConnection connection = new SqliteConnection($"URI=file:{ Application.dataPath }/MethodicaDB.db"))
        {
            connection.Open();
            using (IDbCommand command = new SqliteCommand($"UPDATE Topic SET Student_ID = {studentID}", connection as SqliteConnection))
            {
                command.ExecuteScalar();
            }
        }
    }
}