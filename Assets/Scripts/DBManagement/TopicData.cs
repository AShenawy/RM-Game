public struct TopicData
{
    public int ID, Student_ID, Supervisor_ID, Cosupervisor_ID, ResearchType, StudyLevel, GoalType;
    public string Title, ExpectedOutcome, RegistrationDate, ExpiringDate, Comments, Keywords;

    public TopicData(
        int iD,
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
}