using System.Data.SQLite;
using System.Collections.Generic;

public struct SupervisorData
{
    public int ID;
    public string Name, Organization, Email, Phone, Comments;

    public SupervisorData(int id, string name, string organization, string email, string phone = "", string comments = "") : this()
    {
        ID = id;
        Name = name;
        Organization = organization;
        Email = email;
        Phone = phone;
        Comments = comments;
    }
    public List<SupervisorData> GetSupervisors()
    {
        DatabaseConnector connector = new DatabaseConnector();
        SQLiteDataReader reader = connector.GetTableReader("Supervisor");
        List<SupervisorData> supervisor = new List<SupervisorData>();

        while (reader.Read())
        {
            SupervisorData data = new SupervisorData((int)reader["ID"],
                                                (string)reader["Name"],
                                                (string)reader["Organization"],
                                                (string)reader["Email"],
                                                (string)reader["Phone"],
                                                (string)reader["Comments"]);
            supervisor.Add(data);
        }

        reader.Close();
        return supervisor;
    }
}

