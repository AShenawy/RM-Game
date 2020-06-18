﻿using System.Linq;
using TMPro;
using UnityEngine;

public class DBTest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI supervisor, title, expectedOutcome;

    private void Start()
    {
        var topics = DataAccess.GetTopics();
        var supervisors = DataAccess.GetSupervisors();

        foreach (var topic in topics)
        {
            if (topic.Student_ID != 0)
            {
                //Topic was taken
            }

            title.text = topic.Title;
            supervisor.text = supervisors.Single(s => s.ID == topic.Supervisor_ID).Name;
            expectedOutcome.text = topic.ExpectedOutcome;
        }
    }
}