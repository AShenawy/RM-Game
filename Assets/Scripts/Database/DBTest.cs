using UnityEngine;
using Methodyca.Database;

public class DBTest : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI supervisor, title, expectedOutcome;

    void Start()
    {
        var students = DataAccess.GetStudents();

        foreach (var item in students)
        {
            Debug.Log("Student Name: " + item.Name);
        }

        //foreach (var topic in topics)
        //{
        //    if (topic.Student_ID != 0)
        //    {
        //        //Topic was chosen / not available
        //    }

        //    title.text = topic.Title;
        //    supervisor.text = supervisors.Single(s => s.ID == topic.Supervisor_ID).Name;
        //    expectedOutcome.text = topic.ExpectedOutcome;
        //}
    }
}