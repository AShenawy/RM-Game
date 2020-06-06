using System.Data.SQLite;
using System.Collections.Generic;

public struct TopicData
{
    int ID, Student_ID, Supervisor_ID, Cosupervisor_ID, ResearchType, StudyLevel, GoalType;
    string Title, ExpectedOutcome, RegistrationDate, ExpiringDate, Comments, Keywords;

    public TopicData(int iD,
                     int student_ID,
                     int supervisor_ID,
                     int cosupervisor_ID,
                     int researchType,
                     int studyLevel,
                     int goalType,
                     string title,
                     string expectedOutcome,
                     string registrationDate,
                     string expiringDate,
                     string comments,
                     string keywords)
    {
        ID = iD;
        Student_ID = student_ID;
        Supervisor_ID = supervisor_ID;
        Cosupervisor_ID = cosupervisor_ID;
        ResearchType = researchType;
        StudyLevel = studyLevel;
        GoalType = goalType;
        Title = title;
        ExpectedOutcome = expectedOutcome;
        RegistrationDate = registrationDate;
        ExpiringDate = expiringDate;
        Comments = comments;
        Keywords = keywords;
    }
    public List<TopicData> GetSupervisors()
    {
        DatabaseConnector connector = new DatabaseConnector();
        SQLiteDataReader reader = connector.GetTableReader("Topic");
        List<TopicData> topics = new List<TopicData>();

        while (reader.Read())
        {
            TopicData data = new TopicData((int)reader["ID"],
                                                (int)reader["Student_ID"],
                                                (int)reader["Supervisor_ID"],
                                                (int)reader["Cosupervisor_ID"],
                                                (int)reader["ResearchType"],
                                                (int)reader["StudyLevel"],
                                                (int)reader["GoalType"],
                                                (string)reader["Title"],
                                                (string)reader["ExpectedOutcome"],
                                                (string)reader["RegistrationDate"],
                                                (string)reader["ExpringDate"],
                                                (string)reader["Comments"],
                                                (string)reader["Keywords"]);
            topics.Add(data);
        }

        reader.Close();
        return topics;
    }
}

