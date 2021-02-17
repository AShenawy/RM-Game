using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Interview : MonoBehaviour
{
    static string[] questions = new string[5] {
        "Are you satisfied with your romantic life?",
        "Could you describe how you get to know and connect with new people?",
        "What are your motivations and interests using a dating app?",
        "What are the features in a dating app that you dislike so much?",
        "What are your wishes/desires when you meet new people on a dating app?"
        };
    static string[] answers1 = new string[5] {
        "...",
        "I like online games, so I make new friends there",
        "Loneliness",
        "Random encouraging messages",
        "Making friends"
        };
    static string[] answers2 = new string[5] {
        "No",
        "I often meet new people when I go to parties",
        "Curiosity",
        "Selfies or profiles without face photos",
        "To see how much people will like me, compared to their other dates"
        };
    static string[] answers3 = new string[5] {
        "No",
        "I don't meet new people so often... but sometimes I join board game events and I can see new people there",
        "Just broke up with my girlfriend",
        "Section to list favourite couples in my bio",
        "To find a new friend/wife"
        };
    static string[] answers4 = new string[5] {
        "Yes",
        "My friends often throw a home party. Whenever I join, I can meet new people.",
        "Hope to find a partner",
        "Friends' opinions",
        "Meet my match"
        };
    static string[] answers5 = new string[5] {
        "Yes",
        "I don't know... I don't remember",
        "Boredom",
        "Reminders to take a break and enjoy real life",
        "To have someone to chat with"
        };
    static int questionNr = 0;
    public Text question;
    public Text answer1;
    public Text answer2;
    public Text answer3;
    public Text answer4;
    public Text answer5;
    public Button back;
    public Button next;

    // Start is called before the first frame update
    void Start()
    {
        question.text = questions[questionNr];
        answer1.text = answers1[questionNr];
        answer2.text = answers2[questionNr];
        answer3.text = answers3[questionNr];
        answer4.text = answers4[questionNr];
        answer5.text = answers5[questionNr];
        if(questionNr <= 0)
        {
            back.interactable = false;
        } 
        else if (questionNr >= 4)
        {
            next.interactable = false;
        }
    }

    public void Proceed(bool status)
    {
        if (!status)
        {
            questionNr--;
        } else
        {
            questionNr++;
        }
    } 
}
