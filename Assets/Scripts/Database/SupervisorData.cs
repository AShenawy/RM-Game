namespace Methodyca.Database
{
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
    }
}