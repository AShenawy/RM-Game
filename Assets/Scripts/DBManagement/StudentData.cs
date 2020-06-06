using System.Data.SQLite;
using System.Collections.Generic;

public struct StudentData
{
    int ID;
    string Name, University, Email, Phone, Comments;

    public StudentData(int id, string name, string university, string email, string phone, string comments) : this()
    {
        ID = id;
        Name = name;
        University = university;
        Email = email;
        Phone = phone;
        Comments = comments;
    }

    public List<StudentData> GetStudents()
    {
        DatabaseConnector connector = new DatabaseConnector();
        SQLiteDataReader reader = connector.GetTableReader("Student");
        List<StudentData> students = new List<StudentData>();

        while (reader.Read())
        {
            StudentData data = new StudentData((int)reader["ID"],
                                                (string)reader["Name"],
                                                (string)reader["University"],
                                                (string)reader["Email"],
                                                (string)reader["Phone"],
                                                (string)reader["Comments"]);
            students.Add(data);
        }

        reader.Close();
        return students;
    }
}

