public struct StudentData
{
    public int ID;
    public string Name, University, Email, Phone, Comments;

    public StudentData(int id, string name, string university, string email, string phone, string comments) : this()
    {
        ID = id;
        Name = name;
        University = university;
        Email = email;
        Phone = phone;
        Comments = comments;
    }
}